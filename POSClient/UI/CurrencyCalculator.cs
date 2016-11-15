using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CoreComponent.Core.BusinessObjects;
using POSClient.BusinessObjects;
using POSClient.UI.Controls;

namespace POSClient.UI
{
	public partial class CurrencyCalculator : BaseChildForm
	{
		private const string NUMERIC_ZERO = "0.00";
		private bool m_showOkButton;
		private double m_fixTAmt;

		#region C'tors

		public CurrencyCalculator (bool showOkButton)
			: this ()
		{
			if (showOkButton)
			{
				btnOk.Visible = true;
				btnOk.Enabled = false;
				isCurrTo.Enabled = false;
			}
			txtCurrTo.Text = Currency.BaseCurrency.CurrencyCode;
			txtCurrToVal.Text = NUMERIC_ZERO;
		}

		public CurrencyCalculator ()
		{
			InitializeComponent ();

			oskbCurrCalc.CurrentFocus = txtCurrFromVal;
			m_fixTAmt = 0;

			// Populate Currency
            isCurrFrom.LoadItems<Currency>(Currency.CurrencyList.FindAll(delegate(Currency c) { return c.CurrencyCode != string.Empty;/*Currency.BaseCurrency.CurrencyCode; */}));
			isCurrTo.LoadItems<Currency> (Currency.CurrencyList);
		}

		public CurrencyCalculator (bool showOkButton, string fromCurrencyCode)
			: this (showOkButton)
		{
			txtCurrFrom.Text = fromCurrencyCode;
			isCurrFrom.Select (fromCurrencyCode);
		}

		public CurrencyCalculator (bool showOkButton, decimal toAmount)
			: this (showOkButton)
		{
			txtCurrToVal.Text = toAmount.ToString ("0.00");
			m_fixTAmt = (double) toAmount;
			txtExRateToBase.Text = Currency.GetConversionRate (txtCurrTo.Text).ToString ("0.0000");
			isCurrFrom.Select (0);
		}

		public CurrencyCalculator (string currencyFrom, string currencyTo, decimal fromAmount, decimal toAmount, decimal fromExchangeRate, decimal toExchangeRate)
			: this ()
		{
			txtCurrFrom.Text = currencyFrom;
			txtCurrTo.Text = currencyTo;
			txtCurrFromVal.Text = fromAmount.ToString ("0.00");
			txtCurrToVal.Text = toAmount.ToString ("0.00");
			txtExRateToBase.Text = fromExchangeRate.ToString ("0.00");
			txtExRateFromBase.Text = toExchangeRate.ToString ("0.00");
			isCurrFrom.Enabled = false;
			isCurrTo.Enabled = false;
			btnOk.Visible = true;
			txtCurrFromVal.SelectAll ();
		}

		#endregion

		#region Properties

		public string CurrencyFrom
		{
			get { return txtCurrFrom.Text; }
			set { txtCurrFrom.Text = value; }
		}

		public string CurrencyTo
		{
			get { return txtCurrTo.Text; }
			set { txtCurrTo.Text = value; }
		}

		public decimal CurrencyFromAmount
		{
			get { return Math.Round (Convert.ToDecimal ((txtCurrFromVal.Text.Trim () == string.Empty ? "0" : txtCurrFromVal.Text.Trim ())), Common.DisplayAmountRounding); }
			set { txtCurrFromVal.Text = value.ToString (); }
		}

		public decimal CurrencyToAmount
		{
			get { return Math.Round (Convert.ToDecimal (txtCurrToVal.Text.Trim () == string.Empty ? "0" : txtCurrToVal.Text.Trim ()), 2); }
			set { txtCurrToVal.Text = value.ToString (); }
		}

		public decimal CurrencyFromExchangeRate
		{
			get { return Math.Round (Convert.ToDecimal (txtExRateFromBase.Text.Trim () == string.Empty ? "0" : txtExRateFromBase.Text.Trim ()), 2); }
			set { txtExRateFromBase.Text = value.ToString (); }
		}

		public decimal CurrencyToExchangeRate
		{
			get { return Math.Round (Convert.ToDecimal (txtExRateToBase.Text.Trim () == string.Empty ? "0" : txtExRateToBase.Text.Trim ()), 2); }
			set { txtExRateToBase.Text = value.ToString (); }
		}

