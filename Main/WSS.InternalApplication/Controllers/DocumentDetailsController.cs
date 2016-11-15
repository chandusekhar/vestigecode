
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using WSS.InternalApplication.Authorization;
using WSS.InternalApplication.CustomAttributes;
using WSS.InternalApplication.Models;
using IUnitOfWork = WSS.Data.IUnitOfWork;
using Roles = WSS.InternalApplication.Authorization.Roles;

namespace WSS.InternalApplication.Controllers
{

    [Authorize]
   //[CanExecuteFunction(Roles = Roles.IntrcGA, FunctionCode = Functions.GeneralAdmin)]
    public class DocumentDetailsController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly UtilityBilling.Data.IUnitOfWork _ubUnitofWork;       

        public DocumentDetailsController(IUnitOfWork unitOfWork, UtilityBilling.Data.IUnitOfWork ubUnitofWork)
        {
            _unitOfWork = unitOfWork;
            _ubUnitofWork = ubUnitofWork;
        }

        //GET: DocumentDetails
        //[CanExecuteFunction(FunctionCode = Functions.GeneralAdmin, Roles = Roles.IntrcGA)]
        //[CanExecuteFunction(FunctionCode = Functions.GeneralAdmin_RemoveDocs, Roles = Roles.IntrcWithRemove)]
        [HttpGet]
        public ActionResult Index(int id)
        {
            ViewBag.DocumentId = id;
            var documentDetail =
                _ubUnitofWork.DocumentHeaderRepository.FindAll().FirstOrDefault(dd => dd.DocumentHeaderId == id);
            if (documentDetail == null)
            {
                return View();
            }
            var utilityAccount = _ubUnitofWork.UtilityAccountRepository.FindAll()
                .FirstOrDefault(m => m.UtilityAccountId == documentDetail.UtilityAccountId);

            var detailViewModel = new DocumentListViewModel()
            {
                DocumentHeaderId = documentDetail.DocumentHeaderId,
                MainCustomerName = utilityAccount?.PrimaryAccountHolderName,
                InterceptCode = documentDetail.BillInterceptCode,
                DocumentDate = documentDetail.DocumentIssueDate,
                ccbAccountNumber = utilityAccount?.ccb_acct_id
            };
            var lookupValuesForDocumentStatus = GetStatuLookupDictionary();
            detailViewModel.Status = lookupValuesForDocumentStatus.ContainsKey(documentDetail.DocumentStatusCode) ? lookupValuesForDocumentStatus[documentDetail.DocumentStatusCode] : String.Empty;
            return View(detailViewModel);
        }

        [HttpGet]
        public ActionResult GetDocumentDetailFile(int id)
        {            
            var documentDetail =
                _ubUnitofWork.DocumentDetailRepository.FindAll().FirstOrDefault(dd => dd.DocumentHeaderId == id);

            if (documentDetail?.DocumentPdf != null && documentDetail.DocumentPdf.Length > 0)
            {
                var memStream = new MemoryStream(documentDetail.DocumentPdf);
                return new FileStreamResult(memStream, "application/pdf");
            }
            else
            {
                var message = "We're sorry, but the document you selected could not be found on the server.";
                return Json(new
                {
                    Message = message,
                    documentDetail?.DocumentHeaderId,
                    documentDetail?.DocumentDetailId
                }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// //When user clicks the Action button in the Document Details page, button available will be based on the status of the Document and user permissions.
        /// Updates status in the Utitly billing database based on the Document status
        /// </summary>
        [HttpPost]
        public JsonResult UpdateStatus(int id, string updatedstatus)
        {
            var lookupValuesForDocumentStatus = GetStatuLookupDictionary();
            var statusKey = lookupValuesForDocumentStatus.FirstOrDefault(x => x.Value == updatedstatus).Key;
            var documentDetail =
                _ubUnitofWork.DocumentHeaderRepository.FindAll().FirstOrDefault(dd => dd.DocumentHeaderId == id);
            
            if (documentDetail == null)
            {
                return Json(new { IsUpdated = false, message = "" },
                       JsonRequestBehavior.AllowGet);
            }
            var currentStatus = lookupValuesForDocumentStatus[documentDetail.DocumentStatusCode];
            if (!IsAllowed(currentStatus,updatedstatus))
            {
                return Json(new { IsUpdated = false, message = "" },
                       JsonRequestBehavior.AllowGet);
            }

            documentDetail.DocumentStatusCode = statusKey;
            _ubUnitofWork.Save("0");
            return Json(new { IsUpdated = true, message = "" },
                    JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public Dictionary<string, string> GetStatuLookupDictionary()
        {
            //document type comes from reference LookUp
            var lookupValuesForDocumentStatus =
                _ubUnitofWork.DocumentStatusRepository.FindAll()
                    .ToDictionary(x => x.DocumentStatusCode, x => x.DocumentStatusValue);
            return lookupValuesForDocumentStatus;
        }

        private bool IsAllowed(string currentStatus, string updatedStatus)
        {
            if (currentStatus == "Held")
            {
                return updatedStatus == "Rejected";
            }
            if (currentStatus == "Published" || currentStatus == "Rejected")
            {
                return updatedStatus == "Held";
            }
            if (currentStatus == "Released")
            {
                return updatedStatus == "Removed";
            }
            return false;
        }


    }
}