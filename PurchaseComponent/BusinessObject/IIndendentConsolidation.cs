using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PurchaseComponent.BusinessObjects
{
    public interface IIndentConsolidation
    {
        string IndentNo { get; set; }
        int ConsolidationId { get; set; }
        int Status { get; set; }
        int Source { get; set; }
        int CreatedBy { get; set; }
        string CreatedDate { get; set; }
        int ModifiedBy { get; set; }
        string ModifiedDate { get; set; }
        int HeaderRecordTotal { get; set; }
        int DetailRecordTotal { get; set; }
        List<IndentConsolidationHeader> IndentConsolidationHeader { get; set; }
        bool Save(IIndentConsolidation  objIndentConsolidation, int intConsolidationStatus, ref int consoldationId, ref string errorMessage);
    }

    public interface IIndentConsolidationHeader
    {
        int HeaderId { get; set; }
        int ItemId { get; set; }
        int LineNo { get; set; }
        string IndentNo { get; set; }
        double ApprovedQty { get; set; }
        int ForLocationId { get; set; }
        int ConsolidationId { get; set; }
        List<IndentConsolidationDetail> IndentConsolidationDetail { get; set; }

    }

    public interface IIndentConsolidationDetail
    {
        int HeaderId { get; set; }
        int Status { get; set; }
        int DetailId { get; set; }
        int LineNo { get; set; }
        int RecordType { get; set; }
        double Quantity { get; set; }
        int VendorId { get; set; }
        int DeliveryLocationId { get; set; }
        int TransferFromLocationId { get; set; }
        bool IsFormC { get; set; }
    }
}
