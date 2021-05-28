using IServices;
using IServices.Infrastructure;
using IServices.ISysServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Models.SysModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Extensions;
using Web.Models.AccountViewModels;
using Web.Services;
using static Web.Helpers.AlertType;
using IdentityRole = Microsoft.AspNetCore.Identity.IdentityRole;

namespace Web.Controllers
{
    /// <summary>
    /// </summary>
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;

        private readonly ILogger _logger;
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ISysControllerSysActionService _iSysControllerSysActionService;
        private ISysRoleSysControllerSysActionService _ISysRoleSysControllerSysActionService;
        private ISysHelpService _ISysHelpService;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private IMemoryCache _cache;
        private IJsonDataService jsonDataService;
        private readonly IStringLocalizer<AccountController> _localizer;

        /// <summary>
        /// </summary>
        /// <param name="userManager">
        /// </param>
        /// <param name="signInManager">
        /// </param>
        /// <param name="emailSender">
        /// </param>
        /// <param name="logger">
        /// </param>
        /// <param name="iUnitOfWork">
        /// </param>
        /// <param name="roleManager">
        /// </param>
        /// <param name="iSysControllerSysActionService">
        /// </param>
        /// <param name="iSysRoleSysControllerSysActionService">
        /// </param>
        /// <param name="iSysHelpService">
        /// </param>
        /// <param name="schemeProvider">
        /// </param>

        /// <param name="cache">
        /// </param>
        /// <param name="jsonDataService">
        /// </param>
        /// <param name="localizer">
        /// </param>

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger, IUnitOfWork iUnitOfWork, RoleManager<IdentityRole> roleManager, ISysControllerSysActionService iSysControllerSysActionService, ISysRoleSysControllerSysActionService iSysRoleSysControllerSysActionService, ISysHelpService iSysHelpService, IAuthenticationSchemeProvider schemeProvider, IMemoryCache cache, IJsonDataService jsonDataService, IStringLocalizer<AccountController> localizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _iUnitOfWork = iUnitOfWork;
            _roleManager = roleManager;
            _iSysControllerSysActionService = iSysControllerSysActionService;
            _ISysRoleSysControllerSysActionService = iSysRoleSysControllerSysActionService;
            _ISysHelpService = iSysHelpService;

            _schemeProvider = schemeProvider;

