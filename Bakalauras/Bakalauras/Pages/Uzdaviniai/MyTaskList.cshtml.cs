using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakalauras.Data;
using Bakalauras.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Bakalauras.Pages.Uzdaviniai
{
    public class MyTaskListModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public MyTaskListModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public IEnumerable<_Task> _Task { get; set; }
        public async Task<IActionResult> OnGet()
        {
            _Task = await _db._Task.ToListAsync();

            return Page();
        }
    }
}
