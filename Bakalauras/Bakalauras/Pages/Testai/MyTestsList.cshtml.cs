using Bakalauras.Data;
using Bakalauras.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bakalauras.Pages.Testai
{
    public class MyTestsListModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public MyTestsListModel(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
        }

        [BindProperty]
        public IEnumerable<Test> Test { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Test = await _db.Test.ToListAsync();

            return Page();
        }
    }
}