            _cache = cache;
            this.jsonDataService = jsonDataService;
            _localizer = localizer;
        }

        /// <summary>
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="returnUrl">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> PhoneNumberLogin(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            var model = new PhoneNumberLoginInputModel();
            return View(model);
        }

        /// <summary>
        /// </summary>
        /// <param name="PhoneNumber">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        public async Task<IActionResult> GetVerificationCode(string PhoneNumber)
        {
            if (!string.IsNullOrEmpty(PhoneNumber))
            {
                var user = _userManager.Users.Where(a => a.PhoneNumber == PhoneNumber && a.PhoneNumberConfirmed).FirstOrDefault();

                if (user == null)
                {
                    return new BadRequestObjectResult("手机号不存在");
                }

                var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, PhoneNumber);

                // var res = await smsSender.UserLogin("+86" + PhoneNumber, "[\"" + code + "\",\"10\"]");

                return Content("发送成功");
            }
            else
            {
                return new BadRequestObjectResult("电话号码不能为空");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PhoneNumberLogin(PhoneNumberLoginInputModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.Users.Where(a => a.PhoneNumber == model.PhoneNumber && a.PhoneNumberConfirmed).FirstOrDefault();

                if (user != null)
                {
                    if (await _userManager.VerifyChangePhoneNumberTokenAsync(user, model.VerificationCode, model.PhoneNumber))
                    {
                        await _signInManager.SignInAsync(user, true);
                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else if (string.IsNullOrEmpty(returnUrl))
                        {
                            return Redirect("~/");
                        }
                        else
                        {
                            // user might have clicked on a malicious link - should be logged
                            throw new Exception("invalid return URL");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "验证码错误");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "用户不存在");
                }
            }

            return View();
        }

        /// <summary>
        /// </summary>
        /// <param name="returnUrl">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            ViewBag.scheme = (await _schemeProvider.GetAllSchemesAsync()).Where(a => a.DisplayName != null);

            var model = new LoginInputModel();

            return View(model);
        }

        /// <summary>
        /// </summary>
        /// <param name="model">
        /// </param>
        /// <param name="returnUrl">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputModel model, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            ViewBag.scheme = (await _schemeProvider.GetAllSchemesAsync()).Where(a => a.DisplayName != null);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                // request for a local page
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else if (string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect("~/");
                }
                else
                {
                    // user might have clicked on a malicious link - should be logged
                    throw new Exception("invalid return URL");
                }
            }
            else
            {
                //await _events.RaiseAsync(new UserLoginFailureEvent(model.Email, "invalid credentials", clientId: context?.ClientId));
                ModelState.AddModelError(string.Empty, "用户名或密码错误");
            }

            return View(model);
        }

        /// <summary>
        /// </summary>
        /// <param name="rememberMe">
        /// </param>
        /// <param name="returnUrl">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWith2fa(bool rememberMe, string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            IdentityUser user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            LoginWith2faViewModel model = new LoginWith2faViewModel { RememberMe = rememberMe };
            ViewData["ReturnUrl"] = returnUrl;

            return View(model);
        }

        /// <summary>
        /// </summary>
        /// <param name="model">
        /// </param>
        /// <param name="rememberMe">
        /// </param>
        /// <param name="returnUrl">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWith2fa(LoginWith2faViewModel model, bool rememberMe, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            IdentityUser user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            string authenticatorCode = model.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, model.RememberMachine);

            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID {UserId} logged in with 2fa.", user.Id);
                return RedirectToLocal(returnUrl);
            }
            else if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID {UserId} account locked out.", user.Id);
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                _logger.LogWarning("Invalid authenticator code entered for user with ID {UserId}.", user.Id);
                ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
                return View();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="returnUrl">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWithRecoveryCode(string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            IdentityUser user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        /// <summary>
        /// </summary>
        /// <param name="model">
        /// </param>
        /// <param name="returnUrl">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWithRecoveryCode(LoginWithRecoveryCodeViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            IdentityUser user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            string recoveryCode = model.RecoveryCode.Replace(" ", string.Empty);

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);

            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID {UserId} logged in with a recovery code.", user.Id);
                return RedirectToLocal(returnUrl);
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID {UserId} account locked out.", user.Id);
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                _logger.LogWarning("Invalid recovery code entered for user with ID {UserId}", user.Id);
                ModelState.AddModelError(string.Empty, "Invalid recovery code entered.");
                return View();
            }
        }

        /// <summary>
        /// 锁定
        /// </summary>
        /// <returns>
        /// </returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="returnUrl">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewBag.RegistrationProtocol =
                _ISysHelpService.GetAll().FirstOrDefault(a => a.Title.Contains("用户注册协议"))?.Content;

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        /// <summary>
        /// </summary>
        /// <param name="model">
        /// </param>
        /// <param name="returnUrl">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            //if (!model.Agree)
            //{
            //    ModelState.AddModelError("Agree", "您必须同意注册协议，才可以注册！"+model.Agree);
            //}

            var refereeUserId = "";

            //检测推荐人是否有效
            if (!string.IsNullOrEmpty(model.Referee))
            {
                var referee = await _userManager.FindByNameAsync(model.Referee);

                if (referee == null)
                {
                    ModelState.AddModelError("referee", "推荐人不存在，请检查输入是否正确！");
                }
                else
                {
                    refereeUserId = referee.Id;
                }
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new IdentityUser() { UserName = model.UserName };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(refereeUserId))
                {
                    await _userManager.AddClaimAsync(user, new Claim("Referee", refereeUserId));//推荐人
                }

                await _userManager.AddToRoleAsync(user, "注册用户");

                // 第一个用户 创建角色
                if (_userManager.Users.Count() == 1)
                {
                    //// 创建管理员角色
                    //var role = new IdentityRole("管理员") { };

                    //await _roleManager.CreateAsync(role);

                    //将用户和角色关联
                    await _userManager.AddToRoleAsync(user, "管理员");

                    ////系统管理员自动获得所有权限
                    //foreach (var itemSysControllerSysAction in _iSysControllerSysActionService.GetAll())
                    //{
                    //    await _ISysRoleSysControllerSysActionService.AddAsync(new SysRoleSysControllerSysAction
                    //    {
                    //        SysControllerSysActionId = itemSysControllerSysAction.Id,
                    //        RoleId = role.Id
                    //    });
                    //}

                    await _iUnitOfWork.CommitAsync();

                    _logger.LogInformation("User created a new account with password.");
                }

                // var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                // var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                // await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                //await _signInManager.SignInAsync(user, isPersistent: true);

                TempData[Alerts.Success] = "注册成功，请登陆";

                return RedirectToLocal(returnUrl);
            }
            else
            {
                AddErrors(result);
                // If we got this far, something failed, redisplay form
                return View(model);
            }
        }

        ///// <summary>
        ///// Show logout page
        ///// </summary>
        //[HttpGet]
        //public async Task<IActionResult> Logout(string logoutId)
        //{
        //    // build a model so the logout page knows what to display
        //    var vm = await BuildLogoutViewModelAsync(logoutId);

        // if (vm.ShowLogoutPrompt == false) { // if the request for logout was properly
        // authenticated from IdentityServer, then we // don't need to show the prompt and can just
        // log the user out directly. return await Logout(vm); }

        //    return View(vm);
        //}

        ///// <summary>
        ///// Handle logout page postback
        ///// </summary>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Logout(LogoutInputModel model)
        //{
        //    // build a model so the logged out page knows what to display
        //    var vm = await BuildLoggedOutViewModelAsync(model.LogoutId);

        // if (User?.Identity.IsAuthenticated == true) { // delete local authentication cookie await
        // _signInManager.SignOutAsync(); // raise the logout event await _events.RaiseAsync(new
        // UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName())); }

        // // check if we need to trigger sign-out at an upstream identity provider if
        // (vm.TriggerExternalSignout) { // build a return URL so the upstream provider will
        // redirect back to us after the // user has logged out. this allows us to then complete our
        // single sign-out processing. string url = Url.Action("Logout", new { logoutId =
        // vm.LogoutId });

        // // this triggers a redirect to the external provider for sign-out return SignOut(new
        // AuthenticationProperties { RedirectUri = url }, vm.ExternalAuthenticationScheme); }

        //    return View("LoggedOut", vm);
        //}

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        //[ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            //return new SignOutResult(new[] { "Cookies", "oidc" });

            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return Redirect("~/");
        }

        /// <summary>
        /// </summary>
        /// <param name="provider">
        /// </param>
        /// <param name="returnUrl">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            string redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            AuthenticationProperties properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        /// <summary>
        /// </summary>
        /// <param name="returnUrl">
        /// </param>
        /// <param name="remoteError">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToAction(nameof(Login));
            }
            ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            // Sign in the user with this external login provider if the user already has a login.
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in with {Name} provider.", info.LoginProvider);
                return RedirectToLocal(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                //新建用户
                IdentityUser user = new IdentityUser() { UserName = info.LoginProvider + Guid.NewGuid().ToString() };

                await _userManager.CreateAsync(user);

                await _userManager.AddToRoleAsync(user, "注册用户");

                await _userManager.AddLoginAsync(user, info);

                await _signInManager.SignInAsync(user, isPersistent: false);
                _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                return RedirectToLocal(returnUrl);

                //// If the user does not have an account, then ask the user to create an account.
                //ViewData["ReturnUrl"] = returnUrl;
                //ViewData["LoginProvider"] = info.LoginProvider;
                //string email = info.Principal.FindFirstValue(ClaimTypes.Email);
                //return View("ExternalLogin", new ExternalLoginViewModel { Email = email });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    throw new ApplicationException("Error loading external login information during confirmation.");
                }
                IdentityUser user = new IdentityUser() { UserName = model.Email, Email = model.Email };
                IdentityResult result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "注册用户");

                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(nameof(ExternalLogin), model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction(nameof(HomeController.IndexAsync), "Home");
            }

            IdentityUser user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }

            IdentityResult result = await _userManager.ConfirmEmailAsync(user, code);

            return View("ConfirmEmail", result);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("Email", "用户不存在！");
                    return View();
                }

                // For more information on how to enable account confirmation and password reset
                // please visit https://go.microsoft.com/fwlink/?LinkID=532713
                string code = await _userManager.GeneratePasswordResetTokenAsync(user);
                string callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
                await _emailSender.SendEmailAsync(model.Email, "重置密码",
                   $"请单击此处重置密码: <a href='{callbackUrl}'>进入重置密码</a>");
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
            {
                throw new ApplicationException("A code must be supplied for password reset.");
            }
            ResetPasswordViewModel model = new ResetPasswordViewModel { Code = code };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            IdentityUser user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            IdentityResult result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            AddErrors(result);
            return View();
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        /// <summary>
        /// </summary>
        /// <returns>
        /// </returns>
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return Redirect("~/");
            }
        }

        #endregion Helpers

        /// <summary>
        /// </summary>
        /// <param name="UserName">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpGet] // 只能用GET ！！！
        [AllowAnonymous]
        public async Task<ActionResult> CheckUserAccountExists(string UserName)
        {
            var user = await _userManager.FindByNameAsync(UserName);
            return Json(user == null);
        }

        /// <summary>
        /// </summary>
        /// <param name="Email">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpGet] // 只能用GET ！！！
        [AllowAnonymous]
        public async Task<ActionResult> CheckUserEmailExists(string Email)
        {
            var user = await _userManager.Users.Where(a => a.Email == Email).AnyAsync();
            return Json(!user);
        }

        /// <summary>
        /// </summary>
        /// <param name="PhoneNumber">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpGet] // 只能用GET ！！！
        [AllowAnonymous]
        public async Task<ActionResult> CheckPhoneNumberExists(string PhoneNumber)
        {
            var user = await _userManager.Users.Where(a => a.PhoneNumber == PhoneNumber).AnyAsync();

            return Json(!user);
        }

        /// <summary>
        /// </summary>
        /// <param name="Referee">
        /// </param>
        /// <returns>
        /// </returns>
        [HttpGet] // 只能用GET ！！！
        [AllowAnonymous]
        public async Task<ActionResult> CheckRefereeExists(string Referee)
        {
            var user = await _userManager.FindByNameAsync(Referee);
            return Json(user != null);
        }
    }
}