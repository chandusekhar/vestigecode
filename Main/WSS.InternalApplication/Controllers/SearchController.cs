using PagedList;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using UtilityBilling.Data;
using WSS.InternalApplication.Authorization;
using WSS.InternalApplication.CustomAttributes;
using WSS.InternalApplication.Models;
using WSS.Logging.Service;
using IUnitOfWork = WSS.Data.IUnitOfWork;

namespace WSS.InternalApplication.Controllers
{
    [Authorize]
    [CanExecuteFunction(Roles = Roles.CSR, FunctionCode = Functions.AccountSearch_Result)]
    public class SearchController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UtilityBilling.Data.IUnitOfWork _ubUnitofWork;
        private static readonly ILogger _logger = new Logger(typeof(SearchController));

        public SearchController(IUnitOfWork unitOfWork, UtilityBilling.Data.IUnitOfWork ubUnitofWork)
        {
            _unitOfWork = unitOfWork;
            _ubUnitofWork = ubUnitofWork;
        }

        [CanExecuteFunction(FunctionCode = Functions.AccountSearch_Result, Roles = Roles.CSR)]
        public ActionResult Index(string query, int? page, string rowsPerPage, string sortOrder = "", string sortDir = "")
        {
            if (query == null)
            {
                if (Session["Query"] != null)
                {
                    query = Session["Query"].ToString();
                }

                else
                {
                    return View();
                }
            }

            if (query != null && Session["Query"] != null)
            {
                if (Session["Query"].ToString() != query)
                {
                    page = 1;
                    rowsPerPage = "10";
                }
            }

            query = Server.UrlDecode(query);
            ViewBag.IsMenuHidden = "hidden";
            // Get the PageSize if set, if not default
            var pageSize = string.IsNullOrEmpty(rowsPerPage)
                ? (Convert.ToInt32((Session["UtilityAccountListRowsPerPage"] ?? 10)))
                : Convert.ToInt32(rowsPerPage);
            ViewBag.RowsPerPageSelectList = Helper.ViewDisplayHelper.GetRowCountSelectListItems(pageSize);

            // Store page size in session
            Session["UtilityAccountListRowsPerPage"] = pageSize;

            var currentSort = string.IsNullOrEmpty(sortOrder)
                ? (Convert.ToString((Session["UtilityAccountListSort"] ?? "AccountNumber")))
                : Convert.ToString(sortOrder);

            Session["UtilityAccountListSort"] = currentSort;

            if (Session["UtilityAccountListSortDirection"] == null)
                Session["UtilityAccountListSortDirection"] = "";

            if (sortDir.Equals("Change") && Session["UtilityAccountListSortDirection"].ToString().Equals("Desc"))
            {
                Session["UtilityAccountListSortDirection"] = "";
            }
            else if (sortDir.Equals("Change") && Session["UtilityAccountListSortDirection"].ToString().Equals(""))
            {
                Session["UtilityAccountListSortDirection"] = "Desc";
            }
            var currentDirection = Session["UtilityAccountListSortDirection"].ToString();
        
            // Clear the sort icons on screen back to unsorted (it is re-set below)
            ViewBag.Status = "fa fa-unsorted";
            ViewBag.AccountNumber = "fa fa-unsorted";
            ViewBag.PrimaryEmailAddress = "fa fa-unsorted";
            ViewBag.MainCustomerName = "fa fa-unsorted";

            switch (currentSort)
            {
                case "Status":
                    ViewBag.Status = currentDirection.Equals("Desc") ? "fa fa-sort-up" : "fa fa-sort-down";
                    break;

                case "AccountNumber":
                    ViewBag.AccountNumber = currentDirection.Equals("Desc") ? "fa fa-sort-up" : "fa fa-sort-down";
                    break;

                case "PrimaryEmailAddress":
                    ViewBag.PrimaryEmailAddress = currentDirection.Equals("Desc") ? "fa fa-sort-up" : "fa fa-sort-down";
                    break;

                case "MainCustomerName":
                    ViewBag.MainCustomerName = currentDirection.Equals("Desc") ? "fa fa-sort-up" : "fa fa-sort-down";
                    break;
                default:
                    ViewBag.Status = currentDirection.Equals("Desc") ? "fa fa-sort-up" : "fa fa-sort-down";
                    break;
            }

            var pageNumber = !page.HasValue
            ? (Convert.ToInt32((Session["UtilityAccountListPageNumber"] ?? 1)))
            : Convert.ToInt32(page.Value);

            var viewodel = GetSearchList(query, currentSort, currentDirection, pageNumber, pageSize);

            // store the query string so it is re-displayed on screen
            ViewBag.Query = query;
            Session["Query"] = query;
            //query.All(char.IsDigit);
            if (!string.IsNullOrEmpty(query) && query.Length == 10)
            {
              ViewBag.IsDigitOnly = query.All(char.IsDigit);
            }
            return View(viewodel);
        }

