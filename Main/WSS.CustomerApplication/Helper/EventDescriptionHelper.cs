namespace WSS.CustomerApplication.Helper
{

    public static class EventDescriptionHelper
    {
        public static string Login = "Profile user logged in";
        public static string Register = " {0} registered with account {1} as " + "{2}";
        public static string CsrRegister = "BR initiated registration for {0}";
        public static string ActivateProfile = "Profile user successfully activated";
        public static string AccountLocked = "System locked profile user due to maximum failed login attempts.";
        public static string AccountUnlocked = "BR unlocked account/ System automated unlock.";
        public static string Logout = "Profile user logged out";
        public static string ChangeEmailDescrption = "Profile user changed their user id from {0} to {1}";
        public static string CsrChangeEmailDescrption = "BR user changed their user id from {0} to {1}";
        public static string ResetPassword = "Profile password changed";
        public static string CsrResetPasswordEmail = "BR initiated Forgot Password";
        public static string ResetPasswordEmail = "Profile user initiated Forgot Password";
        public static string LinkedAccount = "Added new linked account {0} as " + "{1}";
        public static string CsrLinkedAccount = "BR added new linked account {0}";
        public static string UnlinkedAccount = "Profile removed linked account {0}";
        public static string CsrUnlinkedAccount = "BR  removed linked account {0}";
        public static string Nickname = "Changed nickname for account {0}  from {1} to {2}";
        public static string ResendActivation = "User requested activation";
        public static string CsrResendActivation = "BR resent activation to user profile {0}";
        public static string AddedSecondaryEmail = "Added secondary email {0}";
        public static string RemoveSecondaryEmail = "Removed secondary email {0}";
        public static string AcceptedEmailInvite = "Secondary email account accepted invite {0}";
        public static string Unsubscribe = "{0}";
        public static string OptOut = "Notification-only user {0} opted out.";
        public static string ChangedSecurityQuestion = "Profile security question changed";
    }
}