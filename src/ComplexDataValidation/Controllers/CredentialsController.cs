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
    public class CredentialsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CredentialsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Credentials
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Credentials.Include(c => c.Person);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Credentials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentials = await _context.Credentials.SingleOrDefaultAsync(m => m.ID == id);
            if (credentials == null)
            {
                return NotFound();
            }

            return View(credentials);
        }

        // GET: Credentials/Create
        public IActionResult Create()
        {
            ViewData["PersonID"] = new SelectList(_context.People, "ID", "ID");
            return View();
        }

        // POST: Credentials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Age,Name,PersonID,Submited")] Credentials credentials)
        {
            if (ModelState.IsValid)
            {
                _context.Add(credentials);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["PersonID"] = new SelectList(_context.People, "ID", "ID", credentials.PersonID);
            return View(credentials);
        }

        // GET: Credentials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentials = await _context.Credentials.SingleOrDefaultAsync(m => m.ID == id);
            if (credentials == null)
            {
                return NotFound();
            }
            ViewData["PersonID"] = new SelectList(_context.People, "ID", "ID", credentials.PersonID);
            return View(credentials);
        }

        // POST: Credentials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Age,Name,PersonID,Submited")] Credentials credentials)
        {
            if (id != credentials.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(credentials);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CredentialsExists(credentials.ID))
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
            ViewData["PersonID"] = new SelectList(_context.People, "ID", "ID", credentials.PersonID);
            return View(credentials);
        }

        // GET: Credentials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var credentials = await _context.Credentials.SingleOrDefaultAsync(m => m.ID == id);
            if (credentials == null)
            {
                return NotFound();
            }

            return View(credentials);
        }

        // POST: Credentials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var credentials = await _context.Credentials.SingleOrDefaultAsync(m => m.ID == id);
            _context.Credentials.Remove(credentials);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CredentialsExists(int id)
        {
            return _context.Credentials.Any(e => e.ID == id);
        }
    }
}
