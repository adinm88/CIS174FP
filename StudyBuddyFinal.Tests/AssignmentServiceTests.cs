using Microsoft.EntityFrameworkCore;
using StudyBuddyFinal.Models;
using StudyBuddyFinal.Services;
using StudyBuddyFinal1.Data;

namespace StudyBuddyFinal.Tests
{
    public class AssignmentServiceTests
    {
        // Helper method to create a new in-memory database context for testing
        private ApplicationDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }
        // Test to verify that AddAsync correctly adds an assignment to the database
        [Fact]
        public async Task AddAsync_AddsAssignment()
        {
            var context = GetContext();
            var service = new AssignmentService(context);

            var course = new Course
            {
                CourseName = "Math",
                Instructor = "Smith",
                UserId = "user1"
            };

            context.Courses.Add(course);
            await context.SaveChangesAsync();

            var assignment = new Assignment
            {
                Title = "Chapter 1 Homework",
                DueDate = DateTime.Today.AddDays(7),
                CourseId = course.Id,
                UserId = "user1"
            };

            await service.AddAsync(assignment);

            var assignments = await service.GetAllAsync("user1");

            Assert.Single(assignments);
            Assert.Equal("Chapter 1 Homework", assignments[0].Title);
        }
        // Test to verify that GetAllAsync returns only the assignments for the specified user
        [Fact]
        public async Task GetAllAsync_ReturnsOnlyCurrentUsersAssignments()
        {
            var context = GetContext();
            var service = new AssignmentService(context);

            var course1 = new Course
            {
                CourseName = "Math",
                Instructor = "Smith",
                UserId = "user1"
            };

            var course2 = new Course
            {
                CourseName = "Science",
                Instructor = "Jones",
                UserId = "user2"
            };

            context.Courses.Add(course1);
            context.Courses.Add(course2);
            await context.SaveChangesAsync();

            await service.AddAsync(new Assignment
            {
                Title = "Math Homework",
                DueDate = DateTime.Today,
                CourseId = course1.Id,
                UserId = "user1"
            });

            await service.AddAsync(new Assignment
            {
                Title = "Science Homework",
                DueDate = DateTime.Today,
                CourseId = course2.Id,
                UserId = "user2"
            });

            var assignments = await service.GetAllAsync("user1");

            Assert.Single(assignments);
            Assert.Equal("Math Homework", assignments[0].Title);
        }
        // Test to verify that DeleteAsync correctly removes an assignment from the database
        [Fact]
        public async Task DeleteAsync_RemovesAssignment()
        {
            var context = GetContext();
            var service = new AssignmentService(context);

            var assignment = new Assignment
            {
                Title = "Delete Me",
                DueDate = DateTime.Today,
                CourseId = 1,
                UserId = "user1"
            };

            await service.AddAsync(assignment);
            await service.DeleteAsync(assignment);

            var assignments = await service.GetAllAsync("user1");

            Assert.Empty(assignments);
        }
    }
}