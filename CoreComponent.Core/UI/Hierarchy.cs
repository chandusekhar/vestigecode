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
    public partial class Hierarchy : Form
    {
        public Hierarchy()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            CoreComponent.Core.BusinessObjects.Common.CloseThisForm(this);
        }        
    }
}
