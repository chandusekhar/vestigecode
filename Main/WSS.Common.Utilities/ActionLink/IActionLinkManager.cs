namespace WSS.Common.Utilities.ActionLink
{
    public interface IActionLinkManager
    {
        /// <summary>
        ///     Generates a Reset Password Action and associated link for the WssAccount which has the provided
        ///     primaryEmailAddress.
        /// </summary>
        /// <param name="wssAccountId"></param>
        /// <returns>
        ///     A string containing the generated URL.  Null if the wssAccount for the provided primaryEmailAddress cannot be
        ///     found.
        /// </returns>
        string GenerateResetPasswordLink(int wssAccountId);

        /// <summary>
        ///     Generates an Activate Account Action and associated link for the WssAccount which has the provided WssAccountId.
        /// </summary>
        /// <param name="wssAccountId"></param>
        /// <returns>A string containing the generated URL.  Null if the wssAccount for the provided wssAccountId cannot be found.</returns>
        string GenerateActivateAccountLink(int wssAccountId);

        /// <summary>
        ///     Generates an Unsubscribe Secondary Email Address Action and associated link for the WssAccount which has the
        ///     provided WssAccountId, and the provided secondary email address.
        /// </summary>
        /// <param name="wssAccountId"></param>
        /// <param name="secondaryEmailAddress"></param>
        /// <returns>
        ///     A string containing the generated URL.  Null if the wssAccount for the provided primaryEmailAddress cannot be
        ///     found, if the provided secondary email address cannot be found, or if the provided secondary email address is not
        ///     associated with the provided wssAccount account
        /// </returns>
        string GenerateUnsubscribeSecondaryLink(int wssAccountId, string secondaryEmailAddress);

        /// <summary>
        ///     Generates an Unsubscribe Primary Email Address Action and associated link for the WssAccount which has the provided
        ///     WssAccountId.
        /// </summary>
        /// <param name="wssAccountId"></param>
        /// <returns></returns>
        string GenerateUnsubscribePrimaryLink(int wssAccountId);
    }
}