using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolGallery.DAL;
using SchoolGallery.Utils;

namespace SchoolGallery.Controllers
{
    public class LoginController : Controller
    {
        protected SchoolContext context;
        public LoginController(SchoolContext schoolContext)
        {
            this.context = schoolContext;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string accountID, string passWord)
        {
            var user = context.Account.Where(i => i.AccountID== accountID && i.PassWord == Tools.DoubleMD5(passWord??"")).FirstOrDefault();
            if (user == null)
            {
                ViewBag.LoginError = "用户名/密码错误";
                ModelState.AddModelError("LoginError", "用户名/密码错误");
            }
            else
            {

                HttpContext.Session.Set("user", BitConverter.GetBytes(user.ID));
                return Redirect("~/Category/Index");
            }
            return View();
        }
    }
}