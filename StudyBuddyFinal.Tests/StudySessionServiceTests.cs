using Microsoft.EntityFrameworkCore;
using StudyBuddyFinal1.Data;
using StudyBuddyFinal.Models;
using StudyBuddyFinal.Services;

namespace StudyBuddyFinal.Tests
{
    public class StudySessionServiceTests
    {
        // Helper method to create a new in-memory database context for testing
        private ApplicationDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }
        // Test method to verify that adding a study session works correctly
        [Fact]
        public async Task AddAsync_AddsStudySession()
        {
            var context = GetContext();
            var service = new StudySessionService(context);

            var session = new StudySession
            {
                Date = DateTime.Today,
                DurationMinutes = 60,
                Notes = "Studied math",
                UserId = "user1"
            };

            await service.AddAsync(session);

            var sessions = await service.GetAllAsync("user1");

            Assert.Single(sessions);
            Assert.Equal(60, sessions[0].DurationMinutes);
        }
        // Test method to verify that deleting a study session works correctly
        [Fact]
        public async Task DeleteAsync_RemovesStudySession()
        {
            var context = GetContext();
            var service = new StudySessionService(context);

            var session = new StudySession
            {
                Date = DateTime.Today,
                DurationMinutes = 30,
                UserId = "user1"
            };

            await service.AddAsync(session);
            await service.DeleteAsync(session);

            var sessions = await service.GetAllAsync("user1");

            Assert.Empty(sessions);
        }
    }
}