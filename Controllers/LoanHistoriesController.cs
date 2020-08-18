using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlquilerDeVideoJuegos_History.Models;

namespace AlquilerDeVideoJuegos_History.Controllers
{
    public class LoanHistoriesController : Controller
    {
        private readonly MyDBContext _context;

        public LoanHistoriesController(MyDBContext context)
        {
            _context = context;
        }

        // GET: LoanHistories
        public async Task<IActionResult> Index()
        {
            var myDBContext = _context.LoanHistory.Include(l => l.Client).Include(l => l.VideoGame);
            return View(await myDBContext.ToListAsync());
        }

        // GET: LoanHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanHistory = await _context.LoanHistory
                .Include(l => l.Client)
                .Include(l => l.VideoGame)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loanHistory == null)
            {
                return NotFound();
            }

            return View(loanHistory);
        }

        // GET: LoanHistories/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Id");
            ViewData["VideoGameId"] = new SelectList(_context.VideoGame, "Id", "Id");
            return View();
        }

        // POST: LoanHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LondDate,LondDateTime,CreationDate,ClientId,VideoGameId")] LoanHistory loanHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loanHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Id", loanHistory.ClientId);
            ViewData["VideoGameId"] = new SelectList(_context.VideoGame, "Id", "Id", loanHistory.VideoGameId);
            return View(loanHistory);
        }

        // GET: LoanHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanHistory = await _context.LoanHistory.FindAsync(id);
            if (loanHistory == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Id", loanHistory.ClientId);
            ViewData["VideoGameId"] = new SelectList(_context.VideoGame, "Id", "Id", loanHistory.VideoGameId);
            return View(loanHistory);
        }

        // POST: LoanHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LondDate,LondDateTime,CreationDate,ClientId,VideoGameId")] LoanHistory loanHistory)
        {
            if (id != loanHistory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loanHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanHistoryExists(loanHistory.Id))
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
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Id", loanHistory.ClientId);
            ViewData["VideoGameId"] = new SelectList(_context.VideoGame, "Id", "Id", loanHistory.VideoGameId);
            return View(loanHistory);
        }

        // GET: LoanHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loanHistory = await _context.LoanHistory
                .Include(l => l.Client)
                .Include(l => l.VideoGame)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loanHistory == null)
            {
                return NotFound();
            }

            return View(loanHistory);
        }

        // POST: LoanHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loanHistory = await _context.LoanHistory.FindAsync(id);
            _context.LoanHistory.Remove(loanHistory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoanHistoryExists(int id)
        {
            return _context.LoanHistory.Any(e => e.Id == id);
        }
    }
}
