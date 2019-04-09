using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolGallery.Models.ViewModels
{
    public class ChangePassWordViewModel
    {
        public int  UserID { get; set; }
        [Display(Name ="新密码")]
        public string PassWord { get; set; }
        [Display(Name ="确定新密码")]
        public string ConfirmPassWord { get; set; }
    }
}
