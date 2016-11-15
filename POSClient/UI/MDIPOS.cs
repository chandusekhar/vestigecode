using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreComponent.Core.BusinessObjects;
using System.Reflection;
using CoreComponent.Core.UI;
using CoreComponent.UI;

namespace POSClient.UI
{
    public partial class MDIPOS : Form
    {
        private MainScreen ms;
        public enum ScreenMode
        {
            FirstVisit=1,
            LoggedIn=2,
            LoggedOut=3,
            DayBeginDone = 4,
            DayEndDone = 5
        }
        public MDIPOS()
        {
            InitializeComponent();
            SetScreenMode(ScreenMode.FirstVisit);
            ms = new MainScreen(this);
            ms.MdiParent = this;
            ms.Show();
        }

        private void SetScreenMode(ScreenMode screenMode)
        {
            switch ((int)screenMode)
            {
                case (int)ScreenMode.FirstVisit:
                    //tsbDayBegin.Enabled = false;
                    tsbOrderHistory.Enabled = false;
                    tsddbExtraFunc.Enabled = false;
                    break;
                case (int) ScreenMode.LoggedIn:
                    //tsbDayBegin.Enabled = true;
                    tsbOrderHistory.Enabled = true;
                    tsddbExtraFunc.Enabled = true;
                    tsbSign.Text = "Sign Out";
                    break;
                case (int) ScreenMode.DayBeginDone:
                    break;
                case (int) ScreenMode.DayEndDone:
                    break;
                case (int) ScreenMode.LoggedOut:
                    tsbOrderHistory.Enabled = false;
                    tsddbExtraFunc.Enabled = false;
                    break;
            }
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsbNewOrder_Click(object sender, EventArgs e)
        {
        }

        private void tsbSign_Click(object sender, EventArgs e)
        {
            if (tsbSign.Tag.ToString() == "SignIn")
            {
                Login lgn = new Login();
                lgn.SignIn += new AuthenticationComponent.UI.SignInHandler(lgn_SignIn);
                lgn.ShowDialog();
            }
            else
            {
                bool canSignout = false;
                if (ms.HoldOrders.Count > 0)
                {
                    DialogResult = MessageBox.Show(Common.GetMessage("VAL0508"), Common.GetMessage("10004"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (DialogResult == DialogResult.Yes)
                    {
                        canSignout = true;
                    }
                    else
                    {
                        canSignout = false;
                    }
                }
                else
                {
                    canSignout = true;
                }
                if (canSignout)
                {
                    ms.CurrentOrder = null;
                    ms.HoldOrders.RemoveRange(0, ms.HoldOrders.Count);
                    tsbSign.Text = "Sign In";
                    tsbSign.Tag = "SignIn";
                    ms.SetScreenState(Common.ScreenMode.LoggedOut);
                    SetScreenMode(ScreenMode.LoggedOut);
                }
            }
        }

        void lgn_SignIn(object sender, AuthenticationComponent.UI.SignInArgs e)
        {
            if (e.IsSuccess)
            {
                tsbSign.Text = "Sign Out";
                tsbSign.Tag = "SignOut";
                SetScreenMode(ScreenMode.LoggedIn);
                ms.SetScreenState(Common.ScreenMode.LoggedIn);

                (sender as POSClient.UI.Login).SignIn -= new AuthenticationComponent.UI.SignInHandler(lgn_SignIn);
                sender = null;
            }
        }

        private void tsbOrderHistory_Click(object sender, EventArgs e)
        {
            try
            {
                frmOrderSearch orderSearchForm = new frmOrderSearch();
                DialogResult dr;
                dr = orderSearchForm.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    if (orderSearchForm.ReturnObject != null)
                    {
                      
                        Common.ScreenMode mode = Common.ScreenMode.LoggedIn;
                        switch (orderSearchForm.ReturnObject.Status)
                        {
                            case 1:
                                {
                                    mode = Common.ScreenMode.OrderSaved;
                                    break;
                                }
                            case 2:
                                {
                                    mode = Common.ScreenMode.OrderCancelled;
                                    break;
                                }
                            case 3:
                                {
                                    mode = Common.ScreenMode.OrderConfirmed;
                                    break;
                                }
                            case 4:
                                {
                                    mode = Common.ScreenMode.Invoiced;
                                    break;
                                }
                            case 5:
                                {
                                    mode = Common.ScreenMode.InvoiceCancelled;
                                    break;
                                }
                            default:
                                break;
                        }
                        
                        
                        ms.LoadOrder(orderSearchForm.ReturnObject);
                        ms.SetScreenState(mode);
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                Common.LogException(ex);
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("10004"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExtraFunc_Click(object sender, EventArgs e)
        {
            DialogResult dr;

            switch (((ToolStripMenuItem)sender).Name)
            {
                case "tsmiReports":
                    using (CoreComponent.UI.POSReportViewer rs = new CoreComponent.UI.POSReportViewer())
                        rs.ShowDialog(this);
                    break;
            }
        }

        private void MDIPOS_Load(object sender, EventArgs e)
        {
            if (Common.CurrentLocationTypeId == (int)Common.LocationConfigId.BO)
            {
                tsmiQuery.Enabled = true;
                tsmiPUCDeposit.Enabled = true;
            }
            else
            {
                tsmiQuery.Enabled = false;
                tsmiPUCDeposit.Enabled = false;
            }
            this.Text += "  [" + (Common.LocationConfigId)Common.CurrentLocationTypeId + " : " + Common.LocationCode + "]  " + "[v" + Common.Version + "]";         //Application name
        }

        
        private void tsmiQuery_Click(object sender, EventArgs e)
        {
            DialogResult dr;

            switch (((ToolStripMenuItem)sender).Name)
            {
                case "tsmiQuery":
                    using (frmDistributorSearchModule rs = new frmDistributorSearchModule())
                    {
                        rs.IsPos = true;
                        rs.ShowDialog(this);
                    }
                    break;
            }
        }

        private void tsmiPUCDeposit_Click(object sender, EventArgs e)
        {
            DialogResult dr;

            switch (((ToolStripMenuItem)sender).Name)
            {
                case "tsmiPUCDeposit":
                    using (POSClient.UI.frmPickUpCentre rs = new POSClient.UI.frmPickUpCentre())
                        rs.ShowDialog(this);
                    break;
            }
        }
    }
}
