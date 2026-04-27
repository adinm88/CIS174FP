using Microsoft.EntityFrameworkCore;
using CIS174FP.Models;

namespace CIS174FP.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<StudySession> StudySessions { get; set; }
    }
}
