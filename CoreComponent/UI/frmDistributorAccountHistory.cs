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
    public partial class frmDistributorAccountHistory : Form
    {
        #region ClassVaribale
        private DataTable m_dtHistoryData;
        int m_iCounter;
        private delegate void delNavigateData();
        private Dictionary<string, delNavigateData> m_objDic;
        ToolTip m_objToolTip;
        public Int64 batchNo;
        #endregion

        #region Constructor

        public frmDistributorAccountHistory()
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
        private void frmDistributorAccountHistory_Load(object sender, EventArgs e)
        {
            DistributorAccountDeatils objDistributor = null;
            try
            {
                objDistributor = new DistributorAccountDeatils();
                m_dtHistoryData = objDistributor.GetDistributorAccountHistory(DistributorId);
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
                    txtDistributorId.Text = m_dtHistoryData.Rows[iIndex]["DistributorId"].ToString();
                    txtDistributorFName.Text = m_dtHistoryData.Rows[iIndex]["DistributorFirstName"].ToString();
                    txtDistributorLName.Text = m_dtHistoryData.Rows[iIndex]["DistributorLastName"].ToString();
                    txtPANNo.Text = m_dtHistoryData.Rows[iIndex]["DistributorPANNumber"].ToString() == "-1" ? "" : m_dtHistoryData.Rows[iIndex]["DistributorPANNumber"].ToString();
                    txtBankName.Text = m_dtHistoryData.Rows[iIndex]["BankName"].ToString();
                    txtDistributorBankBranch.Text = m_dtHistoryData.Rows[iIndex]["DistributorBankBranch"].ToString() == "-1" ? "" : m_dtHistoryData.Rows[iIndex]["DistributorBankBranch"].ToString();
                    txtBankAccountNo.Text = m_dtHistoryData.Rows[iIndex]["DistributorBankAccNumber"].ToString() == "-1" ? "" : m_dtHistoryData.Rows[iIndex]["DistributorBankAccNumber"].ToString();
                    txtSavedBy.Text = m_dtHistoryData.Rows[iIndex]["UserName"].ToString();
                    txtSaveOn.Text = m_dtHistoryData.Rows[iIndex]["ModifiedDate"].ToString();
                    batchNo =Convert.ToInt64( m_dtHistoryData.Rows[iIndex]["batch"].ToString()); 
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

        private void lblDistributorId_Click(object sender, EventArgs e)
        {

        }

        #endregion

        private void grpAddress_Enter(object sender, EventArgs e)
        {

        }

        private void btnPANDetails_Click(object sender, EventArgs e)
        {
            if (txtDistributorId.Text  != null)
            {
                frmDistributorPANBankNew objfrmDistPanBank = new frmDistributorPANBankNew(txtDistributorId.Text , "P", txtSaveOn.Text,"H",txtPANNo.Text,batchNo  );
                objfrmDistPanBank.ShowDialog();
            }
        }

        private void btnBankDetail_Click(object sender, EventArgs e)
        {
            if (txtDistributorId.Text != null)
            {
                frmDistributorPANBankNew objfrmDistPanBank = new frmDistributorPANBankNew(txtDistributorId.Text, "B", txtSaveOn.Text,"H",txtBankAccountNo.Text,batchNo );
                objfrmDistPanBank.ShowDialog();
            }
        }
    }
}
