using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace POSClient.UI.Controls
{
	public enum OnScreenKeyBoardMode
	{
		Alphanumeric,
		Numeric
	}

	public partial class OnScreenKeyBoard : UserControl
	{
		#region Member Variables

		private bool m_capsOn;
		private OnScreenKeyBoardMode m_displayMode;
		private Color m_selectedControlColor, m_dataControlColor, m_actionControlColor;
		private Image m_dataControlBg, m_actionControlBg;
		private TextBoxBase m_focusTextBox;

		#endregion

		#region Constructors

		public OnScreenKeyBoard()
		{
			InitializeComponent();

			m_capsOn = false;
			m_dataControlBg = m_actionControlBg = null;
			DataControlColor = Color.Transparent;
			ActionControlColor = Color.Transparent;
			SelectedControlColor = Color.Orange;
			DisplayMode = OnScreenKeyBoardMode.Numeric;
		}

		#endregion

		#region Properties

		[Category("Appearance"), Description("The background color of data button."), DefaultValue(typeof(Color), "Transparent")]
		public Color DataControlColor
		{
			get { return m_dataControlColor; }
			set
			{
				m_dataControlColor = value;

				this.SuspendLayout();
				btn_0.BackColor = btn_00.BackColor = btn_1.BackColor = btn_2.BackColor = btn_3.BackColor = btn_4.BackColor = btn_5.BackColor = btn_6.BackColor = btn_7.BackColor = btn_8.BackColor = btn_9.BackColor = btn_dot.BackColor = m_dataControlColor;
				btn_a.BackColor = btn_b.BackColor = btn_c.BackColor = btn_d.BackColor = btn_e.BackColor = btn_f.BackColor = btn_g.BackColor = btn_h.BackColor = btn_i.BackColor = btn_j.BackColor = btn_k.BackColor = btn_l.BackColor = btn_m.BackColor = btn_n.BackColor = btn_o.BackColor = btn_p.BackColor = btn_q.BackColor = btn_r.BackColor = btn_s.BackColor = btn_t.BackColor = btn_u.BackColor = btn_v.BackColor = btn_w.BackColor = btn_x.BackColor = btn_y.BackColor = btn_z.BackColor = m_dataControlColor;
				btn_space.BackColor = btn_bracesleft.BackColor = btn_bracesright.BackColor = m_dataControlColor;
				this.ResumeLayout(true);
			}
		}

		[Category("Appearance"), Description("The background image of data button.")]
		public Image DataControlBackgroundImage
		{
			get { return m_dataControlBg; }
			set
			{
				m_dataControlBg = value;

				this.SuspendLayout();
				btn_0.BackgroundImage = btn_00.BackgroundImage = btn_1.BackgroundImage = btn_2.BackgroundImage = btn_3.BackgroundImage = btn_4.BackgroundImage = btn_5.BackgroundImage = btn_6.BackgroundImage = btn_7.BackgroundImage = btn_8.BackgroundImage = btn_9.BackgroundImage = btn_dot.BackgroundImage = m_dataControlBg;
				btn_a.BackgroundImage = btn_b.BackgroundImage = btn_c.BackgroundImage = btn_d.BackgroundImage = btn_e.BackgroundImage = btn_f.BackgroundImage = btn_g.BackgroundImage = btn_h.BackgroundImage = btn_i.BackgroundImage = btn_j.BackgroundImage = btn_k.BackgroundImage = btn_l.BackgroundImage = btn_m.BackgroundImage = btn_n.BackgroundImage = btn_o.BackgroundImage = btn_p.BackgroundImage = btn_q.BackgroundImage = btn_r.BackgroundImage = btn_s.BackgroundImage = btn_t.BackgroundImage = btn_u.BackgroundImage = btn_v.BackgroundImage = btn_w.BackgroundImage = btn_x.BackgroundImage = btn_y.BackgroundImage = btn_z.BackgroundImage = m_dataControlBg;
				btn_space.BackgroundImage = btn_bracesleft.BackgroundImage = btn_bracesright.BackgroundImage = m_dataControlBg;
				this.ResumeLayout(true);
			}
		}

		[Category("Appearance"), Description("The background color of action button."), DefaultValue(typeof(Color), "Transparent")]
		public Color ActionControlColor
		{
			get { return m_actionControlColor; }
			set
			{
				m_actionControlColor = value;

				this.SuspendLayout();
				btn_backspace.BackColor = btn_clear.BackColor = btn_enter.BackColor = btn_return.BackColor = m_actionControlColor;
				btn_capslock.BackColor = m_capsOn ? m_selectedControlColor : m_actionControlColor;
				this.ResumeLayout(true);
			}
		}

		[Category("Appearance"), Description("The background image of action button.")]
		public Image ActionControlBackgroundImage
		{
			get { return m_actionControlBg; }
			set
			{
				m_actionControlBg = value;

				this.SuspendLayout();
				btn_capslock.BackgroundImage = btn_backspace.BackgroundImage = btn_clear.BackgroundImage = btn_enter.BackgroundImage = btn_return.BackgroundImage = m_actionControlBg;
				this.ResumeLayout(true);
			}
		}

		[Category("Appearance"), Description("The background color of selected button."), DefaultValue(typeof(Color), "Orange")]
		public Color SelectedControlColor
		{
			get { return m_selectedControlColor; }
			set
			{
				m_selectedControlColor = value;

				this.SuspendLayout();
				btn_capslock.BackColor = m_capsOn ? m_selectedControlColor : m_actionControlColor;
				this.ResumeLayout(true);
			}
		}

		[Category("Behavior"), Description("The display mode of onscreenkeyboard."), DefaultValue(typeof(OnScreenKeyBoardMode), "Numeric")]
		public OnScreenKeyBoardMode DisplayMode
		{
			get { return m_displayMode; }
			set
			{
				m_displayMode = value;

				this.SuspendLayout();
				tlpAlphaPad.Visible = (m_displayMode == OnScreenKeyBoardMode.Alphanumeric);
				this.Width = (m_displayMode == OnScreenKeyBoardMode.Alphanumeric ? tlpNumPad.Width + tlpAlphaPad.Width : tlpNumPad.Width);
				this.ResumeLayout(true);
			}
		}

		public TextBoxBase CurrentFocus
		{
			get { return m_focusTextBox; }
			set { m_focusTextBox = value; }
		}

		#endregion

		private void OSKB_Click(object sender, EventArgs e)
		{
			if (m_focusTextBox == null) return;

			string sendertag = ((Button)sender).Tag.ToString(), tstr = m_focusTextBox.Text;

			switch (sendertag)
			{
				case "ENTER":
					m_focusTextBox.Focus();
					SendKeys.Send("{TAB}");
					break;

				case "CLEAR":
					m_focusTextBox.Text = string.Empty;
					break;

				case "BACKSPACE":
					m_focusTextBox.Text = tstr.Length > 0 ? tstr.Substring(0, tstr.Length - 1) : tstr;
					break;

				case "CAPSLOCK":
					m_capsOn = !m_capsOn;
					this.SuspendLayout();
					btn_capslock.BackColor = m_capsOn ? SelectedControlColor : ActionControlColor;
					for (int i = 97; i <= 122; i++)
						tlpAlphaPad.Controls["btn_" + (char)i].Text = ((char)(i - (m_capsOn ? 32 : 0))).ToString();
					btn_bracesleft.Text = ((char)(m_capsOn ? 123 : 91)).ToString();
					btn_bracesright.Text = ((char)(m_capsOn ? 125 : 93)).ToString();
					this.ResumeLayout(true);
					break;

				default:
					if (m_focusTextBox.SelectionLength == m_focusTextBox.Text.Length) tstr = string.Empty;
					if(tstr.Length < m_focusTextBox.MaxLength)
						m_focusTextBox.Text = tstr + ((Button)sender).Text;
					break;
			}
		}
	}
}
