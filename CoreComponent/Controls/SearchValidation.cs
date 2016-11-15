using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreComponent.Controls
{
    public class SearchValidation
    {
        private string m_parameter1;

        public string Parameter1
        {
            get { return m_parameter1; }
            set { m_parameter1 = value; }
        }
        private string m_parameter2;

        public string Parameter2
        {
            get { return m_parameter2; }
            set { m_parameter2 = value; }
        }
        private string m_comparison;

        public string Comparison
        {
            get { return m_comparison; }
            set { m_comparison = value; }
        }
    }
}
