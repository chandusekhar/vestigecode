using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WSS.Common.Utilities;
using WSS.CustomerApplication.Models;
using WSS.CustomerApplication.Properties;
using WSS.Identity;
using System;
using WSS.Data;
using System.Linq;

namespace WSS.CustomerApplication.Controllers
{
    public class LoginController : Controller
    {
        public WssIdentitySignInManager SignInManager => HttpContext.GetOwinContext().Get<WssIdentitySignInManager>();

        public WssIdentityUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<WssIdentityUserManager>();

        private IAuthenticationManager WssAuthenticationManager => HttpContext.GetOwinContext().Authentication;

        private readonly IUnitOfWork _wssUnitOfWork;

        public LoginController(IUnitOfWork wssUnitOfWork)
        {
            _wssUnitOfWork = wssUnitOfWork;
        }


        // GET: Login
        public ActionResult Index()
        {
            if (TempData["SuccessfulEmailChange"] != null &&
                !string.IsNullOrEmpty((string) TempData["SuccessfulEmailChange"]))
            {
                AddMessage(UserMessageType.Success, (string)TempData["SuccessfulEmailChange"], Resources.additionalEmailAddresses_cshtml_Execute_Success);
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                AddMessage(UserMessageType.Error, Resources.LoginController_Login_Invalid_login_attempt_message, Resources.LoginController_Login_Invalid_login_attempt_title);
                return View("Index");
            }

            var user = UserManager.FindByEmail(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", Resources.LoginController_Login_Invalid_Username_);
                return View("Index");
            }
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: true);
            switch (result)
            {
                case SignInStatus.Success:
                    Session["LoginUserName"] = null;
                    return RedirectToAction("Index", "Account");

                // DF - Leaving redundant case statements with default so we know what the default case should be
                // ReSharper disable once RedundantCaseLabel
                case SignInStatus.Failure:
                    if (Session["LoginUserName"] == null || !Convert.ToString(Session["LoginUserName"]).Equals(model.Email))
                    {
                        Session["LoginUserName"] = model.Email;
                        Session["CurrentLoginAttempt"] = 0;
                    }
                    Session["CurrentLoginAttempt"] = Session["CurrentLoginAttempt"] == null
                       ? 1 : Convert.ToInt32(Session["CurrentLoginAttempt"]) + 1;
                    var attempts = 3 - Convert.ToInt32(Session["CurrentLoginAttempt"]);
                    if (Convert.ToInt32(Session["CurrentLoginAttempt"]) >= 3 || attempts < 0)
                    {
                        user.LockoutEnabled = true;
                        user.LockoutEndDateUtc = DateTime.Now.AddHours(48);
                        UserManager.Update(user);
                        var wssAccount = _wssUnitOfWork.WssAccountRepository.FindAll().FirstOrDefault(x => x.AuthenticationId == user.Id);
                        if (wssAccount != null)
                        {
                            wssAccount.WssAccountStatusCode = "LCKD";
                            _wssUnitOfWork.Save(user.Email);
                        }
                        ModelState.AddModelError("Password", Resources.PasswordController_ForgotPassword_Attempts_count_Exceeded_Session_Locked);
                    }
                    else
                        ModelState.AddModelError("Password", String.Format(Resources.LoginController_Login_You_have__0__attempts_remaining, attempts));

                    return View("Index");
                // ReSharper disable once RedundantCaseLabel
                case SignInStatus.LockedOut:
                    ModelState.AddModelError("Password", Resources.PasswordController_ForgotPassword_Attempts_count_Exceeded_Session_Locked);
                    return View("Index");
                // ReSharper disable once RedundantCaseLabel
                case SignInStatus.RequiresVerification:
                default:
                    AddMessage(UserMessageType.Error, Resources.LoginController_Login_Invalid_login_attempt_message, Resources.LoginController_Login_Invalid_login_attempt_title);
                    return View("Index");
            }
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Logoff()
        {
            WssAuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Add a message to be displayed on the next page view.
        /// </summary>
        /// <param name="type">Type of the message, success, error etc.</param>
        /// <param name="message">Text of the message to show</param>
        /// <param name="title">Title to show</param>
        private void AddMessage(UserMessageType type, string message, string title)
        {
            var messages = new List<UserMessageModel>
                    {
                        new UserMessageModel()
                            {
                                Message = message,
                                Title = title,
                                Type = type
                            }
                    };
            TempData["Notifications"] = messages;
        }

        /// <summary>
        /// Add a message to be displayed on the next page view, no title shown.
        /// </summary>
        /// <param name="type">Type of the message, success, error etc.</param>
        /// <param name="message">Text of the message to show</param>
        private void AddMessage(UserMessageType type, string message)
        {
            AddMessage(type, message, string.Empty);
        }

        /// <summary>
        /// Clear any pending user messages.
        /// </summary>
        public void ClearMessages()
        {
            TempData["Notifications"] = new List<UserMessageModel>();
        }
    }
}