		public bool ShowOkButton
		{
			get { return m_showOkButton; }
			set { m_showOkButton = value; }
		}

		#endregion

		#region Event Handlers

		private void Currency_ItemSelected (object sender, SelectableItem i)
		{
			double dCurrFAmt = 0, dCurrFRate = 0, dCurrTAmt = 0, dCurrTRate = 0;

			switch (((ItemSelector) sender).Name)
			{
				case "isCurrFrom":
					btnOk.Enabled = true;
					txtCurrFrom.Text = i.DisplayText;

					dCurrFRate = Currency.GetConversionRate (txtCurrFrom.Text);
					txtExRateFromBase.Text = dCurrFRate.ToString ("0.0000");

					dCurrTAmt = (m_fixTAmt > 0 ? m_fixTAmt : (double.TryParse (txtCurrToVal.Text, out dCurrTAmt) ? dCurrTAmt : 0));
					if (txtCurrTo.Text.Trim ().Length > 0)
						dCurrTRate = Currency.GetConversionRate (txtCurrTo.Text);
					txtCurrFromVal.Text = (dCurrTAmt * dCurrTRate / dCurrFRate).ToString ("0.00");
					break;

				case "isCurrTo":
					txtCurrTo.Text = i.DisplayText;
					if (txtCurrFrom.Text.Length > 0)
					{
						dCurrFRate = Currency.GetConversionRate (txtCurrFrom.Text);
						dCurrFAmt = (double.TryParse (txtCurrFromVal.Text, out dCurrFAmt) ? dCurrFAmt : 0);
						dCurrTRate = Currency.GetConversionRate (txtCurrTo.Text);

						txtCurrToVal.Text = (dCurrFAmt * dCurrFRate / dCurrTRate).ToString ("0.00");
						txtExRateToBase.Text = dCurrTRate.ToString ("0.0000");
					}
					else
						txtCurrToVal.Text = NUMERIC_ZERO;
					txtExRateToBase.Text = Currency.GetConversionRate (txtCurrTo.Text).ToString ("0.0000");
					break;
			}
			ValidateAmount ();
			txtCurrFromVal.SelectAll ();
			//txtCurrFromVal.Focus();
		}

		private void DialogButton_Click (object sender, EventArgs e)
		{
			switch (((Button) sender).Name)
			{
				case "btnCancel":
					DialogResult = DialogResult.Cancel;
					this.Close ();
					break;

				case "btnOk":

					try
					{
						if (txtCurrFromVal.Text.Trim ().Length == 0 || Convert.ToDecimal (txtCurrFromVal.Text) == 0)
							txtCurrFromVal.Focus ();
						else
						{
							ValidateAmount ();
							DialogResult = DialogResult.OK;
							this.Close ();
						}
					}
					catch { }
					break;
			}
		}

		private void TextBox_Validated (object sender, EventArgs e)
		{
			ValidateAmount ();
			txtCurrFromVal.Focus ();
		}

		private void TextBox_KeyDown (object sender, KeyEventArgs e)
		{
			switch (((TextBox) sender).Name)
			{
				case "txtCurrFromVal":
					TextBox_Validated (txtCurrFromVal, new EventArgs ());
					break;
			}
		}

		#endregion

		#region Methods

		private void ValidateAmount ()
		{
			double dCurrFAmt = 0, dCurrFRate = 0, dCurrTRate = 0;

			if (txtCurrFromVal.Text.Trim ().Length == 0)
			{
				txtCurrFromVal.Text = NUMERIC_ZERO;
				txtCurrFromVal.SelectAll ();
			}

			if (txtCurrFrom.Text.Length > 0 && txtCurrTo.Text.Length > 0)
			{
				dCurrFRate = Currency.GetConversionRate (txtCurrFrom.Text);
				dCurrFAmt = (double.TryParse (txtCurrFromVal.Text, out dCurrFAmt) ? dCurrFAmt : 0);
				dCurrTRate = Currency.GetConversionRate (txtCurrTo.Text);

				txtCurrToVal.Text = (dCurrFAmt * dCurrFRate / dCurrTRate).ToString ("0.00");
				txtExRateToBase.Text = dCurrTRate.ToString ("0.0000");
			}
		}

		#endregion
	}
}

