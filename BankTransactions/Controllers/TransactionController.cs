using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankTransactions.Models;

namespace BankTransactions.Controllers
{
    public class TransactionController : Controller
    {
        private readonly TransactionDbContext _context;

        public TransactionController(TransactionDbContext context)
        {
            _context = context;
        }

        // GET: Transaction
        public async Task<IActionResult> Index()
        {
              return _context.Transactions != null ? 
                          View(await _context.Transactions.ToListAsync()) :
                          Problem("Entity set 'TransactionDbContext.Transactions'  is null.");
        }

        // GET: Transaction/Details/5  This has been removed because we don't need it.
      
        // GET: Transaction/Create
        public IActionResult Create(int id=0)
        {
            if (id == 0)
            {
                return View(new Transaction());
            }
            else
                return View(_context.Transactions.Find(id));


        }

        // POST: Transaction/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactionId,AccountNumber,BeneficiaryName,BankName,SWIFTCODE,Amount,Date")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {

                if (transaction.TransactionId == 0)
                {

                    transaction.Date = DateTime.Now;
                    _context.Add(transaction);

                }
                else
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }        
            return View(transaction);
        }

        // GET: Transaction/Edit/5 This has been removed because we don't need it.


        // POST: Transaction/Edit/5 This has been removed because we don't need it.


        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        // GET: Transaction/Delete/5  This is has been removed
 

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transactions == null)
            {
                return Problem("Entity set 'TransactionDbContext.Transactions'  is null.");
            }
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
