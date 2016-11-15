using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data;

namespace CoreComponent.Core.BusinessObjects
{
    public static class Validators
    {
        /*
        ------------------------------------------------------------------------
        Created by			    :	Harsh Gupta
        Created Date		    :	22 - June - 2009
        Purpose				    :	Contains Common Error Traping/Casting/Conversion
         * methods. It also contains methods for setting and resetting error messages
         * for ErrorProvider control.
        Modified by			    :
        Date of Modification    :
        Purpose of Modification	:
        ------------------------------------------------------------------------    
        */
        public static bool CheckForEmptyString(int length)
        {
            if (length <= 0)
            {
                return true;
            }
            return false;
        }

        public static bool CheckForCheckState(CheckState checkState)
        {
            if (checkState == CheckState.Indeterminate)
            {
                return true;
            }
            return false;
        }

        public static bool IsBool(string value)
        {
            if (CheckForEmptyString(value.Length))
                return false;
            System.Boolean boolVal = false;
            return System.Boolean.TryParse(value, out boolVal);
        }

        public static bool IsInt32(string value)
        {
            if (CheckForEmptyString(value.Length))
                return false;
            System.Int32 intVal = 0;
            return System.Int32.TryParse(value, out intVal);
        }

        public static bool IsInt16(string value)
        {
            if (CheckForEmptyString(value.Length))
                return false;
            System.Int16 intVal = 0;
            return System.Int16.TryParse(value, out intVal);
        }

        public static bool IsInt64(string value)
        {
            if (CheckForEmptyString(value.Length))
                return false;
            System.Int64 intVal = 0;
            return System.Int64.TryParse(value, out intVal);
        }

        public static bool IsDouble(string value)
        {
            if (CheckForEmptyString(value.Length))
                return false;
            System.Double doubleVal = 0;
            return System.Double.TryParse(value, out doubleVal);
        }

        public static bool IsDecimal(string value)
        {
            if (CheckForEmptyString(value.Length))
                return false;
            System.Decimal decimalVal = 0;
            return System.Decimal.TryParse(value, out decimalVal);
        }

        public static bool IsDecimalCanbeEmpty(string value)
        {
            System.Decimal decimalVal = 0;
            return System.Decimal.TryParse(value, out decimalVal);
        }

        public static bool IsNonZero(string value)
        {
            if (CheckForEmptyString(value.Length))
                return false;
            System.Decimal decimalVal = 0;
            System.Decimal.TryParse(value, out decimalVal);
            if(decimalVal.Equals(0))
                return false;
            else
                return true;
        }
        public static bool IsGreaterThanZero(string value)
        {
            if (CheckForEmptyString(value.Length))
                return false;
            System.Decimal decimalVal = 0;
            System.Decimal.TryParse(value, out decimalVal);
            if (decimalVal<=0)
                return false;
            else
                return true;
        }
        public static bool IsLessThanZero(string value)
        {
            if (CheckForEmptyString(value.Length))
                return false;
            System.Decimal decimalVal = 0;
            System.Decimal.TryParse(value, out decimalVal);
            if (decimalVal < 0)
                return true;
            else
                return false;
        }
        public static bool IsDateTime(string value)
        {
            if (CheckForEmptyString(value.Length))
                return false;
            System.DateTime dateTimeVal = DateTime.MinValue;
            return System.DateTime.TryParse(value, out dateTimeVal);
        }
       
        public static bool IsValidPinCode(string value)
        {
            if (CheckForEmptyString(value.Length))
                return false;

            if (!IsInt32(value))
                return false;

            string regexPattern = @"^\d{6}$";
            return Regex.Match(value, regexPattern).Success;
        }

        public static bool IsValidAmount(string value)
        {
            if (CheckForEmptyString(value.Length))
                return false;

            if (!IsDecimal(value))
                return false;
            string regexPattern;

            if (Common.DisplayAmountRounding == 0)
                regexPattern = @"^\d+(\.\d{0,0})*$"; //+ (Common.DisplayQtyRounding == 0 ? "" : @"(\.\d{1," + Common.DisplayQtyRounding.ToString() + "})*$");
            else
                regexPattern = @"^\d+(\.\d{1," + Common.DisplayAmountRounding.ToString() + "})*$";
            return Regex.Match(value, regexPattern).Success;
        }

