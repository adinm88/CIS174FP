using Microsoft.EntityFrameworkCore;
using StudyBuddyFinal.Models;
using StudyBuddyFinal1.Data;

namespace StudyBuddyFinal.Services
{
    // Handles all database operations related to Assignments
    public class AssignmentService
    {
        private readonly ApplicationDbContext _context;
        // Constructor for database context
        public AssignmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all assignments for a user + the related courses
        public async Task<List<Assignment>> GetAllAsync(string? userId)
        {
            return await _context.Assignments
                .Include(a => a.Course)
                .Where(a => a.UserId == userId)
                .ToListAsync();
        }

        // Get 1 assignment by ID
        public async Task<Assignment?> GetByIdAsync(int id, string? userId)
        {
            return await _context.Assignments
                .Include(a => a.Course)
                .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
        }

        // Add a new assignment
        public async Task AddAsync(Assignment assignment)
        {
            _context.Assignments.Add(assignment);
            await _context.SaveChangesAsync();
        }

        // Update an existing assignment
        public async Task UpdateAsync(Assignment assignment)
        {
            _context.Assignments.Update(assignment);
            await _context.SaveChangesAsync();
        }

        // Delete an assignment
        public async Task DeleteAsync(Assignment assignment)
        {
            _context.Assignments.Remove(assignment);
            await _context.SaveChangesAsync();
        }
    }
}