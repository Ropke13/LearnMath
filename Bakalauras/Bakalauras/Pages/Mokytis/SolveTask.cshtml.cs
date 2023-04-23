using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakalauras.Data;
using Bakalauras.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Bakalauras.Pages.Mokytis
{
    public class SolveTaskModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public SolveTaskModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public Guid? id { get; set; }

        [BindProperty]
        public Test Test { get; set; }
        [BindProperty]
        public TestComplete Complete { get; set; }

        public async Task<IActionResult> OnGet(Guid? id)
        {
            Test = await _db.Test.FindAsync(id);
            Complete = await _db.TestComplete.Where(f => f.fk__Test == id && f.Finished == null).FirstOrDefaultAsync();

            return Page();
        }
    }
}