        public static bool IsValidQuantity(string value)
        {
            if (CheckForEmptyString(value.Length))
                return false;

            if (!IsDecimal(value))
                return false;
            string regexPattern;

            if (Common.DisplayQtyRounding == 0)
                regexPattern = @"^\d+(\.\d{0,0})*$"; //+ (Common.DisplayQtyRounding == 0 ? "" : @"(\.\d{1," + Common.DisplayQtyRounding.ToString() + "})*$");
            else
                regexPattern = @"^\d+(\.\d{1," + Common.DisplayQtyRounding.ToString() + "})*$";
            return Regex.Match(value, regexPattern).Success;
        }

        public static bool IsValidEmailID(string value, bool canBeEmpty)
        {
            if (CheckForEmptyString(value.Length) & !canBeEmpty)
                return false;

            if (value.Length > 0)
            {
                string regexPattern = @"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$";
                return Regex.Match(value, regexPattern).Success;
            }
            else
                return true;
        }

        /// <summary>
        /// Method for range validation.
        /// </summary>
        /// <param name="value">Actual Value to validate.</param>
        /// <param name="minValue">Minimum allowed value.</param>
        /// <param name="maxValue">Maximum allowed value.</param>
        /// <returns></returns>
        public static bool RangeValidator(int value, int minValue, int maxValue)
        {
            if (value < minValue || value > maxValue)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Method to validate numeric values.
        /// </summary>
        /// <param name="value">Input value</param>
        /// <returns>true/false</returns>
        public static bool IsNumeric(string value)
        {
            string regexPattern = @"^[0-9]*$";
            return Regex.Match(value, regexPattern).Success;
        }

        /// <summary>
        /// Method to validate alpha numeric values.
        /// </summary>
        /// <param name="value">Input value</param>
        /// <returns>true/false</returns>
        public static bool IsAlphaNumeric(string value)
        {
            string regexPattern = @"^[a-zA-Z0-9]+$";
            return Regex.Match(value, regexPattern).Success;
        }

        /// <summary>
        /// Method to validate alpha numeric with hyphen and under score characters.
        /// </summary>
        /// <param name="value">Input value</param>
        /// <returns>true/false</returns>
        public static bool IsAlphaNumHyphen(string value)
        {
            string regexPattern = @"^[a-zA-Z0-9\-]+$";
            return Regex.Match(value, regexPattern).Success;
        }

        /// <summary>
        /// Method to validate alphabets only.
        /// </summary>
        /// <param name="value">Input value</param>
        /// <returns>true/false</returns>
        public static bool IsAlphabet(string value)
        {
            string regexPattern = @"^[a-zA-Z]+$";
            return Regex.Match(value, regexPattern).Success;
        }

        /// <summary>
        /// Method to validate alphabets with space only.
        /// </summary>
        /// <param name="value">Input value</param>
        /// <returns>true/false</returns>
        public static bool IsAlphaSpace(string value)
        {
            string regexPattern = @"^[a-zA-Z ]+$";
            return Regex.Match(value, regexPattern).Success;
        }

        public static bool CheckForSelectedValue(int selectedIndex)
        {
            if (selectedIndex <= 0)
            {
                return true;
            }
            return false;
        }

        public static void SetErrorMessage(ErrorProvider errP, Control ctrl, string messageID, string headerText)
        {
            if (Type.Equals(ctrl.GetType(), typeof(TextBox)))
                errP.SetError((TextBox)ctrl, Common.GetMessage(messageID, headerText.Substring(0, headerText.Length - 2)));

            else if (Type.Equals(ctrl.GetType(), typeof(ComboBox)))
                errP.SetError((ComboBox)ctrl, Common.GetMessage(messageID, headerText.Substring(0, headerText.Length - 2)));

            else if (Type.Equals(ctrl.GetType(), typeof(CheckBox)))
                errP.SetError((CheckBox)ctrl, Common.GetMessage(messageID, headerText.Substring(0, headerText.Length - 2)));

             else if (Type.Equals(ctrl.GetType(), typeof(DateTimePicker)))
                errP.SetError((DateTimePicker)ctrl, Common.GetMessage(messageID, headerText.Substring(0, headerText.Length - 2)));
        }

        public static void SetErrorMessage(ErrorProvider errP, Control ctrl, params string[] param)
        {
            if (Type.Equals(ctrl.GetType(), typeof(TextBox)))
                errP.SetError((TextBox)ctrl, Common.GetMessage(param));

            else if (Type.Equals(ctrl.GetType(), typeof(ComboBox)))
                errP.SetError((ComboBox)ctrl, Common.GetMessage(param));

            else if (Type.Equals(ctrl.GetType(), typeof(CheckBox)))
                errP.SetError((CheckBox)ctrl, Common.GetMessage(param));

            else if (Type.Equals(ctrl.GetType(), typeof(DateTimePicker)))
                errP.SetError((DateTimePicker)ctrl, Common.GetMessage(param));
        }

        public static void SetErrorMessage(ErrorProvider errP, Control ctrl)
        {
            if (Type.Equals(ctrl.GetType(), typeof(TextBox)))
                errP.SetError((TextBox)ctrl, string.Empty);

            else if (Type.Equals(ctrl.GetType(), typeof(ComboBox)))
                errP.SetError((ComboBox)ctrl, string.Empty);

            else if (Type.Equals(ctrl.GetType(), typeof(CheckBox)))
                errP.SetError((CheckBox)ctrl, string.Empty);

             else if (Type.Equals(ctrl.GetType(), typeof(DateTimePicker)))
                errP.SetError((DateTimePicker)ctrl, string.Empty);
        }

        public static string GetErrorMessage(ErrorProvider errP, Control ctrl)
        {
            string errorMessage = string.Empty;
            if (Type.Equals(ctrl.GetType(), typeof(TextBox)))
                errorMessage = errP.GetError((TextBox)ctrl);

            else if (Type.Equals(ctrl.GetType(), typeof(ComboBox)))
                errorMessage = errP.GetError((ComboBox)ctrl);

            else if (Type.Equals(ctrl.GetType(), typeof(CheckBox)))
                errorMessage = errP.GetError((CheckBox)ctrl);

            else if (Type.Equals(ctrl.GetType(), typeof(DateTimePicker)))
                errorMessage = errP.GetError((DateTimePicker)ctrl);

            return errorMessage;
        }

        public static void AppendToStringBuilder(String value, ref StringBuilder sb)
        {
            sb.Append(value);
            sb.AppendLine(String.Empty);
        }

        //Check for DB NULLs and Valid conversion
        public static string CheckForDBNull(object currentObj, string falseReturn)
        {
            if (currentObj.Equals(DBNull.Value))
                return falseReturn;

            return currentObj.ToString();
        }
        
        public static int CheckForDBNull(object currentObj, int falseReturn)
        {
            if (currentObj.Equals(DBNull.Value))
                return falseReturn;

            if (IsInt32(currentObj.ToString())) return Convert.ToInt32(currentObj); else return falseReturn;
        }
        
        public static bool CheckForDBNull(object currentObj, bool falseReturn)
        {
            if (currentObj.Equals(DBNull.Value))
                return falseReturn;

            if (IsBool(currentObj.ToString())) return Convert.ToBoolean(currentObj); else return falseReturn;
        }

        public static decimal CheckForDBNull(object currentObj, decimal falseReturn)
        {
            if (currentObj.Equals(DBNull.Value))
                return falseReturn;

            if (IsDecimal(currentObj.ToString())) return Convert.ToDecimal(currentObj); else return falseReturn;
        }

        public static object CheckForDBNull(object currentObj, object falseReturn)
        {
            if (currentObj.Equals(DBNull.Value))
                return falseReturn;

            return currentObj;
        }

        public static DateTime CheckForDBNull(object currentObj, DateTime falseReturn)
        {
            if (currentObj.Equals(DBNull.Value))
                return falseReturn;

            if (IsDateTime(currentObj.ToString())) return Convert.ToDateTime(currentObj); else return falseReturn;
        }

        public static bool CheckForListMatch(string text, DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (text == dr[0].ToString()) return true;
            }
            return false;
        }
    }
}