        private IPagedList<SearchViewModel> GetSearchList(string query, string sortBy, string sortDirection, int pageNumber, int pageSize)
        {
            var finalresult = new List<SearchViewModel>();
            int totalSearchedRecords;

            if (query.Contains("@"))
            {
                var result = _unitOfWork.WssAccountRepository.FindAll().Where(x => x.PrimaryEmailAddress.Contains(query) || x.PrimaryEmailAddress == query);
                if (!result.Any())
                {
                    return new StaticPagedList<SearchViewModel>(finalresult, pageNumber, pageSize, result.Count());
                }

                totalSearchedRecords = result.Count();

                foreach (var record in result)
                {
                    var objlinkedutilityaccounts = record.LinkedUtilityAccounts;

                    foreach (var _item in objlinkedutilityaccounts)
                    {
                        var item = new SearchViewModel()
                        {
                            Status = _item?.WssAccount != null ? _unitOfWork.WssAccountStatusRepository.FindAll().FirstOrDefault(y => y.WssAccountStatusCode == _item.WssAccount.WssAccountStatusCode)?.WssAccountStatusDesc ?? "" : "Not Registered",
                            EmailAddress = record.PrimaryEmailAddress,
                            UtilityAccountId = _item.UtilityAccountId ?? 0
                        };
                        var searchList = GetUtilityAccountDateByEmailAddress(item).ToList();
                        if (searchList.Any() == false)
                        {
                            searchList = new List<SearchViewModel>()
                            {
                                item
                            };
                        }
                        finalresult.AddRange(searchList);
                    }
                }
            }

            else
            {
                var result = _ubUnitofWork.UtilityAccountRepository.FindAll()
               .Where(x => x.UtilityAccountId > 0)
               .Where(
                   x =>
                       x.ccb_acct_id.Contains(query) || x.ccb_acct_id == query || x.PrimaryAccountHolderName == query ||
                       x.PrimaryAccountHolderName.Contains(query));

                if (!result.Any())
                {
                    return new StaticPagedList<SearchViewModel>(finalresult, pageNumber, pageSize, result.Count());
                }
                totalSearchedRecords = result.Count();

                finalresult.AddRange(result.Select(x => new SearchViewModel
                {
                    UtilityAccountId = x.UtilityAccountId,
                    MainCustomerName = x.PrimaryAccountHolderName,
                    ccbAccountNumber = x.ccb_acct_id
                })
                .ToList());

                var linkedAccountData = GetUtilityAccountData(finalresult.Select(x => (int?)x.UtilityAccountId).ToList());

                foreach (var searchViewModel in linkedAccountData)
                {
                    var linkedResult = finalresult.First(x => x.UtilityAccountId == searchViewModel.UtilityAccountId);

                    linkedResult.EmailAddress = searchViewModel.EmailAddress;
                    linkedResult.Status = searchViewModel.Status;
                }
            }

            if ((pageNumber - 1) * pageSize >= totalSearchedRecords)
                pageNumber = 1;

            Session["UtilityAccountListPageNumber"] = pageNumber;
            //Sorting and Direction
            var sort = sortBy + sortDirection;

            switch (sort)
            {
                case "AccountNumber":
                    finalresult = finalresult.OrderBy(x => x.ccbAccountNumber).ToList();
                    break;

                case "AccountNumberDesc":
                    finalresult = finalresult.OrderByDescending(x => x.ccbAccountNumber).ToList();
                    break;

                case "MainCustomerName":
                    finalresult = finalresult.OrderBy(x => x.MainCustomerName).ToList();
                    break;

                case "MainCustomerNameDesc":
                    finalresult = finalresult.OrderByDescending(x => x.MainCustomerName).ToList();
                    break;

                case "Status":
                    finalresult = finalresult.OrderBy(x => x.Status).ToList();
                    break;

                case "StatusDesc":
                    finalresult = finalresult.OrderByDescending(x => x.Status).ToList();
                    break;

                case "PrimaryEmailAddressDesc":
                    finalresult = finalresult.OrderByDescending(x => x.EmailAddress).ToList();
                    break;

                case "PrimaryEmailAddress":
                    finalresult = finalresult.OrderBy(x => x.EmailAddress).ToList();
                    break;

                default:
                    finalresult = finalresult.OrderBy(x => x.ccbAccountNumber).ToList();
                    break;
            }

            finalresult = finalresult.ToPagedList(pageNumber, pageSize).ToList();
            return new StaticPagedList<SearchViewModel>(finalresult, pageNumber, pageSize, totalSearchedRecords);
        }

