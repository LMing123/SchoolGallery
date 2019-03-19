using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolGallery.Models.ViewModels
{
    public class ContentViewModel
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "标题")]
        public string Title { get; set; }
        public string Detail { get; set; }
        [Required]
        [Display(Name = "所属类目")]
        public int CategoryID { get; set; }
        [Display(Name = "上传附件")]
      //  [FileExtensions(Extensions =   "[swf]" , ErrorMessage = "文件格式错误")]
        public IFormFile Accessories { get; set; }
        public DateTime PublishTime { get; set; }
        public string PublisherID { get; set; }
        public string ModifiedIP { get; set; }
        public bool IsVideo { get; set; }
        [Display(Name = "排序")]
        public int Sort { get; set; }
    }
}
