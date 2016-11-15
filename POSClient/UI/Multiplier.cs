using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace POSClient.UI
{
	public partial class Multiplier : POSClient.UI.BaseChildForm
	{
		public Multiplier()
		{
			InitializeComponent();
			oskNumPad.CurrentFocus = txtQuantity;
		}

		private int m_quantity = 1;

		public int Quantity
		{
			get { return m_quantity; }
			set { m_quantity = value; }
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			int result = 1;
			if (Int32.TryParse(txtQuantity.Text.Trim(), out result))
			{
				m_quantity = result;
				DialogResult = DialogResult.OK;
				this.Close();
			}
			else
			{
				txtQuantity.Focus();
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void Quantity_KeyPress(object sender, KeyPressEventArgs e)
		{
			int asck = (int)e.KeyChar;
			e.Handled = ((asck < 48 || asck > 57) && asck != 8);
		}
	}
}


