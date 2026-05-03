using Microsoft.EntityFrameworkCore;
using StudyBuddyFinal.Models;
using StudyBuddyFinal1.Data;

namespace StudyBuddyFinal.Services
{
    // Handles all database operations related to Study Sessions
    public class StudySessionService
    {
        private readonly ApplicationDbContext _context;
        // Constructor to get database context
        public StudySessionService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all study sessions for a user
        public async Task<List<StudySession>> GetAllAsync(string? userId)
        {
            return await _context.StudySessions
                .Where(s => s.UserId == userId)
                .ToListAsync();
        }

        // Get 1 study session by ID
        public async Task<StudySession?> GetByIdAsync(int id, string? userId)
        {
            return await _context.StudySessions
                .FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);
        }

        // Add a new study session
        public async Task AddAsync(StudySession session)
        {
            _context.StudySessions.Add(session);
            await _context.SaveChangesAsync();
        }

        // Update an existing study session
        public async Task UpdateAsync(StudySession session)
        {
            _context.StudySessions.Update(session);
            await _context.SaveChangesAsync();
        }

        // Delete a study session
        public async Task DeleteAsync(StudySession session)
        {
            _context.StudySessions.Remove(session);
            await _context.SaveChangesAsync();
        }
    }
}