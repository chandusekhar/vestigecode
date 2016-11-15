using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Excel ;
using Vinculum.Framework.Data;
using Vinculum.Framework;
using Vinculum.Framework.DataTypes;
using CoreComponent.Core.BusinessObjects;

namespace CoreComponent.BusinessObjects
{
   public class ExportToExcel
    {
        public string DistributorID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        private string Sp_DistributorEditHistory = "usp_rptDistributorPanBankHistory";

        public  void getExcel()
        {
            CreateWorkbook(GetDistributorInfo());
        }
        private  System.Data.DataTable  GetDistributorInfo()
        {
            DBParameterList dbParam;
            String errorMessage = "";
            DataSet ds = new DataSet();
            System.Data.DataTable dt;
            using (DataTaskManager dtManager = new DataTaskManager())
            {

                dbParam = new DBParameterList();
                dbParam.Add(new DBParameter("@distributorid", DistributorID, DbType.String));
                dbParam.Add(new DBParameter("@fromDate", FromDate, DbType.DateTime));
                dbParam.Add(new DBParameter("@toDate", ToDate, DbType.DateTime));
                dbParam.Add(new DBParameter(Common.PARAM_OUTPUT, errorMessage, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                dt = dtManager.ExecuteDataTable(Sp_DistributorEditHistory, dbParam);
                //ds.Tables.Add(dt);    
                errorMessage = dbParam[Common.PARAM_OUTPUT].Value.ToString();
                {
                    if (errorMessage.Length > 0)
                    {
                        //isSuccess = false;

                    }
                    else
                    {
                        //isSuccess = true;
                    }
                }
            }

            return dt;
        
        }
        public void CreateWorkbook(System.Data.DataTable  dt)
          {
                ApplicationClass excel = new ApplicationClass();
                excel.Application.Workbooks.Add(true);

                int counterIndex = 0;
                foreach (System.Data.DataColumn col in  dt.Columns)
                {
                    counterIndex++;
                    excel.Cells[1, counterIndex] = col.ColumnName;
                }
                Range r = excel.get_Range(excel.Cells[1, 1], excel.Cells[1, counterIndex]);
                r.Interior.Color = 240;
                r.Cells.Font.Bold = true;


                int rowIndex = 0;
                foreach (System.Data.DataRow row in dt.Rows)
                {
                    rowIndex++;
                    counterIndex = 0;
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        counterIndex++;
                        Range range = excel.get_Range(excel.Cells[1, 8], excel.Cells[rowIndex + 1, 9]);
                        range.NumberFormat = "@";
                        excel.Cells[rowIndex + 1, counterIndex] = row[i].ToString();
                    }
                    
                }
                excel.Visible = true;
                Worksheet worksheet = (Worksheet)excel.ActiveSheet;
                worksheet.Activate();
            }
        }
    }

