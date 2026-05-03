using Microsoft.EntityFrameworkCore;
using StudyBuddyFinal.Models;
using StudyBuddyFinal1.Data;

namespace StudyBuddyFinal.Services
{
    // Handles all database operations related to Courses
    public class CourseService
    {
        private readonly ApplicationDbContext _context;

        // Constructor to get database context
        public CourseService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all courses for a specific user
        public async Task<List<Course>> GetAllAsync(string? userId)
        {
            return await _context.Courses
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        // Get a single course by ID for a specific user
        public async Task<Course?> GetByIdAsync(int id, string? userId)
        {
            return await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
        }

        // Add a new course to the database
        public async Task AddAsync(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        // Update an existing course
        public async Task UpdateAsync(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        // Delete a course
        public async Task DeleteAsync(Course course)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }
    }
}