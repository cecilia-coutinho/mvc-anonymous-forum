using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AnonymousForum.Data;
using AnonymousForum.Models.ViewModels;

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
            var anonymousForumContext = _context.Threads.Include(t => t.Topic);

            if(anonymousForumContext == null)
            {
                return NotFound();
            }

            return View(await anonymousForumContext.ToListAsync());
        }

        // GET: Threads/TopicThreads/5
        public IActionResult TopicThreads(int id)
        {
            var threadsTopic = _context.Threads
                .Where(t => t.FkTopicId == id)
                .ToList();

            if(threadsTopic == null)
            {
                return BadRequest();
            }

            return View(threadsTopic);
        }

        // GET: Threads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Threads == null)
            {
                return NotFound();
            }

            var thread = await _context.Threads
                .Include(t => t.Topic)
                .FirstOrDefaultAsync(m => m.ThreadId == id);

            if (thread == null)
            {
                return NotFound(nameof(thread));
            }
            var replies = await _context.Replies
                .Where(r => r.FkThreadId == id)
                .ToListAsync();

            var viewModel = new TopicThreadReplyViewModel
            {
                Topic = thread.Topic,
                Thread = thread,
                Replies = replies
            };

            return View(viewModel);
        }



        // GET: Threads/Create
        public IActionResult Create()
        {
            ViewData["FkTopicId"] = new SelectList(_context.Topics, "TopicId", "TopicName");
            return View();
        }

        // POST: Threads/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThreadId,ThreadTitle,ThreadDescription,FkTopicId")] AnonymousForum.Models.Thread thread)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thread);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkTopicId"] = new SelectList(_context.Topics, "TopicId", "TopicName", thread.FkTopicId);
            return View(thread);
        }

        // GET: Threads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Threads == null)
            {
                return NotFound();
            }

            var thread = await _context.Threads
                .Include(t => t.Topic)
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
            if (_context.Threads == null)
            {
                return Problem("Entity set 'AnonymousForumContext.Threads'  is null.");
            }
            var thread = await _context.Threads.FindAsync(id);
            if (thread != null)
            {
                _context.Threads.Remove(thread);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThreadExists(int id)
        {
          return (_context.Threads?.Any(e => e.ThreadId == id)).GetValueOrDefault();
        }
    }
}
