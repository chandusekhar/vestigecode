using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreComponent.BusinessObjects
{
    [Serializable]
    public class Functions
    {
        # region variables

        private int m_recordid;

        public int Recordid
        {
            get { return m_recordid; }
            set { m_recordid = value; }
        }
        private int m_functionid;

        public int Functionid
        {
            get { return m_functionid; }
            set { m_functionid = value; }
        }
        private string m_functioncode;

        public string Functioncode
        {
            get { return m_functioncode; }
            set { m_functioncode = value; }
        }
        private int m_status;

        public int Status
        {
            get { return m_status; }
            set { m_status = value; }
        }

        # endregion

        


    }
}
