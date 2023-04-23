using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakalauras.Data;
using Bakalauras.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Bakalauras.Pages.Testai
{
    public class CreateTestModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public CreateTestModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public List<_Task> _Tasks { get; set; }

        [BindProperty]
        public Test Test { get; set; }

        public async Task<IActionResult> OnGet()
        {
            //if (HttpContext.Session.GetString("id") == null)
            //{
            //    return RedirectToPage("/Login");
            //}

            //else if (HttpContext.Session.GetString("role") != "Administratorius")
            //{
            //    return RedirectToPage("/Login");
            //}

            _Tasks = await _db._Task.ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostCreate(List<_Task> _Tasks)
        {
            Test.Id = Guid.NewGuid();
            Test.TestCode = GenerateRandomCode();

            for (int i = 0; i < _Tasks.Count(); i++)
            {
                string name = "chkl_" + _Tasks[i].Id;
                var answer = Request.Form[name];

                if (answer.Count == 1)
                {
                    TestTasks testTask = new TestTasks();

                    testTask.Id = Guid.NewGuid();
                    testTask.fk__Test = Test.Id;
                    testTask.fk__Task = _Tasks[i].Id;

                    await _db.TestTasks.AddAsync(testTask);
                }
            }

            await _db.Test.AddAsync(Test);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Testai/MyTestsList");
        }

        public string GenerateRandomCode()
        {
            Random rand = new Random();
            const string digits = "0123456789";
            char[] code = new char[10];

            for (int i = 0; i < code.Length; i++)
            {
                code[i] = digits[rand.Next(digits.Length)];
            }

            return new string(code);
        }
    }
}
