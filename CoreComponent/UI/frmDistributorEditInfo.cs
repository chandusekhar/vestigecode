using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreComponent.Core.BusinessObjects;

using CoreComponent.BusinessObjects;

namespace CoreComponent.UI
{
    public partial class frmDistributorEditInfo : Form
    {
        public frmDistributorEditInfo()
        {
            InitializeComponent();
        }

        private void cmdExportToExcel_Click(object sender, EventArgs e)
        {
            string errormsg = "";
            bool isvalidate = CoreComponent.BusinessObjects.Reports.Distributor.ValidateDistributorHistory(ref errormsg, txtDistID.Text, dtpFromDate.Value.ToString() , dtpToDate.Value.ToString() );
            if (isvalidate == false)
                return;
            ExportToExcel objExport = new ExportToExcel();
            objExport.DistributorID = txtDistID.Text ;
            objExport.FromDate = dtpFromDate.Value  ;
            objExport.ToDate = dtpToDate.Value  ;
            objExport.getExcel();
        }

        private void frmDistributorEditInfo_Load(object sender, EventArgs e)
        {
            dtpFromDate.Format = DateTimePickerFormat.Custom;
            dtpFromDate.CustomFormat = Common.DTP_DATE_FORMAT;
            dtpToDate.Format = DateTimePickerFormat.Custom;
            dtpToDate.CustomFormat = Common.DTP_DATE_FORMAT;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
