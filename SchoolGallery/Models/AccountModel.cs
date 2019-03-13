using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolGallery.Models
{
    public enum UserType
    {
        Admin,
        Normal,
    }
    public class AccountModel:BaseModel
    {
        [Display(Name = "用户姓名")]
        public string UserName { get; set; }
        [Display(Name = "账号")]
        public string AccountID { get; set; }
        [Display(Name = "密码")]
        public string PassWord { get; set; }
        [Display(Name ="用户类型")]
        public UserType UserType { get; set; }
    }
}
