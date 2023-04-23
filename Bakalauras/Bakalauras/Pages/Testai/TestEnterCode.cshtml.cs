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
    public class TestEnterCodeModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public TestEnterCodeModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Test Test { get; set; }

        [BindProperty]
        public string Code { get; set; }
        [BindProperty]
        public string Pass { get; set; }

        public IEnumerable<Test> Atsiskaitymas { get; set; }

        public async Task<IActionResult> OnPostEnter()
        {
            Test = await _db.Test.Where(f => f.TestCode == Code && f.Password == Pass).FirstOrDefaultAsync();

            if(Test != null)
            {
                return RedirectToPage("/Testai/TestStart", new { id = Test.Id });
            }
            else
            {
                return RedirectToPage("/Testai/MyTestsList");
            }
        }
    }
}
