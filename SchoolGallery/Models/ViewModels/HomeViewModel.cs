using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolGallery.Models.ViewModels
{
    public class HomeViewModel
    {
        public int SelectID { get; set; }
        public List<CategoryModel> Items { get; set; }
    }
}
