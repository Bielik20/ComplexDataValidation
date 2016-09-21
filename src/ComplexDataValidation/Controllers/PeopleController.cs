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
    public class PeopleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly EntitiesManager _entManager;
        private readonly DocumentControll _docControll;

        public PeopleController(ApplicationDbContext context, EntitiesManager entManager, DocumentControll docControll)
        {
            _context = context;
            _entManager = entManager;
            _docControll = docControll;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            return View(await _context.People.ToListAsync());
        }

        public ActionResult RemoteTest(DateTime creationDate, string id)
        {
            return Json(false);
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People.SingleOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            ViewData["ErrorMessage"] = TempData["ErrorMessage"];
            await _entManager.RetrievePerson(person);
            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] Person person)
        {
            if (ModelState.IsValid)
            {
                await _entManager.CreatePerson(person);
                await _context.SaveChangesAsync();

                return RedirectToAction("FastCreate", "Books", new { id = person.Id });
            }
            return View(person);
        }

        public async Task<IActionResult> CreateBook(string bookId ,string id)
        {
            if (id == null || bookId == null)
            {
                return NotFound();
            }

            var book = await _context.Books.SingleOrDefaultAsync(b => b.Id == bookId);
            if (book == null)
            {
                return NotFound();
            }

            await _entManager.RetrieveBook(book);
            if (_docControll.BookFilled(book) == false)
            {
                TempData["ErrorMessage"] = "You must fill all book information first.";
                return RedirectToAction("Details", new { id = id });
            }

            return RedirectToAction("FastCreate", "Books", new { id = id });
        }

        public async Task<IActionResult> CreateChapter(string bookId, string id)
        {
            if (id == null || bookId == null)
            {
                return NotFound();
            }

            var book = await _context.Books.SingleOrDefaultAsync(b => b.Id == bookId);
            if (book == null)
            {
                return NotFound();
            }

            await _entManager.RetrieveBook(book);
            if (_docControll.BookFilled(book) == false)
            {
                TempData["ErrorMessage"] = "You must fill all book information first.";
                return RedirectToAction("Details", new { id = id });
            }

            return RedirectToAction("Create", "Chapters", new { bookId = bookId, personId = id });
        }



        // GET: People/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People.SingleOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People.SingleOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var person = await _context.People.SingleOrDefaultAsync(m => m.Id == id);
            await _entManager.RetrievePerson(person);
            await _entManager.DeletePerson(person);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PersonExists(string id)
        {
            return _context.People.Any(e => e.Id == id);
        }
    }
}
