using Bakalauras.Data;
using Bakalauras.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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

        public async Task<IActionResult> OnGet(string userId = null)
        {
            if (userId == null)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                if (claim == null)
                {
                    return RedirectToPage("/Account/Login", new { area = "Identity" });
                }
            }

            _Tasks = await _db._Task.Where(f => f.fk__User == userId).ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostCreate(List<_Task> _Tasks, string userId = null)
        {
            if (userId == null)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                userId = claim.Value;
            }

            Test.Id = Guid.NewGuid();
            Test.TestCode = GenerateRandomCode();
            Test.fk__User = userId;

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