        private List<SearchViewModel> GetUtilityAccountData(List<int?> items)
        {
            var wssRecords = _unitOfWork.LinkedUtilityAccountsRepository.FindAll().Where(x => items.Contains(x.UtilityAccountId)).ToList();
            return wssRecords.Select(x => new SearchViewModel
            {
                UtilityAccountId = (int)x.UtilityAccountId,
                EmailAddress = x?.WssAccount != null ? x.WssAccount.PrimaryEmailAddress : string.Empty,
                Status = x?.WssAccount != null ? _unitOfWork.WssAccountStatusRepository.FindAll().FirstOrDefault(y => y.WssAccountStatusCode == x.WssAccount.WssAccountStatusCode)?.WssAccountStatusDesc ?? "" : "Not Registered"
            }).ToList();
        }

        private IEnumerable<SearchViewModel> GetUtilityAccountDateByEmailAddress(SearchViewModel item)
        {
            var linkedUtilityAccounts = _ubUnitofWork.UtilityAccountRepository.FindAll().Where(x => x.UtilityAccountId == item.UtilityAccountId);
            var searchList = new List<SearchViewModel>();
            if (linkedUtilityAccounts.Any())
            {
                foreach (var _item in linkedUtilityAccounts)
                {
                    var listItem = new SearchViewModel()
                    {
                        Status = item.Status,
                        EmailAddress = item.EmailAddress,
                        UtilityAccountId = item.UtilityAccountId,
                        MainCustomerName = _item.PrimaryAccountHolderName,
                        ccbAccountNumber = _item.ccb_acct_id,
                    };
                    searchList.Add(listItem);
                }
            }
            return searchList;
        }

        private IPagedList<SearchViewModel> GetSearchList_old(string query, string sortBy, string sortDirection, int pageNumber, int pageSize)
        {
            var result = _ubUnitofWork.UtilityAccountRepository.FindAll().Where(x => x.UtilityAccountId > 0);

            if (!string.IsNullOrEmpty(query))
            {
                result = result.Where(x => x.ccb_acct_id.Contains(query) || x.ccb_acct_id == query || x.PrimaryAccountHolderName == query || x.PrimaryAccountHolderName.Contains(query));
            }

            if ((pageNumber - 1) * pageSize >= result.Count())
                pageNumber = 1;
            result = result.OrderBy(x => x.UtilityAccountId);

            var finalresult = new List<SearchViewModel>();

            if (!result.Any())
            {
                return new StaticPagedList<SearchViewModel>(finalresult, pageNumber, pageSize, result.Count());
            }

            var subset = result.ToPagedList(1, result.Count()).ToList();
            foreach (var record in subset)
            {
                //var wssRecord = _unitOfWork.WssAccountRepository.FindAll().FirstOrDefault(m => m.UtilityAccountId == record.UtilityAccountId);
                var wssRecord = _unitOfWork.LinkedUtilityAccountsRepository.FindAll().FirstOrDefault(x => x.UtilityAccountId == record.UtilityAccountId);
                var item = new SearchViewModel()
                {
                    UtilityAccountId = record.UtilityAccountId,
                    EmailAddress = wssRecord != null && wssRecord.WssAccount != null ? wssRecord.WssAccount.PrimaryEmailAddress : string.Empty,
                    MainCustomerName = record.PrimaryAccountHolderName,
                    ccbAccountNumber = record.ccb_acct_id,
                    Status = wssRecord != null && wssRecord.WssAccount != null ? wssRecord.WssAccount.WssAccountStatusCode : "Not Registered"
                };
                finalresult.Add(item);
            }

            //Sorting and Direction
            var sort = sortBy + sortDirection;
            switch (sort)
            {
                case "AccountNumber":
                    finalresult = finalresult.OrderBy(x => x.ccbAccountNumber).ToList();
                    break;

                case "AccountNumberDesc":
                    finalresult = finalresult.OrderByDescending(x => x.ccbAccountNumber).ToList();
                    break;

                case "MainCustomerName":
                    finalresult = finalresult.OrderBy(x => x.MainCustomerName).ToList();
                    break;

                case "MainCustomerNameDesc":
                    finalresult = finalresult.OrderByDescending(x => x.MainCustomerName).ToList();
                    break;

                case "Status":
                    finalresult = finalresult.OrderBy(x => x.Status).ToList();
                    break;

                case "StatusDesc":
                    finalresult = finalresult.OrderByDescending(x => x.Status).ToList();
                    break;
                case "PrimaryEmailAddressDesc":
                    finalresult = finalresult.OrderByDescending(x => x.EmailAddress).ToList();
                    break;
                case "PrimaryEmailAddress":
                    finalresult = finalresult.OrderBy(x => x.EmailAddress).ToList();
                    break;
                default:
                    finalresult = finalresult.OrderBy(x => x.ccbAccountNumber).ToList();
                    break;
            }

            if ((pageNumber - 1) * pageSize >= result.Count())
                pageNumber = 1;
            // result = result.OrderBy(x => x.UtilityAccountId);
            finalresult = finalresult.ToPagedList(pageNumber, pageSize).ToList();
            return new StaticPagedList<SearchViewModel>(finalresult, pageNumber, pageSize, result.Count());
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}