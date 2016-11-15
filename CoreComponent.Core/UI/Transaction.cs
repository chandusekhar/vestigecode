using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CoreComponent.Core.UI
{
    /// <summary>
    /// Base class for Transaction
    /// </summary>
    public partial class Transaction : Form
    {
        public Transaction()
        {
            InitializeComponent();
            tabControlTransaction.Visible = true;
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            //ImageList list=new ImageList();
            //Image img=Image.FromFile("Resources\button.png");
            //list.Images.Add("tabImage",img);


        }

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;
        //        return cp;
        //    }
        //}

        private void Transaction_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            CoreComponent.Core.BusinessObjects.Common.CloseThisForm(this);
        }

        //private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        //{

        //}
    }


    //public class TabControlEx : TabControl
    //{
    //    /// </summary>
    //    [
    //    Description("Gets or sets a value indicating whether the tab headers should be drawn"),
    //    DefaultValue(true)
    //    ]
    //    private bool m_showheader = true;
    //    public bool ShowTabHeaders 
    //    {
    //        get{return m_showheader;}
    //        set { m_showheader = value; }
    //    }

    //    public TabControlEx(): base()
    //    {
    //        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
    //    }

    //    protected override void WndProc(ref Message m)
    //    {
    //        if (!ShowTabHeaders && m.Msg == 0x1328 && !DesignMode)
    //            m.Result = (IntPtr)1;
    //        else
    //            base.WndProc(ref m);
    //    }

    //}
}