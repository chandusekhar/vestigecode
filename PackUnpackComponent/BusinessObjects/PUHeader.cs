using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackUnpackComponent.BusinessObjects
{
    [Serializable]
   public class PUHeader
    {
        #region Constants

        public const string PUVocher_SAVE = "usp_PUVocherSave";
        public const string PUVocher_SEARCH = "usp_PUVocherrSearch";

        //GridView definition XML path
        public const string GRIDVIEW_DEFINITION_XML_PATH = "\\App_Data\\GridViewDefinition_PUVoucherSearch.xml";

        #endregion Constants

        public String PUNo
        {
            get;
            set;
        }
        public int CompositeItemId
        {
            get;
            set;
        }
        public int BucketId
        { get; set; }
        public string PUDate
        { get; set; }

        public String DisplayPUDate
        {
            get
            {
                if ((PUDate != null) && (PUDate.Length > 0))
                {
                    return Convert.ToDateTime(PUDate).ToString(CoreComponent.Core.BusinessObjects.Common.DTP_DATE_FORMAT);
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

        public string ItemName
        { get; set; }
        public int Quantity
        {get;set;}

        public double DBQuantity
        {
            get
            {
                return Math.Round((double)Quantity, CoreComponent.Core.BusinessObjects.Common.DBQtyRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public double DisplayQuantity
        {
            get
            {
                return Math.Round((double)DBQuantity, CoreComponent.Core.BusinessObjects.Common.DisplayQtyRounding, MidpointRounding.AwayFromZero);
            }
            set
            {
                throw new NotImplementedException("This property can not be explicitly set");
            }
        }

        public string Remarks
        { get; set; }
        public int CreatedBy
        {get; set;}
        public int ModifiedBy
        {get; set;}
        public bool IsPackVoucher
        { get; set; }
        public string DisplayIsPackVoucher
        { get; set; }
        public string ItemCode
        { get; set; }
        public string ModifiedDate
        { get; set; }
        public int LocationId
        {get; set;}
        public int AvailablePack
        { get; set; }
        public List<PUDetail> DetailItem
        { get; set; }
     

    }
}
