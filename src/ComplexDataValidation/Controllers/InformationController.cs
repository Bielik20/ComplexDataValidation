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
    public class InformationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InformationController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Information
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Information.Include(i => i.Book);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Information/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var information = await _context.Information.SingleOrDefaultAsync(m => m.Id == id);
            if (information == null)
            {
                return NotFound();
            }

            return View(information);
        }

        // GET: Information/Create
        public async Task<IActionResult> Create(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = await _context.Books.Where(x => x.Id == id).FirstOrDefaultAsync();
            ViewData["PersonId"] = book.PersonId;
            ViewData["Id"] = id;
            return View();
        }

        // POST: Information/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreationDate,Submited,Titile")] Information information)
        {
            var book = await _context.Books.Where(x => x.Id == information.Id).FirstOrDefaultAsync();
            if (ModelState.IsValid)
            {
                _context.Add(information);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "People", new { id = book.PersonId });
            }
            ViewData["PersonId"] = book.PersonId;
            ViewData["Id"] = information.Id;
            return View(information);
        }

        // GET: Information/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var information = await _context.Information.SingleOrDefaultAsync(m => m.Id == id);
            if (information == null)
            {
                return NotFound();
            }
            var book = await _context.Books.Where(x => x.Id == information.Id).FirstOrDefaultAsync();
            ViewData["PersonId"] = book.PersonId;
            return View(information);
        }

        // POST: Information/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,CreationDate,Submited,Titile")] Information information)
        {
            if (id != information.Id)
            {
                return NotFound();
            }
            var book = await _context.Books.Where(x => x.Id == information.Id).FirstOrDefaultAsync();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(information);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InformationExists(information.Id))
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
            ViewData["PersonId"] = book.PersonId;
            return View(information);
        }

        // GET: Information/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var information = await _context.Information.SingleOrDefaultAsync(m => m.Id == id);
            if (information == null)
            {
                return NotFound();
            }
            var book = await _context.Books.Where(x => x.Id == information.Id).FirstOrDefaultAsync();
            ViewData["PersonId"] = book.PersonId;
            return View(information);
        }

        // POST: Information/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var information = await _context.Information.SingleOrDefaultAsync(m => m.Id == id);
            _context.Information.Remove(information);
            await _context.SaveChangesAsync();
            var book = await _context.Books.Where(x => x.Id == information.Id).FirstOrDefaultAsync();
            return RedirectToAction("Details", "People", new { id = book.PersonId });
        }

        private bool InformationExists(string id)
        {
            return _context.Information.Any(e => e.Id == id);
        }
    }
}
