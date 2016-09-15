using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComplexDataValidation.Data;
using ComplexDataValidation.Models;
using ComplexDataValidation.Helpers;

namespace ComplexDataValidation.Controllers
{
    public class ChaptersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly EntitiesManager _entManager;

        public ChaptersController(ApplicationDbContext context, EntitiesManager entManager)
        {
            _context = context;
            _entManager = entManager;
        }

        // GET: Chapters
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Chapters.Include(c => c.Book);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Chapters/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chapter = await _context.Chapters.SingleOrDefaultAsync(m => m.Id == id);
            if (chapter == null)
            {
                return NotFound();
            }

            return View(chapter);
        }

        // GET: Chapters/Create
        public IActionResult Create(string bookId, string personId)
        {
            if (bookId == null || personId == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = personId;
            ViewData["BookId"] = bookId;
            return View();
        }

        // POST: Chapters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookId,CreationDate,Description,Submited")] Chapter chapter, string personId)
        {
            if (ModelState.IsValid)
            {
                await _entManager.CreateChapter(chapter, chapter.BookId);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "People", new { id = personId });
            }
            ViewData["PersonId"] = personId;
            ViewData["BookId"] = chapter.BookId;
            return View(chapter);
        }

        // GET: Chapters/Edit/5
        public async Task<IActionResult> Edit(string id, string personId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chapter = await _context.Chapters.SingleOrDefaultAsync(m => m.Id == id);
            if (chapter == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = personId;
            return View(chapter);
        }

        // POST: Chapters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,BookId,CreationDate,Description,Submited")] Chapter chapter, string personId)
        {
            if (id != chapter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chapter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChapterExists(chapter.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "People", new { id = personId });
            }
            ViewData["PersonId"] = personId;
            return View(chapter);
        }

        // GET: Chapters/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chapter = await _context.Chapters.SingleOrDefaultAsync(m => m.Id == id);
            if (chapter == null)
            {
                return NotFound();
            }

            return View(chapter);
        }

        // POST: Chapters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var chapter = await _context.Chapters.SingleOrDefaultAsync(m => m.Id == id);
            _context.Chapters.Remove(chapter);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ChapterExists(string id)
        {
            return _context.Chapters.Any(e => e.Id == id);
        }
    }
}
