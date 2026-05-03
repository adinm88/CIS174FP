using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddyFinal.Models;
using StudyBuddyFinal.Services;
using System.Security.Claims;

namespace StudyBuddyFinal.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly CourseService _courseService;

        public CoursesController(CourseService courseService)
        {
            _courseService = courseService;
        }

        // Get logged-in user ID
        private string? UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.SetString("LastPage", "Courses");
            var courses = await _courseService.GetAllAsync(UserId);
            return View(courses);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            if (!ModelState.IsValid)
                return View(course);

            course.UserId = UserId;
            await _courseService.AddAsync(course);

            return RedirectToAction("Index");
        }

        // GET: Courses/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseService.GetByIdAsync(id, UserId);

            if (course == null)
                return NotFound();

            return View(course);
        }

        // POST: Courses/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Course course)
        {
            if (!ModelState.IsValid)
                return View(course);

            course.UserId = UserId;
            await _courseService.UpdateAsync(course);

            return RedirectToAction("Index");
        }

        // GET: Courses/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseService.GetByIdAsync(id, UserId);

            if (course == null)
                return NotFound();

            return View(course);
        }

        // POST: Courses/Delete
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _courseService.GetByIdAsync(id, UserId);

            if (course != null)
                await _courseService.DeleteAsync(course);

            return RedirectToAction("Index");
        }
    }
}