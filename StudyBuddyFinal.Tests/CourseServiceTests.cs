using Microsoft.EntityFrameworkCore;
using StudyBuddyFinal1.Data;
using StudyBuddyFinal.Models;
using StudyBuddyFinal.Services;

namespace StudyBuddyFinal.Tests
{
    public class CourseServiceTests
    {
        // Helper method to create a new in-memory database context for testing
        private ApplicationDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }
        // Tests if AddAsync successfully adds a course to the database
        [Fact]
        public async Task AddAsync_AddsCourse()
        {
            var context = GetContext();
            var service = new CourseService(context);

            var course = new Course
            {
                CourseName = "Math",
                Instructor = "Smith",
                UserId = "user1"
            };

            await service.AddAsync(course);

            var courses = await service.GetAllAsync("user1");

            Assert.Single(courses);
            Assert.Equal("Math", courses[0].CourseName);
        }
        // Tests if GetAllAsync returns only courses for the specified user
        [Fact]
        public async Task GetAllAsync_ReturnsOnlyCurrentUsersCourses()
        {
            var context = GetContext();
            var service = new CourseService(context);

            await service.AddAsync(new Course { CourseName = "Math", UserId = "user1" });
            await service.AddAsync(new Course { CourseName = "Science", UserId = "user2" });

            var courses = await service.GetAllAsync("user1");

            Assert.Single(courses);
            Assert.Equal("Math", courses[0].CourseName);
        }
        // Tests if DeleteAsync successfully removes a course from the database
        [Fact]
        public async Task DeleteAsync_RemovesCourse()
        {
            var context = GetContext();
            var service = new CourseService(context);

            var course = new Course
            {
                CourseName = "English",
                UserId = "user1"
            };

            await service.AddAsync(course);
            await service.DeleteAsync(course);

            var courses = await service.GetAllAsync("user1");

            Assert.Empty(courses);
        }
    }
}