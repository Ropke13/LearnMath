using Bakalauras.Data;
using Bakalauras.Models;
using Microsoft.AspNetCore.Identity;
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
    public class MyTestsListModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public MyTestsListModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public IEnumerable<Test> Test { get; set; }

        public async Task<IActionResult> OnGet(string userId = null)
        {
            if (userId == null)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                userId = claim.Value;
            }

            Test = await _db.Test.Where(f => f.fk__User == userId).ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostDelete(Guid Id)
        {
            var del = await _db.Test.FindAsync(Id);

            if (del == null)
            {
                return NotFound();
            }

            _db.Test.Remove(del);

            await _db.SaveChangesAsync();

            return RedirectToAction("OnGet");
        }
    }
}
