using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReturnsComponent.BusinessObjects
{
    [Serializable]
    public class BatchDetail
    {
        public int ItemId
        { get; set; }
        public string BatchNo
        { get; set; }
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

        public string ManufactureBatchNo
        { get; set; }
        public double MRP
        { get; set; }


        public BatchDetail()
        {

        }
    }
}
