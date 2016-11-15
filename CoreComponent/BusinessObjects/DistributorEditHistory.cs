using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
using CoreComponent.Core.BusinessObjects;

namespace CoreComponent.BusinessObjects
{
   public class DistributorEditHistory
    {
        private int distributorId;
        private string fieldName="";
        private string oldValue="";
        private string newValue="";
        private DateTime logDate = DateTime.Now;
        private string remarks;
        public const string SP_DISTRIBUTOR_REGISTER_LOG = "USP_DistributorInfoLogSave";
        public int DistributorId
        {
            set 
            {
                distributorId = value;
            }
            get 
            {
                return distributorId;
            }
        }
        public string FieldName
        {
            set{ 
                    fieldName = value; 
               }
            get {
                    return fieldName;
                }
        
        }
        public string OldValue
        {
            set { oldValue = value; }
            get { return oldValue; }
        }
        public string NewValue
        {
            set { newValue = value; }
            get { return newValue; }
        }

        public DateTime  LogDate
        {
            set { logDate = value; }
            get { return logDate; }
        }
        
        public string Remarks
        {
            set { remarks = value; }
            get { return remarks; }
        }
        
    }
}
