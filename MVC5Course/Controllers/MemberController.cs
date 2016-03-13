using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC5Course.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        [AllowAnonymous]
        // GET: Memberontroller
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel loginData)
        {
            if (ChkLoginData(loginData.Email, loginData.Password))
            {
                FormsAuthentication.RedirectFromLoginPage(loginData.Email, loginData.RememberMe);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("Password", "帳號或密碼錯誤，請重新輸入");

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Member");
        }

        private bool ChkLoginData(string email, string password)
        {
            return (email.Equals("finkytiger@gmail.com") && password.Equals("yayayaya"));
        }
    }
}