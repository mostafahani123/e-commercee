﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using e_commercee.Data;
using e_commercee.Models;

namespace e_commercee.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dashboard/Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: Dashboard/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Dashboard/Categories/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category,IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                if (Image == null)
                {
                    ModelState.AddModelError(nameof(Category.Image), "Image is Requird");
                    return View(category);
                }
                var ImageName = Guid.NewGuid() + Path.GetExtension(Image.FileName);

                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Categories")))
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Categories"));
                }

                var savepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Categories", ImageName);

                await using (var stream = new FileStream(savepath, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }
                category.Image = $"/img/Categories/{ImageName}";

                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Dashboard/Categories/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
           
            return View(category);
        }

        // POST: Dashboard/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category, IFormFile Image)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldcategory = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);


                    if (Image != null)
                    {
                        var ImageName = Guid.NewGuid() + Path.GetExtension(Image.FileName);

                        if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Categories")))
                        {
                            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Categories"));
                        }

                        var savepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Categories", ImageName);

                        await using (var stream = new FileStream(savepath, FileMode.Create))
                        {
                            await Image.CopyToAsync(stream);
                        }
                        oldcategory.Image = $"/img/Categories/{ImageName}";
                    }
                    oldcategory.Name = category.Name;
                    oldcategory.Decription = category.Decription;
                    _context.Update(oldcategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(category);
        }

        // GET: Dashboard/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Dashboard/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
