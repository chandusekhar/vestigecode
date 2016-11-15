using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
using CoreComponent.Core.BusinessObjects;
using CoreComponent.BusinessObjects;

namespace CoreComponent.UI
{
    [Serializable]
    public partial class frmDistributorHistory : Form
    {
        #region ClassVaribale
        private DataTable m_dtHistoryData;
        int m_iCounter;
        private delegate void delNavigateData();
        private Dictionary<string, delNavigateData> m_objDic;
        ToolTip m_objToolTip;
        #endregion

        #region Constructor

        public frmDistributorHistory()
        {
            InitializeComponent();
            m_objToolTip = new ToolTip();
            m_objToolTip.SetToolTip(btnExit, "Exit");
            m_objToolTip.SetToolTip(btnFirstRecord, "Go to First Record");
            m_objToolTip.SetToolTip(btnLastRecord, "Go to Last Record");
            m_objToolTip.SetToolTip(btnBackWrd, "Go to Previous Record");
            m_objToolTip.SetToolTip(btnForwrd, "Go to Next Record");
            m_objDic = new Dictionary<string, delNavigateData>();
            m_objDic.Add("first", FirstRecord);
            m_objDic.Add("previous", PreviousRecord);
            m_objDic.Add("next", NextRecord);
            m_objDic.Add("last", LastRecord);
            m_objDic.Add("exit", Exit);
        }

        #endregion

        #region Properties
        public string DistributorId
        {
            get;
            set;
        }
        #endregion
                
        #region Event
        private void frmDistributorHistory_Load(object sender, EventArgs e)
        {
           Distributor objDistributor = null;
            try
            {
                objDistributor = new Distributor();
                m_dtHistoryData = objDistributor.GetDistributorHistory(DistributorId);
                m_iCounter = m_dtHistoryData.Rows.Count-1;
                NavigateData(m_iCounter);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
            finally
            {
                objDistributor = null;
            }
        }

        private void DataNavigation(object sender, EventArgs e)
        {
            Button objbtn;
            try
            {
                objbtn = (Button)sender;
                if (objbtn != null && m_objDic.ContainsKey(objbtn.Tag.ToString()))
                {
                    m_objDic[objbtn.Tag.ToString()]();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(Common.GetMessage("10002"), Common.GetMessage("30007"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                Common.LogException(ex);
            }
        }
        #endregion Event


        #region Private Method
        private void NavigateData(int iIndex)
        {
            try
            {
                if (m_dtHistoryData != null && m_dtHistoryData.Rows.Count >0)
                {
                    txtDistributorTitle.Text = m_dtHistoryData.Rows[iIndex]["distributortitle"].ToString();
                    txtCoTitle.Text =  m_dtHistoryData.Rows[iIndex]["co_distributortitle"].ToString();
                    txtFName.Text = m_dtHistoryData.Rows[iIndex]["distributorfirstname"].ToString();
                    txtCoDFname.Text = m_dtHistoryData.Rows[iIndex]["co_distributorfirstname"].ToString();
                    txtLastName.Text = m_dtHistoryData.Rows[iIndex]["distributorlastname"].ToString();
                    txtCoLastName.Text = m_dtHistoryData.Rows[iIndex]["co_distributorlastname"].ToString();
                    txtDob.Text = m_dtHistoryData.Rows[iIndex]["distributordob"].ToString();
                    txtCoDob.Text = m_dtHistoryData.Rows[iIndex]["co_distributordob"].ToString();
                    txtEnrolledNo.Text = m_dtHistoryData.Rows[iIndex]["enrolledNo"].ToString();
                    txtSerial.Text = m_dtHistoryData.Rows[iIndex]["serialno"].ToString();
                    txtUplineARN.Text = m_dtHistoryData.Rows[iIndex]["uplinedistributorid"].ToString();
                    if ((Common.IsMiniBranchLocation != 1) && (!Common.CheckIfDistributorAddHidden(Convert.ToInt32(DistributorId))))
                    {
                        txtAddress1.Text = m_dtHistoryData.Rows[iIndex]["distributoraddress1"].ToString();
                        txtAddress2.Text = m_dtHistoryData.Rows[iIndex]["distributoraddress2"].ToString();
                        txtAddress3.Text = m_dtHistoryData.Rows[iIndex]["distributoraddress3"].ToString();
                    }

                    txtCity.Text = m_dtHistoryData.Rows[iIndex]["cityname"].ToString();
                    txtState.Text = m_dtHistoryData.Rows[iIndex]["statename"].ToString();
                    txtPin.Text = m_dtHistoryData.Rows[iIndex]["distributorpincode"].ToString();
                    txtMobile.Text = m_dtHistoryData.Rows[iIndex]["distributormobnumber"].ToString();
                    txtPhone.Text = m_dtHistoryData.Rows[iIndex]["distributortelenumber"].ToString();
                    txtEmail1.Text = m_dtHistoryData.Rows[iIndex]["distributoremailid"].ToString();
                    txtPanNo.Text = m_dtHistoryData.Rows[iIndex]["distributorpannumber"].ToString();
                    txtRemarks.Text = m_dtHistoryData.Rows[iIndex]["remarks"].ToString();
                    txtSavedBy.Text = m_dtHistoryData.Rows[iIndex]["username"].ToString();
                    txtSaveOn.Text = m_dtHistoryData.Rows[iIndex]["modifieddate"].ToString();
                    txtZone.Text = m_dtHistoryData.Rows[iIndex]["zonename"].ToString();
                    txtBank.Text = m_dtHistoryData.Rows[iIndex]["bankname"].ToString();
                }
            }
            finally
            {
            }
        }

        #region PreviousRecord
        /// <summary>
        /// this is used to navigate the previous record
        /// </summary>
        private void PreviousRecord()
        {
            if (m_dtHistoryData.Rows.Count == 0)
                return;
            if (m_iCounter == 0)
            {
               // m_iCounter = m_dtHistoryData.Rows.Count - 1;\
                
                //MessageBox.Show("You are already on the First Record");
                MessageBox.Show(string.Format(Common.GetMessage("VAL0061"), "First Record"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
                m_iCounter--;
            NavigateData(m_iCounter);
        }
        #endregion

        #region NextRecord
        /// <summary>
        /// this is used to navigate the next record.
        /// </summary>
        private void NextRecord()
        {
            if (m_dtHistoryData.Rows.Count == 0)
                return;
            if (m_iCounter == m_dtHistoryData.Rows.Count - 1)
            {
               // m_iCounter = 0;
                //MessageBox.Show("You are already on the Last Record");
                MessageBox.Show(string.Format(Common.GetMessage("VAL0061"),"Last Record"), Common.GetMessage("10001"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
                m_iCounter++;
            NavigateData(m_iCounter);

        }
        #endregion

        #region LastRecord
        /// <summary>
        /// this is used to navigate the last record.
        /// </summary>
        private void LastRecord()
        {
            m_iCounter = m_dtHistoryData.Rows.Count - 1;
            NavigateData(m_iCounter);
        }
        #endregion

        #region FirstRecord
        /// <summary>
        /// this is used to navigate the firstrecord.
        /// </summary>
        private void FirstRecord()
        {
            m_iCounter = 0;
            NavigateData(m_iCounter);
        }
        #endregion

        #region Exit
        private void Exit()
        {
            this.Close();
        }

        #endregion
        #endregion
    }
}
