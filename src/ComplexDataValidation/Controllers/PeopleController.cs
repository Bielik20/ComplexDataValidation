using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComplexDataValidation.Data;
using ComplexDataValidation.Models;

namespace ComplexDataValidation.Controllers
{
    public class PeopleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PeopleController(ApplicationDbContext context)
        {
            _context = context;    
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

            person.Credentials = await _context.Credentials.Where(x => x.Id == person.Id).FirstOrDefaultAsync();
            person.Pet = await _context.Pets.Where(x => x.Id == person.Id).FirstOrDefaultAsync();
            var booksQuery = _context.Books
                       .Where(p => p.PersonId == person.Id)
                       .Select(p => p);
            person.Books = await booksQuery.ToListAsync();
            foreach (var book in person.Books)
            {
                book.Information = await _context.Information.Where(x => x.Id == book.Id).FirstOrDefaultAsync();

                var chaptersQuery = from c in _context.Chapters
                                    where c.BookId == book.Id
                                    orderby c.CreationDate
                                    select c;
                book.Chapters = await chaptersQuery.ToListAsync();
            }
            person.Books.OrderBy(b => b.Information.CreationDate).ThenBy(b => b.Id);

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
                var myId = Guid.NewGuid().ToString("N");
                while (await _context.People.Where(x => x.Id == myId).AnyAsync())
                {
                    myId = Guid.NewGuid().ToString("N");
                }
                person.Id = myId;
                _context.Add(person);

                var book = new Book()
                {
                    PersonId = person.Id,
                    Submited = false
                };
                myId = Guid.NewGuid().ToString("N");
                while (await _context.Books.Where(x => x.Id == myId).AnyAsync())
                {
                    myId = Guid.NewGuid().ToString("N");
                }
                book.Id = myId;
                _context.Add(book);

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(person);
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
            _context.People.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PersonExists(string id)
        {
            return _context.People.Any(e => e.Id == id);
        }
    }
}
