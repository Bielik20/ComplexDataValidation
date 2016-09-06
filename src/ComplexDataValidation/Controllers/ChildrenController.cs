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
    public class ChildrenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChildrenController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Children
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Children.Include(c => c.Parent);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Children/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var child = await _context.Children.SingleOrDefaultAsync(m => m.ID == id);
            if (child == null)
            {
                return NotFound();
            }

            return View(child);
        }

        // GET: Children/Create
        public IActionResult Create()
        {
            ViewData["ParentID"] = new SelectList(_context.Parents, "ID", "ID");
            return View();
        }

        // POST: Children/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Age,Name,ParentID")] Child child)
        {
            if (ModelState.IsValid)
            {
                _context.Add(child);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ParentID"] = new SelectList(_context.Parents, "ID", "ID", child.ParentID);
            return View(child);
        }

        // GET: Children/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var child = await _context.Children.SingleOrDefaultAsync(m => m.ID == id);
            if (child == null)
            {
                return NotFound();
            }
            ViewData["ParentID"] = new SelectList(_context.Parents, "ID", "ID", child.ParentID);
            return View(child);
        }

        // POST: Children/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Age,Name,ParentID")] Child child)
        {
            if (id != child.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(child);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChildExists(child.ID))
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
            ViewData["ParentID"] = new SelectList(_context.Parents, "ID", "ID", child.ParentID);
            return View(child);
        }

        // GET: Children/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var child = await _context.Children.SingleOrDefaultAsync(m => m.ID == id);
            if (child == null)
            {
                return NotFound();
            }

            return View(child);
        }

        // POST: Children/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var child = await _context.Children.SingleOrDefaultAsync(m => m.ID == id);
            _context.Children.Remove(child);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ChildExists(int id)
        {
            return _context.Children.Any(e => e.ID == id);
        }
    }
}
