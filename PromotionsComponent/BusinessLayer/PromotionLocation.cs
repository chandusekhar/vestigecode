using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionsComponent.BusinessLayer
{
    public class PromotionLocation
    {
        public PromotionLocation()
        {
        }

        public PromotionLocation(int locationId, int promotionId, string locationVal, string locationCode, string locationType, int lineNumber, int statusId, string statusVal)
        {
            m_LocationId = locationId;
            m_PromotionId = promotionId; 
            m_LocationVal = locationVal;
            m_LocationCode = locationCode;
            m_LocationType = locationType;
            m_LineNo = lineNumber;
            m_StatusId = statusId;
            m_StatusVal = statusVal; 
        }

        private int m_LocationId;
        public  int LocationId
        {
            get { return m_LocationId; }
            set { m_LocationId = value; }
        }

        private int m_PromotionId;
        public int PromotionId
        {
            get
            {
                return m_PromotionId;
            }
            set { m_PromotionId= value; }
        }

        private string m_LocationVal;
        public  string LocationVal
        {
            get { return m_LocationVal; }
            set{m_LocationVal =value;}
        }

        private string m_LocationCode;
        public string LocationCode
        {
            get { return m_LocationCode; }
            set {m_LocationCode=value;}
        }

        private string m_LocationType;
        public string LocationType
        {
            get { return m_LocationType; }
            set { m_LocationType = value; }
        }

        private int m_LineNo;
        public int LineNo
        {
            get { return m_LineNo; }
            set { m_LineNo = value; }
        }

        private int m_StatusId;
        public int StatusId
        {
            get { return m_StatusId; }
            set { m_StatusId = value; }
        }

        private string m_StatusVal;
        public string StatusVal
        {
            get { 
                    return m_StatusVal; 
                }
            set { m_StatusVal = value; }
        }

        public void UpdateLocation(PromotionLocation location, int locationId,int promotionId, string locationVal, string locationCode, string locationType, int lineNumber, int statusId, string statusVal)
        {
            location.LocationId   = locationId;
            location.PromotionId = promotionId;  
            location.LocationVal   = locationVal;
            location.LocationCode   = locationCode;
            location.LocationType   = locationType;
            location.m_LineNo = lineNumber;
            location.StatusId   = statusId;
            location.StatusVal = statusVal;
        }
    }
}
