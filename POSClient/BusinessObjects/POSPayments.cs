using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Vinculum.Framework.Data;
using Vinculum.Framework.DataTypes;
using CoreComponent.Core.BusinessObjects;

namespace POSClient.BusinessObjects
{
    [Serializable]
    public class POSPayments
    {

        public const string SP_PAYMENT_TYPES_SEARCH = "usp_POS_GetPayments";

        public POSPayments(int id, int parentId, int level, string shortDescription, string description, decimal value, string currencyId, int openPopUp, string receiptDisplay, int paymentMode)
        {
            PaymentModeId = id;
            Level = level;
            SmallDescription = shortDescription;
            LongDescription = description;
            Value = value;
            Currency = currencyId;
            PopupId = openPopUp;
            ReceiptDisplay = receiptDisplay;
            PaymentMode = paymentMode;
            ParentId = parentId;
            SubPayments = new List<POSPayments>();
        }



        public Int32 PaymentModeId
        {
            get;
            set;
        }

        public Int32 ParentId
        {
            get;
            set;
        }

        public Int32 Level
        {
            get;
            set;
        }

        public String SmallDescription
        {
            get;
            set;
        }

        public String LongDescription
        {
            get;
            set;
        }

        public Decimal Value
        {
            get;
            set;
        }

        public string Currency
        {
            get;
            set;
        }

        public Int32 PopupId
        {
            get;
            set;
        }

        public Int32 SortOrder
        {
            get;
            set;
        }

        public String ReceiptDisplay
        {
            get;
            set;
        }

        public Int32 PaymentMode
        {
            get;
            set;
        }

        public Int32 Status
        {
            get;
            set;
        }

        public List<POSPayments> SubPayments
        {
            get;
            set;
        }

