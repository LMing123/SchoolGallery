﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolGallery.DAL;
using SchoolGallery.Models;
using SchoolGallery.Utils;
using PagedList.Core.Mvc;
using PagedList.Core;

namespace SchoolGallery.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly SchoolContext _context;

        public CategoryController(SchoolContext context) : base(context)
        {
            _context = context;
        }

        // GET: Category
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var list = BulidTree(_context.Category.ToList(), -1, 0).AsQueryable();
            return View(list.ToPagedList(pageNumber,pageSize));
        }

        private IEnumerable<CategoryModel> BulidTree(List<CategoryModel> items, int parentID,int depth)
        {
            var list = new List<CategoryModel>();
            var temItems = items.Where(z => z.ParentID == parentID).ToList();

            string tem = depth == 0 ?  "├" :  "│";
            string tem1 = new string('-', depth * 3);

            tem = tem+tem1+' ';


            if (temItems.Count > 0)
            {
                for (int i = 0; i < temItems.Count; i++)
                {
                    temItems[i].Title = tem + temItems[i].Title;
                    list.Add(temItems[i]);
                    list.AddRange(BulidTree(items, temItems[i].ID, depth + 1));
                }
            }
            return list;
        }

        // GET: Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryModel = await _context.Category
                .FirstOrDefaultAsync(m => m.ID == id);
            if (categoryModel == null)
            {
                return NotFound();
            }

            return View(categoryModel);
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            var Category = new List<SelectListItem> { new SelectListItem() { Text = "主目录", Value = "-1",Selected=true} };
            var allCategory = _context.Category.ToList();


            Category.AddRange(Tools.CreateTree(allCategory, -1, 0));
            ViewBag.CategoryType = Category;
            return View();
        }
       
        // POST: Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,ParentID,NeedPassWord,PassWord,ID")] CategoryModel categoryModel)
        {
            ViewBag.isSuccess = null;
            var Category = new List<SelectListItem> { new SelectListItem() { Text = "主目录", Value = "-1", Selected = true } };
            var allCategory = _context.Category.ToList();


            Category.AddRange(Tools.CreateTree(allCategory, -1, 0));
            //add
            for (int i = 0; i < Category.Count; i++)
            {
                if (Category[i].Value == categoryModel.ParentID.ToString())
                {
                    Category[i].Selected = true;
                }
            }
            //add
            ViewBag.CategoryType = Category;
            if (ModelState.IsValid)
            {
                _context.Add(categoryModel);
                await _context.SaveChangesAsync();
                ViewBag.isSuccess = true;
                return  View() ;
            }
            ViewBag.isSuccess = false;
            // add
            ViewBag.InputTitle = "";

            return View(categoryModel);
        }
    
        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var categoryModel = await _context.Category.FindAsync(id);
            if (categoryModel == null)
            {
                return NotFound();
            }
            var Category = new List<SelectListItem> { new SelectListItem() { Text = "主目录", Value = "-1" } };

            var allCategory = _context.Category.ToList();
            

            Category.AddRange(Tools.CreateTree(allCategory, -1,0));

            for(int i=0;i<Category.Count;i++)
            {
                if(Category[i].Value==categoryModel.ParentID.ToString())
                {
                    Category[i].Selected = true;
                }
            }

            ViewBag.CategoryType = Category;
       
            return View(categoryModel);
        }



        // POST: Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,ParentID,NeedPassWord,PassWord,ID")] CategoryModel categoryModel)
        {
            if (id != categoryModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryModelExists(categoryModel.ID))
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
            return View(categoryModel);
        }

        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryModel = await _context.Category
                .FirstOrDefaultAsync(m => m.ID == id);
            if (categoryModel == null)
            {
                return NotFound();
            }

            return View(categoryModel);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoryModel = await _context.Category.FindAsync(id);
            _context.Category.Remove(categoryModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryModelExists(int id)
        {
            return _context.Category.Any(e => e.ID == id);
        }
    }
}
