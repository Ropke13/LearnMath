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
    public class MyTestsListModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public MyTestsListModel(ApplicationDbContext db)
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
