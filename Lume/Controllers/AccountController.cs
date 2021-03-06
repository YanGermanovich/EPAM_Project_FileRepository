﻿using BLL.Services_Interface;
using Lume.Infrastructure;
using Lume.Models;
using Lume.Providers;
using NLog;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Lume.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private readonly IUserService _userService;

        private readonly ILogger _logger;

        public AccountController(IUserService service, ILogger logger)
        {
            _userService = service;
            _logger = logger;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            var type = HttpContext.User.GetType();
            var iden = HttpContext.User.Identity.GetType();
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LogInViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(viewModel.Email, viewModel.Password))
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Email, viewModel.RememberMe);
                    _logger.Info(String.Format("User {0}, signed in",viewModel.Email));
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        if (Request.IsAjaxRequest())
                        {
                            return JavaScript("window.location ='" + returnUrl + "'");
                        }
                        return RedirectToAction(returnUrl);               
                    }
                    else
                    {
                        if (Request.IsAjaxRequest())
                        {
                            return JavaScript("window.location = '" + Url.Action("Index", "Home") + "'");
                        }
                        return RedirectToAction("Index", "Home");        
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or password.");
                }
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("_LogInFormPartial");
            }
            return View("SignIn");
        }

        [AllowAnonymous]
        public ActionResult SignIn()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(SignUpViewModel viewModel)
        {

            if (viewModel.Captcha != (string)Session[CaptchaImage.CaptchaValueKey])
            {
                ModelState.AddModelError("Captcha", "Incorrect input.");

                return View(viewModel);
            }

            var anyUser = _userService.GetAllByPredicate(us=>true).Any(u => u.Email.Contains(viewModel.Email));

            if (anyUser)
            {
                ModelState.AddModelError("", "User with this address already registered.");
                return View(viewModel);
            }

            if (ModelState.IsValid)
            {
                var membershipUser = ((CustomMembershipProvider)Membership.Provider)
                    .CreateUser(viewModel.Email, viewModel.Password);

                if (membershipUser != null)
                {
                    _logger.Info(String.Format("User {0}, signed up", viewModel.Email));
                    FormsAuthentication.SetAuthCookie(viewModel.Email, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Error registration.");
                }
            }
            return View(viewModel);
        }

        public ActionResult LogOut()
        {
            _logger.Info(String.Format("User {0}, signed out", User.Identity.Name));
            FormsAuthentication.SignOut();
            
            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        public ActionResult Captcha()
        {
            Session[CaptchaImage.CaptchaValueKey] =
                new Random(DateTime.Now.Millisecond).Next(1111, 9999).ToString(CultureInfo.InvariantCulture);
            var ci = new CaptchaImage(Session[CaptchaImage.CaptchaValueKey].ToString(), 211, 50, "Helvetica");

            // Change the response headers to output a JPEG image.
            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";

            // Write the image to the response stream in JPEG format.
            ci.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);

            // Dispose of the CAPTCHA image object.
            ci.Dispose();
            return null;
        }

        [AllowAnonymous]
        public ActionResult GetLogInForm()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("_LogInPartial");
            }
            else
                return null;
        }

    }
}
