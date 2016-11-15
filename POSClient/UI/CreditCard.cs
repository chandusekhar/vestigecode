using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using POSClient.BusinessObjects;
using CoreComponent.Core.BusinessObjects;

namespace POSClient.UI
{
    public partial class CreditCard : BaseChildForm
    {

		private const string ASTERIX = "Astx";
		private const string CAPTION = "Cap";
		private const string DATA = "Val";

		#region C'tors

        public CreditCard(decimal amount, string cardType, string cardNumber, string cardHolderName, string expiryDate)
        {
            InitializeComponent();

			foreach (int field in Enum.GetValues(typeof(Common.CreditCardInformation)))
				(this.Controls.Find (ASTERIX + field.ToString (), true)[0] as Label).Visible = ((m_MandatoryCreditCardInformation & field) == field);

            pnlSwipe.Visible = true;
            pnlManual.Visible = false;
            m_amount = amount;
            m_cardType = cardType;
            m_cardNumber = cardNumber;
            m_cardHolderName = cardHolderName;
            m_expiryDate = expiryDate;
            oskbCreditCard.CurrentFocus = txtAmount;
			//btnOk.Enabled = false;
        }

        public CreditCard(decimal amount, string cardType)
            :this(amount, cardType, string.Empty, string.Empty, string.Empty)
        {
        }

        public CreditCard(): this(0, string.Empty, string.Empty, string.Empty, string.Empty)
        {
			txtAmount.Enabled = false;
			oskbCreditCard.Enabled = false;
        }

        #endregion

        #region Member Variables

        private decimal m_amount;
        private string m_cardType;
        private string m_cardNumber;
        private string m_cardHolderName;
        private string m_expiryDate;
		private int m_MandatoryCreditCardInformation = 15;
        
        #endregion

        #region Properties

        public string ExpiryDate
        {
            get { return m_expiryDate; }
            set { m_expiryDate = value; }
        }

        public decimal Amount
        {
            get
            {
                return m_amount;
            }
            set
            {
                m_amount = value;
            }
        }

        public string CardType
        {
            get
            {
                return m_cardType;
            }
            set
            {
                m_cardType = value;
            }
        }

        public string CardNumber
        {
            get
            {
                return m_cardNumber;
            }
            set
            {
                m_cardNumber = value;
            }
        }

        public string CardHolderName
        {
            get
            {
                return m_cardHolderName;
            }
            set
            {
                m_cardHolderName = value;
            }
        }

        #endregion

        private void btnManual_Click(object sender, EventArgs e)
        {
			this.CancelButton = btnCancel;
            ManualButtonClick(true); 
        }

        private void ManualButtonClick(bool loadValues)
        {
            if (loadValues)
            {
                txtAmount.Text = m_amount.ToString();
                val1.Text = m_cardType;
                val2.Text = m_cardHolderName;
                val4.Text = m_cardNumber;
                val8.Text = m_expiryDate;
                val1.ReadOnly = true;
                if (!string.IsNullOrEmpty(m_cardType))
                {
                    btnOk.Enabled = true;
                    switch ((Common.CreditCardType)Enum.Parse(typeof(Common.CreditCardType), m_cardType))
                    {
                        case Common.CreditCardType.Amex:
                            pbLogo.Image = Properties.Resources.Amex;
                            break;
                        case Common.CreditCardType.MasterCard:
                            pbLogo.Image = Properties.Resources.Mastercard;
                            break;
                        case Common.CreditCardType.Visa:
                            pbLogo.Image = Properties.Resources.Visa;
                            break;
                        default:
                            pbLogo.Image = Properties.Resources.Unknown;
                            break;
                    }
                
                }
            }
            pnlSwipe.Visible = false;
            pnlManual.Visible = true;
			textBox1.Text = string.Empty;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            textBox1.Text = string.Empty;
            //this.Close();
        }

