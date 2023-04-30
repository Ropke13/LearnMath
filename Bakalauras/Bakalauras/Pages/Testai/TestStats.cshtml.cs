using Bakalauras.Data;
using Bakalauras.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakalauras.Pages.Testai
{
    public class TestStatsModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public TestStatsModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public Test Test { get; set; }
        public List<TestComplete> Completes { get; set; }
        public List<TestCompletedTask> completedTasks { get; set; }

        public async Task<IActionResult> OnGet(Guid? id)
        {
            Test = await _db.Test.FindAsync(id);

            Completes = await _db.TestComplete.Where(f => f.fk__Test == id && f.Finished != null).ToListAsync();

            completedTasks = await _db.TestCompletedTask.ToListAsync();

            return Page();
        }
    }
}
