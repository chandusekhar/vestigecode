namespace Vinculum.Framework.DataTypes
{
    using System;
    using System.Data;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential), DebuggerStepThrough]
    public struct DBParameter
    {
        private string parameterName;
        private object parameterValue;
        private object parameterType;
        private int parameterSize;
        private ParameterDirection parameterDirection;
        private bool parameterNullable;
        public DBParameter(string paramName, object paramValue, object paramType)
        {
            this.parameterName = paramName;
            this.parameterValue = paramValue;
            this.parameterType = paramType;
            this.parameterDirection = ParameterDirection.Input;
            this.parameterSize = 0;
            this.parameterNullable = true;
        }

        public DBParameter(string paramName, object paramValue, object paramType, ParameterDirection paramDirection, int paramSize, bool paramNullable) : this(paramName, paramValue, paramType)
        {
            this.parameterDirection = paramDirection;
            this.parameterSize = paramSize;
            this.parameterNullable = paramNullable;
        }

        public DBParameter(string paramName, object paramValue, object paramType, ParameterDirection paramDirection, int paramSize) : this(paramName, paramValue, paramType, paramDirection, paramSize, true)
        {
        }

        public string Name
        {
            get
            {
                return this.parameterName;
            }
            set
            {
                this.parameterName = value;
            }
        }
        public object Value
        {
            get
            {
                return this.parameterValue;
            }
            set
            {
                this.parameterValue = value;
            }
        }
        public object Type
        {
            get
            {
                return this.parameterType;
            }
            set
            {
                this.parameterType = value;
            }
        }
        public ParameterDirection Direction
        {
            get
            {
                return this.parameterDirection;
            }
            set
            {
                this.parameterDirection = value;
            }
        }
        public int Size
        {
            get
            {
                return this.parameterSize;
            }
            set
            {
                this.parameterSize = value;
            }
        }
        public bool IsNullable
        {
            get
            {
                return this.parameterNullable;
            }
            set
            {
                this.parameterNullable = value;
            }
        }
        public override string ToString()
        {
            return string.Concat(new object[] { this.parameterName, " ", this.Type.ToString(), "(", this.parameterSize, ") ", this.parameterDirection.ToString(), " = ", this.parameterValue.ToString() });
        }
    }
}

