using JWT.Data;
using JWT.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public StudentsController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet("Student")]
        public IActionResult GetAllStudents() => Ok(_context.students.ToList());

        [HttpPost("AddStudent")]
        [Authorize(Roles ="Admin")]
        public IActionResult AddStudent(Student student)
        {
            _context.students.Add(student);
            _context.SaveChanges();
            return Ok(student);
        }

        [HttpPut("EditStudent")]
        [Authorize(Roles = "Admin")]
        public IActionResult EditStudent(Student student) {
                        var stu = _context.students.Find(student.Id);
            if (stu == null) return NotFound();
            stu.Name = student.Name;
            stu.contact = student.contact;
            stu.Status = student.Status;
            _context.SaveChanges();
            return Ok(stu);
        }

        [HttpDelete("DeleteStudent/{id}")]
        [Authorize(Roles= "Admin")]
        public IActionResult DeleteStudent(int id) {
            var stu = _context.students.Find(id);
            if (stu == null) return NotFound();
            _context.students.Remove(stu);
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("Search")]
        public IActionResult SearchStudent(string? name, int? id, string? type, string? status)
        {
            var query = _context.students.AsQueryable();

            if (id.HasValue)
                query = query.Where(s => s.Id == id.Value);

            if (!string.IsNullOrEmpty(name))
                query = query.Where(s => s.Name.ToLower().Contains(name.ToLower()));

            if(!string.IsNullOrEmpty(type))
                query = query.Where(s => s.Type.ToString().ToLower().Contains(type.ToLower()));

            if(!string.IsNullOrEmpty(status))
                query = query.Where(s => s.Status.ToString().ToLower().Contains(status.ToLower()));

            var result = query.ToList();

            if (!result.Any())
                return NotFound(new { Message = "No students found matching your search criteria." });

            return Ok(result);
        }
    }


    
}
