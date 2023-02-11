using BackendApi.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendApi.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly SchoolContext _context;

        public HomeController(SchoolContext context)
        {
            _context = context;
        }

        [HttpGet("/")]
        public async Task<IActionResult> Index([FromQuery] string? keyword)
        {
            var query = from s in _context.Students
                        join e in _context.Enrollments on s.ID equals e.StudentID into joinedEnroll
                        from e in joinedEnroll.DefaultIfEmpty()
                        join c in _context.Courses on e.CourseID equals c.CourseID
                        where c.Title.Contains(keyword)
                        group new { s, c } by s.ID into gStudent
                        select new
                        {
                            gStudent.Key,
                            gStudent.First().s.FirstMidName,
                            Courses = string.Join("|", gStudent.Select(g => g.c.Title))
                        };

            var queryStr = query.ToQueryString();
            var r = await query.ToListAsync();

            return Ok("Hello, world!");
        }
    }
}
