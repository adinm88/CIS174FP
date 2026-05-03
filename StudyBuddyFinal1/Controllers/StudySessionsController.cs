using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddyFinal.Models;
using StudyBuddyFinal.Services;
using System.Security.Claims;

namespace StudyBuddyFinal.Controllers
{
    [Authorize] // user must be logged in
    public class StudySessionsController : Controller
    {
        private readonly StudySessionService _service;

        public StudySessionsController(StudySessionService service)
        {
            _service = service;
        }

        // get current user id
        private string? UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);

        // show all sessions
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.SetString("LastPage", "Study Sessions");
            return View(await _service.GetAllAsync(UserId));
        }

        // show create page
        public IActionResult Create()
        {
            return View();
        }

        // save new session
        [HttpPost]
        public async Task<IActionResult> Create(StudySession session)
        {
            if (!ModelState.IsValid) return View(session);

            session.UserId = UserId;
            await _service.AddAsync(session);

            return RedirectToAction("Index");
        }

        // show edit page
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _service.GetByIdAsync(id, UserId);
            return item == null ? NotFound() : View(item);
        }

        // save edit
        [HttpPost]
        public async Task<IActionResult> Edit(StudySession session)
        {
            if (!ModelState.IsValid) return View(session);

            session.UserId = UserId;
            await _service.UpdateAsync(session);

            return RedirectToAction("Index");
        }

        // show delete page
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _service.GetByIdAsync(id, UserId);
            return item == null ? NotFound() : View(item);
        }

        // delete session
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _service.GetByIdAsync(id, UserId);

            if (item != null)
                await _service.DeleteAsync(item);

            return RedirectToAction("Index");
        }
    }
}