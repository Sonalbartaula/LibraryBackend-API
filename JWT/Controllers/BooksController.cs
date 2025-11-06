using Jwt.Model;
using JWT.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly MyDbContext _context;

        public BooksController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllBooks() => Ok(_context.Books.ToList());

        [HttpPost("AddBooks")]
        [Authorize(Roles = "Admin")]
        public IActionResult AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            return Ok(book);
        }

        [HttpPut("Edit/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateBook(int id, Book updated)
        {
            var book = _context.Books.Find(id);
            if (book == null) return NotFound();

            book.Title = updated.Title;
            book.Author = updated.Author;
            book.ISBN = updated.ISBN;
            book.Status = updated.Status;

            _context.SaveChanges();
            return Ok(book);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null) return NotFound();

            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }

        

        [HttpGet("Search")]
        public IActionResult GetBooks(string? searchText = null, string? status = null, string? categories = "All Categories")
        {            
            var query = _context.Books.AsQueryable();
   
            if (!string.IsNullOrEmpty(searchText))
            {
                string lowerSearch = searchText.ToLower();
                query = query.Where(b =>
                    b.Title.ToLower().Contains(lowerSearch) ||
                    b.ISBN.ToLower().Contains(lowerSearch));
            }
            if (!string.IsNullOrEmpty(categories) && categories.ToLower() != "all categories")
            {
                var selectedCategories = categories
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(c => c.Trim().ToLower())
                    .ToList();

                query = query.Where(b => selectedCategories.Contains(b.Category.ToLower()));
            }
            if (!string.IsNullOrEmpty(status) && status.ToLower() != "all status")
            {
                if (Enum.TryParse(typeof(Bookstatus), status, true, out var statusEnum))
                {
                    query = query.Where(b => b.Status == (Bookstatus)statusEnum);
                }
                else
                {
                    return BadRequest(new { Message = "Invalid status value." });
                }
            }

            var results = query.ToList();

            if (!results.Any())
                return NotFound(new { Message = "No books found matching your search criteria." });

            return Ok(results);
        }

    }
}