using Bakalauras.Data;
using Bakalauras.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bakalauras.Pages.Mokytis
{
    public class ThemeMenuModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public ThemeMenuModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public string Title { get; set; }
        public string Text { get; set; }

        [BindProperty]
        public List<_Task> Tasks { get; set; }

        public IActionResult OnGet()
        {
            Title = Request.Query["Title"];
            Text = Request.Query["Text"];

            return Page();
        }
        public async Task<IActionResult> OnPostStart(string Title, string userId = null)
        {
            if (userId == null)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                userId = claim.Value;
            }

            Tasks = await _db._Task.Where(f => f.IsPublic && f.Theme == Title).ToListAsync();

            if(Tasks.Count() == 0) return RedirectToPage("/Index");

            byte[] tasksData = JsonSerializer.SerializeToUtf8Bytes(Tasks);

            HttpContext.Session.Set("tasksData", tasksData);
            HttpContext.Session.SetInt32("index", 0);

            return RedirectToPage("/Mokytis/SolveTask", new { id = Guid.Empty, fromPublic = true });
        }
    }
}
