using Microsoft.AspNetCore.Identity;
using Bakalauras.Data;
using Bakalauras.Models;
using Bakalauras.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Bakalauras.Pages.Uzdaviniai
{
    [Authorize(Roles = SD.TeachUser + ", " + SD.AdminUser)]
    public class CreateTaskModel : PageModel
    {

        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateTaskModel(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }

        public string Input = "";
        [BindProperty]
        public string plainText { get; set; }

        [BindProperty]
        public List<API_Pods> podsItems { get; set; }
        [BindProperty]
        public List<_Task> task { get; set; }

        [BindProperty]
        public API_Pods API_Pod { get; set; }

        [BindProperty]
        public _Task _Task { get; set; }

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

        public async Task<IActionResult> OnPostCreate(List<API_Pods> podsItems, string userId = null)
        {
            if (userId == null)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                userId = claim.Value;
            }
            _Task.Id = Guid.NewGuid();
            _Task.fk__User = userId;

            for (int i = 0; i < podsItems.Count(); i++)
            {
                string name = "chkl_" + podsItems[i].Id;
                var answer = Request.Form[name];

                if (answer.Count == 1)
                {
                    podsItems[i].IsActive = true;
                }

                podsItems[i].fk__Task = _Task.Id;
                string fileName = podsItems[i].Id.ToString() + ".jpg";
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "savedImages_API", fileName);

                using (var client = new WebClient())
                {
                    client.DownloadFile(podsItems[i].ImgUrl, filePath);
                }

                await _db.API_Pods.AddAsync(podsItems[i]);
            }

            _Task.TaskString = Request.Form["taskString"];

            _Task.Answer1 = Request.Form["Ans1"];
            _Task.Answer2 = Request.Form["Ans2"];
            _Task.Answer3 = Request.Form["Ans3"];
            _Task.Answer4 = Request.Form["Ans4"];

            _Task.Level = Request.Form["sunk"];
            _Task.Theme = Request.Form["tema"];

            var user = await _userManager.GetUserAsync(User);
            var isAdmin = await _userManager.IsInRoleAsync(user, SD.AdminUser);

            if (isAdmin)
            {
                _Task.IsPublic = true;
            }
            else
            {
                _Task.IsPublic = false;
            }

            await _db._Task.AddAsync(_Task);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Uzdaviniai/MyTaskList");
        }

        public async Task<IActionResult> OnPostGetAPIResults(string Input, string plainText)
        {
            // Set up HttpClient
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Build request URL
            var baseUrl = "https://api.wolframalpha.com/v2/query?format=image&output=json&";
            var appId = "PRJ24L-753KK7AQYR";
            var query = HttpUtility.UrlEncode(Input); // URL-encode user input
            var url = $"{baseUrl}appid={appId}&input={query}";

            // Send request and handle response
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                // Deserialize response
                var result = await response.Content.ReadAsStringAsync();
                var deserializedResult = JsonConvert.DeserializeObject<JObject>(result);
                var pods = deserializedResult["queryresult"]["pods"];
                podsItems = new List<API_Pods>();

                if (pods == null)
                {
                    return Page();
                }
                else
                {
                    foreach (var pod in pods)
                    {
                        var subpods = pod["subpods"];
                        foreach (var sub in subpods)
                        {
                            API_Pods item = new API_Pods
                            {
                                Id = Guid.NewGuid(),
                                imgId = (string)pod["id"],
                                Title = (string)pod["title"],
                                Alt = (string)sub["img"]["alt"],
                                ImgUrl = (string)sub["img"]["src"],
                                ImgHeight = (string)sub["img"]["height"] + "px",
                                ImgWidth = (string)sub["img"]["width"] + "px",
                                IsActive = false
                            };

                            podsItems.Add(item);
                        }
                    }
                }
            }
            else
            {
                // Handle error response
                var errorMessage = $"Error calling Wolfram Alpha API: {response.ReasonPhrase}";
                ViewData["Error"] = errorMessage;
            }

            this.plainText = plainText;

            return Page();
        }
    }
}
