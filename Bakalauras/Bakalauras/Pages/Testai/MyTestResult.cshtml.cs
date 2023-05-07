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
    public class MyTestResultModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public MyTestResultModel(ApplicationDbContext db)
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
            Complete = await _db.TestComplete.FindAsync(id);

            Test = await _db.Test.FindAsync(Complete.fk__Test);

            completedTasks = await _db.TestCompletedTask.Where(f => f.fk__TestComplete == Complete.Id).ToListAsync();

            Tasks = await _db.TestTasks
                            .Where(tt => tt.fk__Test == Test.Id)
                            .Select(tt => tt._Task).ToListAsync();

            totalCorrectAnswers = 0;

            foreach (var item in completedTasks)
            {
                if (item.IsAnsweredCorrectly) totalCorrectAnswers++;
            }

            return Page();
        }
    }
}
