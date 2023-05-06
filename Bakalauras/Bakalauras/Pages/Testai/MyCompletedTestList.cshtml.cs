using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Bakalauras.Data;
using Bakalauras.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Bakalauras.Pages.Testai
{
    public class MyCompletedTestListModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public MyCompletedTestListModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<TestComplete> Completes { get; set; }
        public List<TestCompletedTask> completedTasks { get; set; }

        public async Task<IActionResult> OnGet(string userId = null)
        {
            if (userId == null)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                userId = claim.Value;
            }

            Completes = await _db.TestComplete.Where(f => f.fk__User == userId && f.Finished != null)
                .Include(f => f.User)
                .Include(f => f.Test).ToListAsync();

            completedTasks = await _db.TestCompletedTask.ToListAsync();

            return Page();
        }
    }
}
