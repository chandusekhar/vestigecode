using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreComponent.BusinessObjects
{
    public class MenuItemFunction
    {
        private int m_functionId;

        public int FunctionId
        {
            get { return m_functionId; }
            set { m_functionId = value; }
        }
        private string m_functionCode;

        public string FunctionCode
        {
            get { return m_functionCode; }
            set { m_functionCode = value; }
        }
    }
}
