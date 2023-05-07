using Bakalauras.Data;
using Bakalauras.Models;
using Bakalauras.Utility;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = SD.TeachUser + ", " + SD.AdminUser)]
    public class EditTestModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public EditTestModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public List<_Task> _Tasks { get; set; }

        [BindProperty]
        public Test Test { get; set; }

        [BindProperty]
        public List<TestTasks> SelectedTasks { get; set; }

        public async Task<IActionResult> OnGet(Guid Id, string userId = null)
        {
            if (userId == null)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                if (claim == null)
                {
                    return RedirectToPage("/Account/Login", new { area = "Identity" });
                }
                else userId = claim.Value;
            }
            Test = await _db.Test.FindAsync(Id);

            _Tasks = await _db._Task.Where(f => f.fk__User == userId).ToListAsync();

            SelectedTasks = await _db.TestTasks
                            .Where(tt => tt.fk__Test == Test.Id).ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostEdit()
        {
            var RegFromDb = await _db.Test.FindAsync(Test.Id);

            RegFromDb.TestName = Test.TestName;
            RegFromDb.Time = Test.Time;
            RegFromDb.From = Test.From;
            RegFromDb.To = Test.To;
            RegFromDb.Password = Test.Password;

            await _db.SaveChangesAsync();

            return RedirectToPage("/Testai/MyTestsList");
        }
    }
}
