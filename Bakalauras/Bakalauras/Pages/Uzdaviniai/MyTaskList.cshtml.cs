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

namespace Bakalauras.Pages.Uzdaviniai
{
    [Authorize(Roles = SD.TeachUser + ", " + SD.AdminUser)]
    public class MyTaskListModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public MyTaskListModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public IEnumerable<_Task> _Task { get; set; }

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

                userId = claim.Value;
            }

            _Task = await _db._Task.Where(f => f.fk__User == userId && f.IsActive).ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostDelete(Guid Id)
        {
            var del = await _db._Task.FindAsync(Id);

            if (del == null)
            {
                return NotFound();
            }

            del.IsActive = false;

            await _db.SaveChangesAsync();

            return RedirectToAction("OnGet");
        }
    }
}
