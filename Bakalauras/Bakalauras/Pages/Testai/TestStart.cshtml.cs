using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakalauras.Data;
using Bakalauras.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Bakalauras.Pages.Testai
{
    public class TestStartModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public TestStartModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public Guid? id { get; set; }

        [BindProperty]
        public IEnumerable <TestComplete> TestC { get; set; }
        public Test Test { get; set; }
        public TestComplete Complete { get; set; }
        public List<_Task> Tasks { get; set; }

        public async Task<IActionResult> OnGet(Guid? id)
        {
            TestC = await _db.TestComplete.Where(f => f.fk__Test == id).ToListAsync();
            Test = await _db.Test.FindAsync(id);
            Tasks = await _db.TestTasks
                            .Where(tt => tt.fk__Test == id)
                            .Select(tt => tt._Task).ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostStart(Guid id)
        {
            Complete = new TestComplete();
            Tasks = await _db.TestTasks
                            .Where(tt => tt.fk__Test == id)
                            .Select(tt => tt._Task).ToListAsync();

            Test = await _db.Test.FindAsync(id);

            Complete.Id = Guid.NewGuid();
            Complete.Started = DateTime.Now;
            Complete.TotalTasks = Tasks.Count();
            Complete.fk__Test = id;

            await _db.TestComplete.AddAsync(Complete);

            byte[] tasksData = JsonSerializer.SerializeToUtf8Bytes(Tasks);

            HttpContext.Session.Set("tasksData", tasksData);
            HttpContext.Session.SetInt32("index", 0);

            await _db.SaveChangesAsync();

            return RedirectToPage("/Mokytis/SolveTask", new { id = Test.Id });
        }
    }
}
