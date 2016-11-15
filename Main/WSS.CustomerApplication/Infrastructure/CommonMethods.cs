using System;
using System.Linq;
namespace WSS.CustomerApplication.Infrastructure
{
    public class CommonMethods
    {
        public static bool ValidateUtilityAccountNumberAndMeterNumber(UtilityBilling.Data.IUnitOfWork ubUnitOfWork, string ccbAccountNumber, string meterNumberForAccount,
           bool isAccountUnmetered)
        {
            // No CCB Account number -- not valid
            if (string.IsNullOrWhiteSpace(ccbAccountNumber))
            {
                return false;
            }

            // Meter number entered and unmetered is checked -- not valid
            if (!string.IsNullOrWhiteSpace(meterNumberForAccount) && isAccountUnmetered)
            {
                return false;
            }

            // Unmetered is not checked (and therefore we have a meter number entered, per the above check) -- See if the CCB Account number / meter number pair exists in the lookup table
            if (!isAccountUnmetered)
            {
                return ubUnitOfWork.AccountMeterLookupRepository.FindAll()
                    .Any(x => x.CcbAcctId == ccbAccountNumber && x.CcbBadgeNbr == meterNumberForAccount);
            }

            // Last but not least, Unmetered is checked, and no meter number was entered -- See if the CCB account number exists in the lookup table, with a meter number of UNMETERED, with no other entries for that CCB account number
            var accountMeterLookupResults =
                    ubUnitOfWork.AccountMeterLookupRepository.FindAll()
                        .Where(x => x.CcbAcctId == ccbAccountNumber);

            return accountMeterLookupResults.Count() == 1 && (accountMeterLookupResults.FirstOrDefault()?.CcbBadgeNbr?.Equals("unmetered", StringComparison.CurrentCultureIgnoreCase) ?? false);
        }
    }
}