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

        public PeopleController(ApplicationDbContext context, EntitiesManager entManager)
        {
            _context = context;
            _entManager = entManager;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            return View(await _context.People.ToListAsync());
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

        public async Task<IActionResult> BookCreate(string id)
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

            if (await IsBookCompleted(id) == false)
            {
                ViewData["BookSubmitError"] = "You must fill all book information first.";
                await _entManager.RetrievePerson(person);
                return View("Details", person);
            }

            return RedirectToAction("FastCreate", "Books", new { id = id });
        }

        private async Task<bool> IsBookCompleted(string personId)
        {
            foreach (var book in await _context.Books.Where(x => x.PersonId == personId && x.Submited == false).ToListAsync())
            {
                await _entManager.RetrieveBook(book);
                if (book.Information == null)
                {
                    return false;
                }
            }
            return true;
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
