using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakalauras.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bakalauras.Pages.Testai
{
    public class TestStartModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public TestStartModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public Guid? id { get; set; }

        public async Task<IActionResult> OnGet(Guid? id)
        {
            if (id.HasValue)
            {

            }

            return Page();
        }
    }
}
