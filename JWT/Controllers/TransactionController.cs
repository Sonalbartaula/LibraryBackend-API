using Jwt.Model;
using JWT.Data;
using JWT.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public TransactionsController(MyDbContext context)
        {
            _context = context;
        }

        
        [HttpPost("checkout")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> Checkout([FromBody] Transaction checkoutRequest)
        {
            
            var book = await _context.Books
                .FirstOrDefaultAsync(b => b.Title == checkoutRequest.BookName || b.ISBN == checkoutRequest.Isbn);

            if (book == null)
                return NotFound("Book not found.");

            
            var activeLoan = await _context.Transactions
                .FirstOrDefaultAsync(t => t.BookName == checkoutRequest.BookName && t.ReturnDate == null);

            if (activeLoan != null)
                return BadRequest("This book is already checked out.");

            
            var transaction = new Transaction
            {
                BookName = checkoutRequest.BookName,
                Booktitle = checkoutRequest.Booktitle,
                MemeberName = checkoutRequest.MemeberName,
                Isbn = checkoutRequest.Isbn,
                IssueStatus = Issuestatus.Issued,
                CheckoutDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(14)
            };

            _context.Transactions.Add(transaction);

            book.Status = Bookstatus.Unavailable;
            book.IssuedCopies += 1;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Book checked out successfully",
                transaction
            });
        }

        
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveLoans()
        {
            var activeLoans = await _context.Transactions
                .Where(t => t.ReturnDate == null)
                .ToListAsync();

            return Ok(activeLoans);
        }

        
        [HttpPut("return/{id}")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> ReturnBook(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
                return NotFound("Transaction not found.");

            transaction.ReturnDate = DateTime.UtcNow;
            if (transaction.ReturnDate > transaction.DueDate)
            {
                var daysLate = (transaction.ReturnDate.Value - transaction.DueDate).Days;
                transaction.Fine = daysLate * 10; // Example: Rs.10 per day
            }
            else
            {
                transaction.Fine = 0;
            }


            var book = await _context.Books.FirstOrDefaultAsync(b => b.Title == transaction.BookName);
            if (book != null)
                book.Status = Bookstatus.Available;


            await _context.SaveChangesAsync();

            return Ok(new { message = "Book returned successfully", transaction });
        }

        
        [HttpGet("history")]
        public async Task<IActionResult> GetTransactionHistory()
        {
            var history = await _context.Transactions
                .OrderByDescending(t => t.CheckoutDate)
                .ToListAsync();

            return Ok(history);
        }
    }
}
