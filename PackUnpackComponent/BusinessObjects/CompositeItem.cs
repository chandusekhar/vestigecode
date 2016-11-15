using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackUnpackComponent.BusinessObjects
{
 [Serializable]
 public class CompositeItem
    {

     public int ItemId
        {
            get;
            set;
        }
     public string ItemName
        { get; set; }
     public string Shortname
        { get; set; }
     public string ItemCode
        { get; set; }
     private Boolean m_IsPackVoucher = true;
     public Boolean IsPackVoucher
     {
         get
         {
             return m_IsPackVoucher;
         }
         set
         {
             m_IsPackVoucher = value;
         }
     }
     public int PackUnpackQty
     { get; set; }

     public double DBPackUnpackQty
     {
         get
         {
             return Math.Round((double)PackUnpackQty, CoreComponent.Core.BusinessObjects.Common.DBQtyRounding, MidpointRounding.AwayFromZero);
         }
         set
         {
             throw new NotImplementedException("This property can not be explicitly set");
         }
     }

     public double DisplayPackUnpackQty
     {
         get
         {
             return Math.Round((double)DBPackUnpackQty, CoreComponent.Core.BusinessObjects.Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
         }
         set
         {
             throw new NotImplementedException("This property can not be explicitly set");
         }
     }

     public double MRP
     { get; set;}

     public double DBMRP
     {
         get
         {
             return Math.Round((double)MRP, CoreComponent.Core.BusinessObjects.Common.DBAmountRounding, MidpointRounding.AwayFromZero);
         }
         set
         {
             throw new NotImplementedException("This property can not be explicitly set");
         }
     }

     public double DisplayMRP
     {
         get
         {
             return Math.Round((double)DBMRP, CoreComponent.Core.BusinessObjects.Common.DisplayAmountRounding, MidpointRounding.AwayFromZero);
         }
         set
         {
             throw new NotImplementedException("This property can not be explicitly set");
         }
     }

     public string MfgDate
     { get; set; }

     public String DisplayMfgDate
     {
         get
         {
             if ((MfgDate != null) && (MfgDate.Length > 0))
             {
                 return Convert.ToDateTime(MfgDate).ToString(CoreComponent.Core.BusinessObjects.Common.DTP_DATE_FORMAT);
             }
             else
             {
                 return String.Empty;
             }
         }
         set
         {
             throw new NotImplementedException("This property can not be explicitly set");
         }
     }

     public string MfgNo
     { get; set; }
     public string ExpDate
     { get; set; }

     public String DisplayExpDate
     {
         get
         {
             if ((ExpDate != null) && (ExpDate.Length > 0))
             {
                 return Convert.ToDateTime(ExpDate).ToString(CoreComponent.Core.BusinessObjects.Common.DTP_DATE_FORMAT);
             }
             else
             {
                 return String.Empty;
             }
         }
         set
         {
             throw new NotImplementedException("This property can not be explicitly set");
         }
     }

     public string BatchNo
     { get; set; }
     public List<BatchDetails> ListBatchDetails
     { get; set; }

     public List<CompositeBOM> CompositeDetail
        {
            get;
            set;
        }
    }
}
