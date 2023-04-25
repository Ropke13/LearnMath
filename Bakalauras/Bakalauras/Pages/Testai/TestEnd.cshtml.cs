using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Bakalauras.Data;
using Bakalauras.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Bakalauras.Pages.Testai
{
    public class TestEndModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public TestEndModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public Guid? id { get; set; }
        public int totalCorrectAnswers { get; set; }
        public Test Test { get; set; }
        public TestComplete Complete { get; set; }
        public List<_Task> Tasks { get; set; }
        public List<TestCompletedTask> completedTasks { get; set; }

        public async Task<IActionResult> OnGet(Guid? id)
        {
            Test = await _db.Test.FindAsync(id);

            Complete = await _db.TestComplete
                                .Where(f => f.fk__Test == id && f.Finished != null)
                                .OrderByDescending(f => f.Finished)
                                .FirstOrDefaultAsync();

            byte[] tasksData = HttpContext.Session.Get("tasksData");
            Tasks = JsonSerializer.Deserialize<List<_Task>>(tasksData);

            completedTasks = await _db.TestCompletedTask.Where(f => f.fk__TestComplete == Complete.Id).ToListAsync();

            totalCorrectAnswers = 0;

            foreach (var item in completedTasks)
            {
                if (item.IsAnsweredCorrectly) totalCorrectAnswers++;
            }

            return Page();
        }
    }
}
