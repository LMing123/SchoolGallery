using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SchoolGallery.DAL;
using SchoolGallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolGallery.Controllers
{
    public class BaseController:Controller
    {
        protected SchoolContext _Context;
        protected AccountModel user;
        public BaseController(SchoolContext schoolContext)
        {
            this._Context = schoolContext;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (HttpContext.Session.TryGetValue("user", out byte[] data) && data != null && data.Length == 4)
            {
                user = _Context.Account.Where(x => x.ID == BitConverter.ToInt32(data, 0)).FirstOrDefault();
                ViewBag.User = user;
                base.OnActionExecuting(context);
            }
            else
            {
                context.Result = new RedirectResult("~/Login/Login");
            }


        }
    }
}