        public static List<POSPayments> Search(ref string dbMessage)
        {
            try
            {
                DBParameterList dbParamList = new DBParameterList();
                List<POSPayments> paymentList = new List<POSPayments>();
                int Mode = 1;
                if (Common.IsMiniBranchLocation == 1)
                {
                     Mode = 2;
                }
                else
                {
                     Mode = 1;
                }
                //dbParamList.Add(new DBParameter("@businessLineId", Common.BusinessLine, DbType.String));
                dbParamList.Add(new DBParameter(Common.PARAM_MODE, Mode, DbType.Int32));
                dbParamList.Add(new DBParameter(Common.PARAM_OUTPUT, string.Empty, DbType.String, ParameterDirection.Output, Common.PARAM_OUTPUT_LENGTH));
                using (DataTaskManager dtManager = new DataTaskManager())
                {
                    DataTable dt = dtManager.ExecuteDataTable(SP_PAYMENT_TYPES_SEARCH, dbParamList);
                    if (dt != null)
                    {
                        DataRow[] firstLevelRows = dt.Select("Level = 1");

                        foreach (DataRow dr in firstLevelRows)
                        {
                            //POSTypes serviceType = new POSTypes(dr["Id"].ToString(), dr["ShortDescription"].ToString(), dr["CustomerTypeId"].ToString(), dr["ReceiptDisplay"].ToString(), dr["ParentType"].ToString(), Convert.ToUInt16(dr["Level"]), Convert.ToDecimal(dr["Price"]));
                            POSPayments payment = CreatePayment(dr);
                            DataRow[] childRows = dt.Select("ParentType = '" + payment.PaymentModeId + "'");
                            if (childRows.Length > 0)
                            {
                                AddPaymentHierarchy(childRows, payment, dt);
                            }
                            paymentList.Add(payment);
                        }
                    }
                }
                return paymentList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void AddPaymentHierarchy(DataRow[] childRows, POSPayments payment, DataTable dt)
        {
            foreach (DataRow dr in childRows)
            {
                POSPayments subPayment = CreatePayment(dr);
                DataRow[] subChildren = dt.Select("ParentType = '" + subPayment.PaymentModeId + "'");
                if (subChildren.Length > 0)
                {
                    AddPaymentHierarchy(subChildren, subPayment, dt);
                }
                payment.SubPayments.Add(subPayment);
            }
        }

        private static POSPayments CreatePayment(DataRow row)
        {
            return new POSPayments(Convert.ToInt32(row["Id"]), Convert.ToInt32(row["ParentType"]),
                                                         Convert.ToInt32(row["Level"]), row["ShortDescription"].ToString(), row["Description"].ToString(),
                                                        Convert.ToInt32(row["Value"]), row["Currency"].ToString(), Convert.ToInt32(row["OpenPopup"]), row["ReceiptDisplay"].ToString(), Convert.ToInt32(row["PaymentMode"]));
        }

        public static Common.CreditCardType GetCardType(string cardNumber)
        {
            byte[] number = new byte[16]; // number to validate
            // Remove non-digits
            int len = 0;
            for (int i = 0; i < cardNumber.Length; i++)
            {
                if (char.IsDigit(cardNumber, i))
                {
                    if (len == 16) return Common.CreditCardType.Unknown; // number has too many digits
                    number[len++] = byte.Parse(cardNumber[i].ToString());
                }
            }
            if (len == 16 && (number[0] == 5 && number[1] != 0 && number[1] <= 5))
                //if (System.Text.RegularExpressions.Regex.IsMatch(cardNumber, masterCardRegEx)) 
                return Common.CreditCardType.MasterCard;
            else if (len == 16 && (number[0] == 5 && number[1] == 6 && number[2] <= 1))
                return Common.CreditCardType.BankCard;
            else if ((len == 16 || len == 13) && number[0] == 4)
                return Common.CreditCardType.Visa;
            else if (len == 15 && (number[0] == 3 && (number[1] == 4 || number[1] == 7)))
                return Common.CreditCardType.Amex;
            else if (len == 16 && (number[0] == 6 && number[1] == 0 && number[2] != 1 && number[3] != 1))
                return Common.CreditCardType.Discover;
            else if (len == 14 && (number[0] == 3 && (number[1] == 0 || number[1] == 6 || number[1] == 8)
               && number[1] != 0 || number[2] <= 5))
                return Common.CreditCardType.DinersClub;
            else if ((len == 16 || len == 15) && (number[0] == 3 && number[1] == 5))
                return Common.CreditCardType.JCB;
            else
                return Common.CreditCardType.Unknown;
        }

        public static bool ValidateCreditCard(CoreComponent.Core.BusinessObjects.Common.CreditCardType cardType, string cardNumber)
        {
            byte[] number = new byte[16]; // number to validate

            // Remove non-digits
            int len = 0;
            for (int i = 0; i < cardNumber.Length; i++)
            {
                if (char.IsDigit(cardNumber, i))
                {
                    if (len == 16) return false; // number has too many digits
                    number[len++] = byte.Parse(cardNumber[i].ToString());
                }
            }

            // Validate based on card type, first if tests length, second tests prefix
            switch (cardType)
            {
                case Common.CreditCardType.MasterCard:
                    if (len != 16)
                        return false;
                    if (number[0] != 5 || number[1] == 0 || number[1] > 5)
                        return false;
                    break;

                case Common.CreditCardType.BankCard:
                    if (len != 16)
                        return false;
                    if (number[0] != 5 || number[1] != 6 || number[2] > 1)
                        return false;
                    break;

                case Common.CreditCardType.Visa:
                    if (len != 16 && len != 13)
                        return false;
                    if (number[0] != 4)
                        return false;
                    break;

                case Common.CreditCardType.Amex:
                    if (len != 15)
                        return false;
                    if (number[0] != 3 || (number[1] != 4 && number[1] != 7))
                        return false;
                    break;

                case Common.CreditCardType.Discover:
                    if (len != 16)
                        return false;
                    if (number[0] != 6 || number[1] != 0 || number[2] != 1 || number[3] != 1)
                        return false;
                    break;

                case Common.CreditCardType.DinersClub:
                    if (len != 14)
                        return false;
                    if (number[0] != 3 || (number[1] != 0 && number[1] != 6 && number[1] != 8)
                       || number[1] == 0 && number[2] > 5)
                        return false;
                    break;
                case Common.CreditCardType.JCB:
                    if (len != 16 && len != 15)
                        return false;
                    if (number[0] != 3 || number[1] != 5)
                        return false;
                    break;
            }

            return DoLuhn(number, len);
        }

        private static bool DoLuhn(Byte[] number, int len)
        {
            // Use Luhn Algorithm to validate
            int sum = 0;
            for (int i = len - 1; i >= 0; i--)
            {
                if (i % 2 == len % 2)
                {
                    int n = number[i] * 2;
                    sum += (n / 10) + (n % 10);
                }
                else
                    sum += number[i];
            }
            return (sum % 10 == 0);
        }

    }
}
