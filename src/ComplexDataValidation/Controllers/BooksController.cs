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
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly EntitiesManager _entManager;

        public BooksController(ApplicationDbContext context, EntitiesManager entManager)
        {
            _context = context;
            _entManager = entManager;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Books.Include(b => b.Person);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.SingleOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            await _entManager.RetrieveBook(book);

            return View(book);
        }

        // GET: Books/FastCreate
        public async Task<IActionResult> FastCreate(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = new Book();
            await _entManager.CreateBook(book, id);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "People", new { id = book.PersonId });
        }

        // GET: Books/Create
        public IActionResult Create(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["PersonId"] = id;
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PersonId,Submited")] Book book)
        {
            if (ModelState.IsValid)
            {
                await _entManager.CreateBook(book, book.PersonId);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "People", new { id = book.PersonId });
            }
            ViewData["PersonId"] = book.PersonId;
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.SingleOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,PersonId,Submited")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "People", new { id = book.PersonId });
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.SingleOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var book = await _context.Books.SingleOrDefaultAsync(m => m.Id == id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "People", new { id = book.PersonId });
        }

        private bool BookExists(string id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