		private void btnOk_Click(object sender, EventArgs e)
		{
			bool isValid = true;

            foreach (int field in Enum.GetValues(typeof(Common.CreditCardInformation)))
			{
				if ((m_MandatoryCreditCardInformation & field) == field)
				{
					Control ctrl = this.Controls.Find(DATA + field.ToString(), true)[0];
					string controlName = ctrl.Name;
					switch (ctrl.GetType().Name)
					{
						case "TextBox":
							if ((ctrl as TextBox).Text.Trim().Length == 0)
							{
								isValid = false;
								(ctrl as TextBox).Focus();
							}
							else if (controlName == "val4")
							{
								isValid = ValidateCardNumber((ctrl as TextBox).Text.Trim());
							}
							else if (controlName == "val8")
							{
								isValid = ValidateDate((ctrl as TextBox).Text.Trim());
							}
							if (!isValid)
							{
								ctrl.Focus();
							}
							break;
					}
					if (!isValid)
						break;
				}
			}
			if (txtAmount.Text == string.Empty)
			{
				isValid = false;
			}
			if (isValid)
			{
				DialogResult = DialogResult.OK;
				m_cardType = val1.Text;
				m_cardHolderName = val2.Text;
				m_cardNumber = val4.Text;
				m_expiryDate = val8.Text;
				m_amount = Convert.ToDecimal(txtAmount.Text.Trim());
				val1.ReadOnly = true;
				this.Close();
			}
		}

		private bool ValidateCardNumber(string cardNumber)
		{
            Common.CreditCardType cardType = POSPayments.GetCardType(cardNumber);
            m_cardNumber = cardNumber;
           
			if (POSPayments.ValidateCreditCard(cardType, cardNumber))
			{
                //m_cardNumber = cardNumber;
                val1.Text = m_cardType = cardType.ToString();
				switch (cardType)
				{
                    case Common.CreditCardType.Amex:
						pbLogo.Image = Properties.Resources.Amex;
						break;
                    case Common.CreditCardType.MasterCard:
						pbLogo.Image = Properties.Resources.Mastercard;
						break;
                    case Common.CreditCardType.Visa:
						pbLogo.Image = Properties.Resources.Visa;
						break;
					default:
						pbLogo.Image = Properties.Resources.Unknown;
						break;
				}
				return true;
			}
			else
			{
                pbLogo.Image = Properties.Resources.Unknown;
                val1.Text = m_cardType = "Unknown";
                return true;
			}
		}

        private void textBox_Enter(object sender, EventArgs e)
        {
            oskbCreditCard.CurrentFocus = (TextBox) sender;
        }

        private void CreditCardInformation_Paint(object sender, PaintEventArgs e)
        {
            textBox1.Focus();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string[] arr = textBox1.Text.Trim().Split('^');
                if (arr.Length < 3)
                {
                    textBox1.Text = string.Empty;
                    MessageBox.Show("Please swipe again");
                    textBox1.Focus();
                }
                else
                {
                    m_cardNumber = arr[0].Substring(2);
                    m_cardHolderName = arr[1].Trim().Split('/')[0];
					//m_expiryDate = arr[2].Trim().Substring(0, 4).Insert(2, "/");
					m_expiryDate = arr[2].Trim().Substring(2, 2) + "/" + arr[2].Trim().Substring(0, 2);
                    m_cardType = POSPayments.GetCardType(m_cardNumber).ToString();
                    MakeFieldsReadOnly();
                    ManualButtonClick(true);
                }
            }
        }

        private void MakeFieldsReadOnly()
        {
            val1.ReadOnly = true;
			//val4.ReadOnly = true;
			//txtAmount.ReadOnly = true;
			//val2.ReadOnly = true;
			//val8.ReadOnly = true;
        }

        private void FillValues()
        {
            val1.Text = m_cardType;
            txtAmount.Text = m_amount.ToString("0.00");
            val4.Text = m_cardNumber;
            val2.Text = m_cardHolderName;
            val8.Text = m_expiryDate;
        }

        private bool ValidateDate(string date)
        {
			bool isvalid = true;
			if (date.Contains ("/") == false)
				return false;

			string[] dateParts = date.Split("/".ToCharArray()[0]);
			int mm, yy;
			if (Int32.TryParse(dateParts[0], out mm) && Int32.TryParse(dateParts[1], out yy))
			{
				if (mm > 12 || mm < 1)
				{
					isvalid = false;
				}
				else if (yy < Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2)) || yy > Convert.ToInt32(DateTime.Now.AddYears(10).Year.ToString().Substring(2)))
				{
					isvalid = false;
				}
				else if (yy == Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2)) && mm < DateTime.Now.Month)
				{
					return false;
				}
			}
			else
			{
				isvalid = false;
			}
			return isvalid;
        }

		private void val4_TextChanged(object sender, EventArgs e)
		{
			ValidateCardNumber((sender as TextBox).Text.Trim());
		}

		private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
		{
			double val;
			if (!Double.TryParse((sender as TextBox).Text.Trim() + e.KeyChar, out val))
				e.Handled = true;
		}
      }
}