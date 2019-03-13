using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolGallery.DAL;
using SchoolGallery.Models;
using SchoolGallery.Models.ViewModels;
using SchoolGallery.Utils;

namespace SchoolGallery.Controllers
{
    public class VideoController : BaseController
    {
        private readonly SchoolContext _context;
        private readonly IHostingEnvironment _env;


        public VideoController(SchoolContext context, IHostingEnvironment env) : base(context)
        {
            _context = context;
            _env = env;
        }

        // GET: Content
        public async Task<IActionResult> Index()
        {
            return View(await _context.Content.Where(z=>z.IsVideo==true).ToListAsync());
        }

        // GET: Content/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentModel = await _context.Content
                .FirstOrDefaultAsync(m => m.ID == id);
            if (contentModel == null)
            {
                return NotFound();
            }

            return View(contentModel);
        }

        // GET: Content/Create
        public IActionResult Create()
        {
            var Category = new List<SelectListItem> { new SelectListItem() { Text = "主目录", Value = "-1" } };

            var allCategory = _context.Category.ToList();

            Category.AddRange(Tools.CreateTree(allCategory, -1, 0));
            ViewBag.Category = Category;
            return View();
        }

        // POST: Content/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Detail,CategoryID,Accessories,Sort,ID")] ContentViewModel contentModel)
        {
           
            if (!contentModel.Accessories.FileName.EndsWith(".flv"))
            {
                ModelState.AddModelError("Accessories", "上传FLV格式文件");
            }
            if (ModelState.IsValid)
            {
                string FilePath = Path.Combine(_env.ContentRootPath, "Upload", "Videos");

                if (!Directory.Exists(FilePath))
                {
                    Directory.CreateDirectory(FilePath);
                }
                string fileName = Guid.NewGuid().ToString();
                using (var files = new FileStream(Path.Combine(FilePath, fileName + ".flv"), FileMode.CreateNew))
                {
                    contentModel.Accessories.CopyTo(files);
                }
                ContentModel content = new ContentModel()
                {
                    CategoryID = contentModel.CategoryID,
                    Title = contentModel.Title,
                    PublishTime = DateTime.Now,
                    PublisherID = user.AccountID,
                    ModifiedIP = HttpContext.Connection.RemoteIpAddress.ToString(),
                    Accessories = fileName,
                    IsVideo = true,
                };

                _context.Add(content);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var Category = new List<SelectListItem> { new SelectListItem() { Text = "主目录", Value = "-1" } };

            var allCategory = _context.Category.ToList();

            Category.AddRange(Tools.CreateTree(allCategory, -1, 0));
            ViewBag.Category = Category;
            return View(contentModel);
        }

        // GET: Content/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentModel = await _context.Content.FindAsync(id);

            var Category = new List<SelectListItem> { new SelectListItem() { Text = "主目录", Value = "-1" } };

            var allCategory = _context.Category.ToList();


            Category.AddRange(Tools.CreateTree(allCategory, -1, 0));

            for (int i = 0; i < Category.Count; i++)
            {
                if (Category[i].Value == contentModel.CategoryID.ToString())
                {
                    Category[i].Selected = true;
                }
            }
            ViewBag.Category = Category;
            if (contentModel == null)
            {
                return NotFound();
            }
            return View(new ContentViewModel
            {
                ID = contentModel.ID,
                Title = contentModel.Title,
                CategoryID = contentModel.CategoryID,
                PublisherID = contentModel.PublisherID,
                PublishTime = contentModel.PublishTime,
                ModifiedIP = contentModel.ModifiedIP,
                Sort = contentModel.Sort
            });
        }

        // POST: Content/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Detail,CategoryID,Accessories,Sort,ID")] ContentViewModel contentModel)
        {
            if (id != contentModel.ID)
            {
                return NotFound();
            }
            var content = await _context.Content.FindAsync(id);
            if (content == null)
            {
                return NotFound();
            }
            if (!contentModel.Accessories.FileName.EndsWith(".swf"))
            {
                ModelState.AddModelError("Accessories", "上传SWF格式文件");
            }

            if (ModelState.IsValid)
            {
                try
                {


                    if (contentModel.Accessories == null || contentModel.Accessories.Length == 0)
                    {
                        content.Title = contentModel.Title;
                        content.PublisherID = user.AccountID;
                        content.IsVideo = contentModel.IsVideo;
                        content.CategoryID = contentModel.CategoryID;
                        content.PublishTime = DateTime.Now;
                        content.ModifiedIP = HttpContext.Connection.RemoteIpAddress.ToString();
                        content.Sort = contentModel.Sort;
                    }
                    else
                    {
                        string filePath = Path.Combine(_env.ContentRootPath, "Upload", "Files");
                        string fileName = Guid.NewGuid().ToString();
                        using (var files = new FileStream(Path.Combine(filePath, fileName), FileMode.CreateNew))
                        {
                            contentModel.Accessories.CopyTo(files);
                        }
                        content.Title = contentModel.Title;
                        content.PublisherID = user.AccountID;
                        content.IsVideo = contentModel.IsVideo;
                        content.CategoryID = contentModel.CategoryID;
                        content.PublishTime = DateTime.Now;
                        content.ModifiedIP = HttpContext.Connection.RemoteIpAddress.ToString();
                        content.Sort = contentModel.Sort;
                        content.Accessories = fileName;
                    }

                    _context.Update(contentModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentModelExists(contentModel.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var Category = new List<SelectListItem> { new SelectListItem() { Text = "主目录", Value = "-1" } };

            var allCategory = _context.Category.ToList();


            Category.AddRange(Tools.CreateTree(allCategory, -1, 0));

            for (int i = 0; i < Category.Count; i++)
            {
                if (Category[i].Value == contentModel.CategoryID.ToString())
                {
                    Category[i].Selected = true;
                }
            }
            ViewBag.Category = Category;
            return View(contentModel);
        }

        // GET: Content/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentModel = await _context.Content
                .FirstOrDefaultAsync(m => m.ID == id);
            if (contentModel == null)
            {
                return NotFound();
            }

            return View(contentModel);
        }

        // POST: Content/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contentModel = await _context.Content.FindAsync(id);
            _context.Content.Remove(contentModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContentModelExists(int id)
        {
            return _context.Content.Any(e => e.ID == id);
        }
    }
}