using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using UtilityBilling.Data;
using WSS.Email.Service;
using WSS.InternalApplication.Helper;
using WSS.InternalApplication.Models;
using WSS.Logging.Service;

namespace WSS.InternalApplication.Controllers
{
    public class DocumentSearchController : BaseController
    {
        private readonly WSS.Data.IUnitOfWork _wssUnitOfWork;
        private readonly UtilityBilling.Data.IUnitOfWork _ubUnitofWork;
        private readonly IMapper _mapper;

        private static readonly ILogger Logger = new Logger(typeof(DocumentSearchController));

        public DocumentSearchController(WSS.Data.IUnitOfWork wssUnitOfWork, UtilityBilling.Data.IUnitOfWork ubUnitofWork, IMapper mapper, ISendEmail emailService) : base(emailService)
        {
            _wssUnitOfWork = wssUnitOfWork;
            _ubUnitofWork = ubUnitofWork;
            _mapper = mapper;
        }

        // GET: DocumentSearch
        public ActionResult Index()
        {
            ViewBag.InterceptCodes = new System.Collections.Generic.Dictionary<string, string> {
                {"allintercept","--All Intercept Codes--" },
                { "alldocs","All Documents"},
                {"nonintercept","Non Intercepted" },
                {"other","Others" }
            };

            ViewBag.RouteTypeCodes =
                _ubUnitofWork.DocumentHeaderRepository.FindAll().Select(x => x.RouteTypeCode).Distinct();

            ViewBag.DocumentStatuses = _ubUnitofWork.DocumentStatusRepository.FindAll();
            return View();
        }

        [HttpPost]
        public PartialViewResult PerformSearch(DocumentSearchViewModel model)
        {
            var documentHeaderQuery = _ubUnitofWork.DocumentHeaderRepository.FindAll();
            var utilityAccountQuery = _ubUnitofWork.UtilityAccountRepository.FindAll();
            var documentStatusQuery = _ubUnitofWork.DocumentStatusRepository.FindAll();

            var documentSearchQuery = utilityAccountQuery.Join(
                documentHeaderQuery,
                ua => ua.UtilityAccountId,
                dh => dh.UtilityAccountId,
                (ua, dh) => new 
                {
                    InterceptCode = dh.BillInterceptCode,
                    RouteTypeCode = dh.RouteTypeCode,
                    AccountNumber = ua.ccb_acct_id,
                    DateIssued = dh.DocumentIssueDate,
                    CustomerName = ua.PrimaryAccountHolderName,
                    BillId = dh.DocumentHeaderId,
                    StatusCode = dh.DocumentStatusCode
                }
            ).Join(
                documentStatusQuery,
                left => left.StatusCode,
                right => right.DocumentStatusCode,
                (left, right) => new DocumentSearchResultViewModel
                {
                    InterceptCode = left.InterceptCode,
                    RouteTypeCode = left.RouteTypeCode,
                    AccountNumber = left.AccountNumber,
                    DateIssued = left.DateIssued,
                    CustomerName = left.CustomerName,
                    BillId = left.BillId,
                    StatusCode = right.DocumentStatusCode,
                    StatusDescription = right.DocumentStatusValue
                }
            );

            if (!string.IsNullOrEmpty(model.InterceptCode))
            {
               
                switch (model.InterceptCode)
                {

                    case "nonintercept":
                        documentSearchQuery = documentSearchQuery.Where(x => x.InterceptCode == "nonintercept" && x.InterceptCode != "" && x.InterceptCode != null);
                        break;
                    case "other":
                        documentSearchQuery = documentSearchQuery.Where(x => x.InterceptCode== "other" && x.InterceptCode != "" && x.InterceptCode != null);
                        break;
                    case "allintercept":
                        documentSearchQuery = documentSearchQuery.Where(x=>x.InterceptCode!="" && x.InterceptCode != null);
                        break;
                }
            }

            if (!string.IsNullOrEmpty(model.StatusCode))
            {
                documentSearchQuery = documentSearchQuery.Where(x => x.StatusCode == model.StatusCode);
            }

            if (!string.IsNullOrEmpty(model.RouteTypeCode))
            {
                documentSearchQuery = documentSearchQuery.Where(x => x.RouteTypeCode == model.RouteTypeCode);
            }

            if (!string.IsNullOrEmpty(model.AccountNumber))
            {
                documentSearchQuery = documentSearchQuery.Where(x => x.AccountNumber.Contains(model.AccountNumber));
            }

            if (!string.IsNullOrEmpty(model.CustomerName))
            {
                documentSearchQuery = documentSearchQuery.Where(x => x.CustomerName.Contains(model.CustomerName));
            }

            if (null != model.DocumentId)
            {
                documentSearchQuery = documentSearchQuery.Where(x => x.BillId == model.DocumentId);
            }

            if (null != model.StartDate)
            {
                documentSearchQuery = documentSearchQuery.Where(x => x.DateIssued >= model.StartDate.Value);
            }

            if (null != model.EndDate)
            {
                documentSearchQuery = documentSearchQuery.Where(x => x.DateIssued <= model.EndDate.Value);
            }

            documentSearchQuery = documentSearchQuery.OrderByDescending(x => x.DateIssued);

            model.SearchResults = documentSearchQuery.ToList();

            return PartialView("_DocumentSearchResults", model.SearchResults);
        }
    }
}