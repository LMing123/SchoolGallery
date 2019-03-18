using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolGallery.Models
{
    public class CategoryModel:BaseModel
    {
        [Required]
        [Display(Name ="标题")]
        public string Title { get; set; }
        [Display(Name = "所属级别")]
        public int ParentID { get; set; }
        [Display(Name ="是否需要密码")]
        public bool NeedPassWord { get; set; }
        [Display(Name = "密码")]
        public string PassWord { get; set; }
        [Display(Name ="排序")]
        [Required]
        public int Sort { get; set; }
    }
}
