using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolGallery.Models
{
    public class ContentModel:BaseModel
    {
        [Required]
        [Display(Name ="标题")]
        public string Title { get; set; }
        [Display(Name ="详情")]
        public string Detail { get; set; }
        [Display(Name ="所属类目")]
        public int CategoryID { get; set; }
        [Display(Name ="上传附件")]
        public string Accessories { get; set; }
        public bool IsVideo { get; set; }
        public DateTime PublishTime { get; set; }
        public string PublisherID { get; set; }
        public string ModifiedIP { get; set; }
        [Display(Name = "排序")]
        [Required]
        public int Sort { get; set; }
    }
}
