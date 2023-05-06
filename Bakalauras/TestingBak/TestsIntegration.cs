using Bakalauras.Data;
using Bakalauras.Models;
using Bakalauras.Pages.Testai;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestingBak
{
    public class Tests
    {
        [Test]
        public async Task GettingTasks()
        {
            string userIrd = "ee1694e5-1884-4a16-b043-47cf59c5d27a";
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlServer("Server=DESKTOP-QCV6S39;Database=Bakis;Trusted_Connection=True;");

            using (var dbContext = new ApplicationDbContext(optionsBuilder.Options))
            {
                await dbContext.Database.EnsureCreatedAsync();

                var pageModel = new CreateTestModel(dbContext);
                var test = await pageModel.OnGet(userIrd);

                Assert.IsTrue(pageModel._Tasks.Count() > 0); ;
            }
        }

        [Test]
        public async Task GettingTests()
        {
            string userIrd = "ee1694e5-1884-4a16-b043-47cf59c5d27a";
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseSqlServer("Server=DESKTOP-QCV6S39;Database=Bakis;Trusted_Connection=True;");

            using (var dbContext = new ApplicationDbContext(optionsBuilder.Options))
            {
                await dbContext.Database.EnsureCreatedAsync();

                var pageModel = new MyTestsListModel(dbContext);
                var test = await pageModel.OnGet(userIrd);

                Assert.IsTrue(pageModel.Test.Count() > 0);
            }
        }
    }
}