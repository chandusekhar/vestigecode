using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreComponent.Controls
{
    public class SearchParam
    {
        private bool m_isEnabled;

        public bool IsEnabled
        {
            get { return m_isEnabled; }
            set { m_isEnabled = value; }
        }

        private bool m_isReadOnly;

        public bool IsReadOnly
        {
            get { return m_isReadOnly; }
            set { m_isReadOnly = value; }
        }
        private bool m_isVisible;

        public bool IsVisible
        {
            get { return m_isVisible; }
            set { m_isVisible = value; }
        }

        private string m_parameterType;

        public string ParameterType
        {
            get { return m_parameterType; }
            set { m_parameterType = value; }
        }
        private string m_name;

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }
        private string m_defaultValue;

        public string DefaultValue
        {
            get { return m_defaultValue; }
            set { m_defaultValue = value; }
        }
        private int m_controlWidth;

        public int ControlWidth
        {
            get { return m_controlWidth; }
            set { m_controlWidth = value; }
        }
        private int m_controlRow;

        public int ControlRow
        {
            get { return m_controlRow; }
            set { m_controlRow = value; }
        }
        private int m_controlColumn;

        public int ControlColumn
        {
            get { return m_controlColumn; }
            set { m_controlColumn = value; }
        }
        private string m_dataType;

        public string DataType
        {
            get { return m_dataType; }
            set { m_dataType = value; }
        }
        private string m_source;

        public string Source
        {
            get { return m_source; }
            set { m_source = value; }
        }
        private string m_label;

        public string Label
        {
            get { return m_label; }
            set { m_label = value; }
        }
        private int m_maxLength;

        public int MaxLength
        {
            get { return m_maxLength; }
            set { m_maxLength = value; }
        }
        private bool m_isMandatory;

        public bool IsMandatory
        {
            get { return m_isMandatory; }
            set { m_isMandatory = value; }
        }
        private string m_propertyName;

        public string PropertyName
        {
            get { return m_propertyName; }
            set { m_propertyName = value; }
        }
        private string m_assemblyName;

        public string AssemblyName
        {
            get { return m_assemblyName; }
            set { m_assemblyName = value; }
        }
        private string m_className;

        public string ClassName
        {
            get { return m_className; }
            set { m_className = value; }
        }
        private string m_methodName;

        public string MethodName
        {
            get { return m_methodName; }
            set { m_methodName = value; }
        }

        private string m_parameterCode;

        public string ParameterCode
        {
            get { return m_parameterCode; }
            set { m_parameterCode = value; }
        }
        private int m_key1;

        public int Key1
        {
            get { return m_key1; }
            set { m_key1 = value; }
        }
        private int m_key2;

        public int Key2
        {
            get { return m_key2; }
            set { m_key2 = value; }
        }
        private int m_key3;

        public int Key3
        {
            get { return m_key3; }
            set { m_key3 = value; }
        }
    }
}
