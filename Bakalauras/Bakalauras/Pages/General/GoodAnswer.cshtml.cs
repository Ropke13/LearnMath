using Bakalauras.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bakalauras.Pages.General
{
    public class GoodAnswerModel : PageModel
    {
        public List<_Task> Tasks { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostNext()
        {
            byte[] tasksData = HttpContext.Session.Get("tasksData");
            Tasks = JsonSerializer.Deserialize<List<_Task>>(tasksData);

            int Index = (int)HttpContext.Session.GetInt32("index");

            if (Tasks.Count() == Index)
            {
                return RedirectToPage("/Index");
            }

            return RedirectToPage("/Mokytis/SolveTask", new { id = Guid.Empty, fromPublic = true });
        }
    }
}
