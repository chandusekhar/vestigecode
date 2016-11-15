using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using PagedList;
using UtilityBilling.Data;
using WSS.InternalApplication.Models;
using WSS.Logging.Service;
using IUnitOfWork = WSS.Data.IUnitOfWork;
using WSS.InternalApplication.Helper;

namespace WSS.InternalApplication.Controllers
{
    public class DashboardController:Controller
    {
        private readonly IUnitOfWork _wssUnitOfWork;
        private readonly UtilityBilling.Data.IUnitOfWork _utilityUnitofWork;
        private readonly IMapper _mapper;

        private static readonly ILogger Logger = new Logger(typeof(DashboardController));

        public DashboardController(IUnitOfWork wssUnitOfWork, UtilityBilling.Data.IUnitOfWork utilityUnitofWork, IMapper mapper)
        {
            _wssUnitOfWork = wssUnitOfWork;
            _utilityUnitofWork = utilityUnitofWork;
            _mapper = mapper;
        }
        
         public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Index(string interceptCode,int id)
        {
            var document =
                _utilityUnitofWork.DocumentHeaderRepository.FindAll().FirstOrDefault(x => x.DocumentHeaderId == id);
            var allInterceptedDocuments =
                _utilityUnitofWork.DocumentHeaderRepository.FindAll().Where(x=>x.BillInterceptCode==interceptCode);
         
             var status = document?.DocumentStatusCode;
            var model = new DocumentListViewModel()
            {
                InterceptCode = document.BillInterceptCode,
                DocumentDate = document.DocumentIssueDate,
                Last10Days = document.DocumentIssueDate,
                Status = status,
                GreaterThan10Days = document.DocumentIssueDate.AddDays(10)
            };                       

            ViewBag.allDocuemntsStatus = GetAllDocuemnts(status, 1, "10");

            return View(model);
        }
        
        [HttpGet]
        public ActionResult AllHeldDocuments(string status)
        {                      

            ViewBag.Query = status;                                    

            var query =
               _utilityUnitofWork.DocumentHeaderRepository.FindAll()
                                 .Where(x => x.DocumentStatusCode == status)
                                 .ToList();

            // Test and handle no documents returned properly
            List<DocumentListViewModel> list;
            if (query.Any())
            {
                list = _mapper.Map<List<DocumentHeader>, List<DocumentListViewModel>>(query);
                list = GetDocumenStatusValues(list);
            }
            else
            {
                list = new List<DocumentListViewModel>();
            }
            var docuemntsData = GetAllDocuemnts(status, 1, "10");
            ViewBag.BillsQuery = status;           
            return PartialView("_allHeldDocuments",docuemntsData);
        }

        [HttpGet]
        public ActionResult WssHeldNoPrint(string routeType)
        {           
            return PartialView("_wssHeldNoPrint");
        }

        [HttpGet]
        public ActionResult AllWssHeldDocuments(string routeType, string status)
        {
          
            return PartialView("_allWssHeldDocuments");
        }

        public PartialViewResult GetDocumentsPartialViewResult(string status, string page, string rowsPerPage)
        {
            var pageSize = Convert.ToInt32(rowsPerPage);
            var pageNumber = Convert.ToInt32(page);
            var allHeldDocumentsList = GetAllDocuemnts(status, pageNumber, rowsPerPage);
            if ((pageNumber - 1) * pageSize >= allHeldDocumentsList.Count)
            {
                pageNumber = 1;
            }
            ViewBag.RowsPerPageSelectList =
                ViewDisplayHelper.GetRowCountSelectListItems(Convert.ToInt32(rowsPerPage));
            ViewBag.PageNumber = page;
            ViewBag.PageSize = rowsPerPage;
            return PartialView("_allHeldDocuments", allHeldDocumentsList);
        }
        public IPagedList<DocumentListViewModel> GetAllDocuemnts(string status, int? page,
           string rowsPerPage)
        {
            var listOfAllDocuemnts = _utilityUnitofWork.DocumentHeaderRepository.FindAll().Where(x => x.DocumentStatusCode == status);
            var records = new List<DocumentListViewModel>();
            
            foreach (var item in listOfAllDocuemnts)
            {
                var addressviewmodel = new DocumentListViewModel()
                {
                    InterceptCode = item.BillInterceptCode,                    
                    Last10Days = item.DocumentIssueDate,
                    GreaterThan10Days = item.DocumentIssueDate.AddDays(10)
                };
                records.Add(addressviewmodel);
            }
            return new PagedList<DocumentListViewModel>(records, page ?? 1,
               Convert.ToInt32(rowsPerPage));
        }
       
        private List<DocumentListViewModel> GetDocumenStatusValues(List<DocumentListViewModel> list)
        {
            //document type comes from view
            var lookupValuesForDocumentStatus =
                _utilityUnitofWork.DocumentStatusRepository.FindAll()
                .ToDictionary(x => x.DocumentStatusCode, x => x.DocumentStatusValue);

            if (lookupValuesForDocumentStatus.Any())
            {
                foreach (var item in list)
                {
                    item.Status = lookupValuesForDocumentStatus[item.DocumnetStatuscode];
                }
            }
            return list;
        }

    }
}