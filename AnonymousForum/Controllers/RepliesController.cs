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
    public class RepliesController : Controller
    {
        private readonly AnonymousForumContext _context;

        public RepliesController(AnonymousForumContext context)
        {
            _context = context;
        }

        // GET: Replies
        public async Task<IActionResult> Index()
        {
            var anonymousForumContext = _context.Replies.Include(r => r.Thread);
            return View(await anonymousForumContext.ToListAsync());
        }

        // GET: Replies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Replies == null)
            {
                return NotFound();
            }

            var reply = await _context.Replies
                .Include(r => r.Thread)
                .FirstOrDefaultAsync(m => m.ReplyId == id);
            if (reply == null)
            {
                return NotFound();
            }

            return View(reply);
        }

        // GET: Replies/Create
        public IActionResult Create(int fkThreadId)
        {
            ViewData["FkThreadId"] = new SelectList(_context.Threads, "ThreadId", "ThreadTitle", fkThreadId);
            return View();
        }

        // POST: Replies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReplyId,ReplyTitle,ReplyDescription,FkThreadId")] Reply reply)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reply);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Threads", new {id = reply.FkThreadId});
            }
            ViewData["FkThreadId"] = new SelectList(_context.Threads, "ThreadId", "ThreadTitle", reply.FkThreadId);
            return View(reply);
        }

        // GET: Replies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Replies == null)
            {
                return NotFound();
            }

            var reply = await _context.Replies.FindAsync(id);
            if (reply == null)
            {
                return NotFound();
            }
            ViewData["FkThreadId"] = new SelectList(_context.Threads, "ThreadId", "ThreadDescription", reply.FkThreadId);
            return View(reply);
        }

        // POST: Replies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReplyId,ReplyTitle,ReplyDescription,FkThreadId")] Reply reply)
        {
            if (id != reply.ReplyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reply);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReplyExists(reply.ReplyId))
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
            ViewData["FkThreadId"] = new SelectList(_context.Threads, "ThreadId", "ThreadDescription", reply.FkThreadId);
            return View(reply);
        }

        // GET: Replies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Replies == null)
            {
                return NotFound();
            }

            var reply = await _context.Replies
                .Include(r => r.Thread)
                .FirstOrDefaultAsync(m => m.ReplyId == id);
            if (reply == null)
            {
                return NotFound();
            }

            return View(reply);
        }

        // POST: Replies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Replies == null)
            {
                return Problem("Entity set 'AnonymousForumContext.Replies'  is null.");
            }
            var reply = await _context.Replies.FindAsync(id);
            if (reply != null)
            {
                _context.Replies.Remove(reply);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReplyExists(int id)
        {
          return (_context.Replies?.Any(e => e.ReplyId == id)).GetValueOrDefault();
        }
    }
}
