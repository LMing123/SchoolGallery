using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolGallery.Models.ViewModels
{
    public class HomeViewModel
    {
        public string Title { set; get; }
        public int SelectID { get; set; }
        public List<CategoryModel> CategoryItems { get; set; }
        public List<ContentModel> ContentItems { get; set; }
    }
}
