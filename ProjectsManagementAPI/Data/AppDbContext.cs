using Microsoft.EntityFrameworkCore;
using ProjectsManagementAPI.Models;

namespace ProjectsManagementAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {
        }

        public DbSet<UserModel> Users { get; set; }  
        public DbSet<ProjectModel> Projects { get; set; }  
        public DbSet<TaskModel> Tasks { get; set; }  
    }
}
