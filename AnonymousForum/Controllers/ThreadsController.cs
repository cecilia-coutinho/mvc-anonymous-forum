using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AnonymousForum.Data;
using AnonymousForum.Models;

namespace AnonymousForum.Controllers
{
    public class ThreadsController : Controller
    {
        private readonly AnonymousForumContext _context;

        public ThreadsController(AnonymousForumContext context)
        {
            _context = context;
        }

        // GET: Threads
        public async Task<IActionResult> Index()
        {
              return _context.Thread != null ? 
                          View(await _context.Thread.ToListAsync()) :
                          Problem("Entity set 'AnonymousForumContext.Thread'  is null.");
        }

        // GET: Threads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Thread == null)
            {
                return NotFound();
            }

            var thread = await _context.Thread
                .FirstOrDefaultAsync(m => m.ThreadId == id);
            if (thread == null)
            {
                return NotFound();
            }

            return View(thread);
        }

        // GET: Threads/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Threads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThreadId,ThreadTitle,ThreadDescription,FkTopicId")] Models.Thread thread)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thread);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(thread);
        }

        // GET: Threads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Thread == null)
            {
                return NotFound();
            }

            var thread = await _context.Thread.FindAsync(id);
            if (thread == null)
            {
                return NotFound();
            }
            return View(thread);
        }

        // POST: Threads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ThreadId,ThreadTitle,ThreadDescription,FkTopicId")] Models.Thread thread)
        {
            if (id != thread.ThreadId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thread);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThreadExists(thread.ThreadId))
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
            return View(thread);
        }

        // GET: Threads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Thread == null)
            {
                return NotFound();
            }

            var thread = await _context.Thread
                .FirstOrDefaultAsync(m => m.ThreadId == id);
            if (thread == null)
            {
                return NotFound();
            }

            return View(thread);
        }

        // POST: Threads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Thread == null)
            {
                return Problem("Entity set 'AnonymousForumContext.Thread'  is null.");
            }
            var thread = await _context.Thread.FindAsync(id);
            if (thread != null)
            {
                _context.Thread.Remove(thread);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThreadExists(int id)
        {
          return (_context.Thread?.Any(e => e.ThreadId == id)).GetValueOrDefault();
        }
    }
}
