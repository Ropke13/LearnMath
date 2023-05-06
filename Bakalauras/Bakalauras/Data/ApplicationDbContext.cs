using Bakalauras.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Bakalauras.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<_Task> _Task { get; set; }
        public DbSet<API_Pods> API_Pods { get; set; }
        public DbSet<Test> Test { get; set; }
        public DbSet<TestComplete> TestComplete { get; set; }
        public DbSet<TestTasks> TestTasks { get; set; }
        public DbSet<TestCompletedTask> TestCompletedTask { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
