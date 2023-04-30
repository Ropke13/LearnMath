using Bakalauras.Data;
using Bakalauras.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bakalauras.Pages.Mokytis
{
    public class SolveTaskModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public SolveTaskModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public int Index { get; set; }

        public Guid? id { get; set; }

        [BindProperty]
        public Test Test { get; set; }
        [BindProperty]
        public TestComplete Complete { get; set; }

        [BindProperty]
        public List<_Task> Tasks { get; set; }

        [BindProperty]
        public List<string> Answers { get; set; }
        public TestCompletedTask completedTask { get; set; }
        [BindProperty]
        public List<API_Pods> Api_Pods { get; set; }

        public async Task<IActionResult> OnGet(Guid? id)
        {
            Test = await _db.Test.FindAsync(id);
            Complete = await _db.TestComplete.Where(f => f.fk__Test == id && f.Finished == null).FirstOrDefaultAsync();

            byte[] tasksData = HttpContext.Session.Get("tasksData");
            Tasks = JsonSerializer.Deserialize<List<_Task>>(tasksData);

            Index = (int)HttpContext.Session.GetInt32("index");

            Api_Pods = await _db.API_Pods.Where(f => f.fk__Task == Tasks[Index].Id && f.IsActive).ToListAsync();

            Answers = new List<string>()
            {
                Tasks[Index].Answer1,
                Tasks[Index].Answer2,
                Tasks[Index].Answer3,
                Tasks[Index].Answer4
            };

            ShuffleList(Answers);

            return Page();
        }

        public async Task<IActionResult> OnPostAnswer(Guid? id)
        {
            Complete = await _db.TestComplete.Where(f => f.fk__Test == id && f.Finished == null).FirstOrDefaultAsync();

            int currentIndex = (int)HttpContext.Session.GetInt32("index");
            HttpContext.Session.SetInt32("index", currentIndex + 1);

            byte[] tasksData = HttpContext.Session.Get("tasksData");
            Tasks = JsonSerializer.Deserialize<List<_Task>>(tasksData);

            completedTask = new TestCompletedTask();

            completedTask.Id = Guid.NewGuid();

            string selectedAnswer = Request.Form["formRadios"];

            completedTask.SelectedAnswer = selectedAnswer;

            if (selectedAnswer == Tasks[currentIndex].Answer1)
            {
                completedTask.IsAnsweredCorrectly = true;
            }
            else
            {
                completedTask.IsAnsweredCorrectly = false;
            }

            completedTask.TaskId = Tasks[currentIndex].Id;
            completedTask.fk__TestComplete = Complete.Id;

            await _db.TestCompletedTask.AddAsync(completedTask);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Mokytis/SolveTask", new { id });
        }

        public async Task<IActionResult> OnPostEndTest(Guid? id)
        {
            Complete = await _db.TestComplete.Where(f => f.fk__Test == id && f.Finished == null).FirstOrDefaultAsync();

            int currentIndex = (int)HttpContext.Session.GetInt32("index");
            HttpContext.Session.SetInt32("index", currentIndex + 1);

            byte[] tasksData = HttpContext.Session.Get("tasksData");
            Tasks = JsonSerializer.Deserialize<List<_Task>>(tasksData);

            completedTask = new TestCompletedTask();

            completedTask.Id = Guid.NewGuid();

            string selectedAnswer = Request.Form["formRadios"];

            completedTask.SelectedAnswer = selectedAnswer;

            if (selectedAnswer == Tasks[currentIndex].Answer1)
            {
                completedTask.IsAnsweredCorrectly = true;
            }
            else
            {
                completedTask.IsAnsweredCorrectly = false;
            }

            completedTask.TaskId = Tasks[currentIndex].Id;
            completedTask.fk__TestComplete = Complete.Id;

            Complete.Finished = DateTime.Now;

            await _db.TestCompletedTask.AddAsync(completedTask);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Testai/TestEnd", new { id });
        }

        public static void ShuffleList(List<string> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                string value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
