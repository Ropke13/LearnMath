using Bakalauras.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Bakalauras.Pages.Uzdaviniai
{
    [Authorize(Roles = SD.TeachUser + ", " + SD.AdminUser)]
    public class EditTaskModel : PageModel
    {
        public IActionResult OnGet(string userId = null)
        {
            if (userId == null)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                if (claim == null)
                {
                    return RedirectToPage("/Account/Login", new { area = "Identity" });
                }

                return Page();
            }

            return Page();
        }
    }
}
