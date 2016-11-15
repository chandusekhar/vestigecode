using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CoreComponent.UI
{
    public partial class FTPProcess : Form
    {
        private string strMsg;

        public string StrMsg
        {
            get { return strMsg; }
            set { lblMsg.Text = value; }
        }

        private bool _strStatus;
        public bool strStatus
        {
            get { return _strStatus; }
            set { progressBar1.Visible = value; }
        }


        public FTPProcess()
        {
            InitializeComponent();
        }
    }
}
