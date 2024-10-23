using Company.G05.DAL.Models;
using Company.G05.PL.Helper;
using Company.G05.PL.ViewModels.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NuGet.Common;

namespace Company.G05.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> userManger;
		private readonly SignInManager<ApplicationUser> _signInmManager;

		public AccountController( UserManager<ApplicationUser> User ,SignInManager<ApplicationUser> signInManager)
		{
			userManger = User;
			_signInmManager = signInManager;
		}
		#region SignUp
		//SignUp
		[HttpGet]   
        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(SignUPViewModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManger.FindByNameAsync(model.UeserName);
                    if (user is null)
                    {
                        user = await userManger.FindByEmailAsync(model.Email);
                        if (user is null)
                        {
                            user = new ApplicationUser()
                            {
                                UserName = model.UeserName,
                                FirstName = model.FirstName,
                                LastName = model.LastName,
                                Email = model.Email,
                                IsAgree = model.IsAgree,

                            };
                            var result = await userManger.CreateAsync(user, model.Password);

                            if (result.Succeeded)
                            {
                                return RedirectToAction(nameof(SignIn));
                            }
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                        }
                        ModelState.AddModelError(string.Empty, "InValidData");
                        return View(model);

                    }
                    ModelState.AddModelError(string.Empty, "InValidData");

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(model);
        }
		#endregion

		#region SignIn

		//SignIn
		[HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignINViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = await userManger.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var flag = await userManger.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {

                        var Result = await _signInmManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if (Result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }

                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid LogIn");

            }
            return View(model);
        }
		#endregion

		#region SignOut
		//SignOut
		public new async Task<IActionResult> SignOut()
        {
            await _signInmManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }
        #endregion

        #region ForgetPassword
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendResetPasswordUrl(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManger.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    //Create Token  
                    var token = userManger.GeneratePasswordResetTokenAsync(user);
                    //Create 
                    var url = Url.Action("ResetPassword", "Account", new { model.Email, token }, Request.Scheme);

                    //Create Email 
                    var email = new DAL.Models.Email()
                    {
                        To = model.Email,
                        Subject = "Reset Password",
                        Body = url
                    };

                    //Send Email 
                    EmailSetting.SendingEmail(email);
                    return RedirectToAction("CheckYourInBox");
                }

                ModelState.AddModelError(string.Empty, "Invalid Operation ! TryAgain - _ -");
            }
            return View();
        } 
        #endregion

        [HttpGet]
	    public IActionResult CheckYourInBox()
        { 
            return View(); 
        }

        #region ResetPassword
        [HttpGet]   
        public IActionResult ResetPassword(DAL.Models.Email email, Token token)
        {
            TempData["Email"] = email;
            TempData["Token"] = token;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ResrtPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var email = TempData["Email"] as string;
                var token = TempData["Token"] as string;

                var user = await userManger.FindByEmailAsync(email);
                if (user is not null)
                {

                    var Result = await userManger.ResetPasswordAsync(user, token, model.Password);
                    if (Result.Succeeded)
                    {
                        return RedirectToAction(nameof(SignIn));
                    }
                }

            }
            ModelState.AddModelError(string.Empty, "Invalid Operation ! TryAgain - _ -");
            return View(model);
        } 
        #endregion

        public IActionResult AccessDenied() {  return View(); }

    }
}
