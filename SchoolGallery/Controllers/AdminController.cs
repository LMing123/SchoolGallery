using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolGallery.DAL;
using SchoolGallery.Models.ViewModels;
using SchoolGallery.Utils;

namespace SchoolGallery.Controllers
{
    public class AdminController : BaseController
    {
        private SchoolContext _context;
        public AdminController(SchoolContext context):base(context)
        {
            _context = context;
        }
        public IActionResult ChangePW()
        {
            var user = _context.Account.Find(base.user.ID);
            if(user==null)
            {
                return NotFound();
            }
            return View(new ChangePassWordViewModel { UserID = user.ID });
        }
        [HttpPost]
        public IActionResult ChangePW([Bind("UserID,PassWord,ConfirmPassWord")]ChangePassWordViewModel model)
        {
            var user = _context.Account.Find(model.UserID);
            if(user==null)
            {
                return NotFound();
            }
            if(string.IsNullOrEmpty( model.PassWord))
            {
                ModelState.AddModelError("PassWord", "密码不能为空");
            }
            if(model.PassWord!=model.ConfirmPassWord)
            {
                ModelState.AddModelError("PassWord", "两次输入密码不一致");

                ModelState.AddModelError("ConfirmPassWord", "两次输入密码不一致");
            }
            if(ModelState.IsValid)
            {
                user.PassWord = Tools.DoubleMD5(model.PassWord);
                _context.SaveChanges();
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Category");
            }
            return View(model);
        }
    }
}