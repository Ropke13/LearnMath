using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Bakalauras.Data;
using Bakalauras.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bakalauras.Pages.Uzdaviniai
{
    public class CreateTaskModel : PageModel
    {

        private readonly ApplicationDbContext _db;

        public CreateTaskModel(ApplicationDbContext db)
        {
            _db = db;
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

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostCreate(string plainText, List<API_Pods> podsItems)
        {
            _Task.Id = Guid.NewGuid();

            for (int i = 0; i < podsItems.Count(); i++)
            {
                string name = "chkl_" + podsItems[i].Id;
                var answer = Request.Form[name];

                if(answer.Count == 1)
                {
                    podsItems[i].IsActive = true;
                }

                podsItems[i].fk__Task = _Task.Id;

                string filePath = @"C:\Users\Devina\Documents\GitHub\LearnMath\Bakalauras\Bakalauras\wwwroot\savedImages_API\" + podsItems[i].Id.ToString() + ".jpg";

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

            _Task.Explaining = Request.Form["expl"];
            _Task.Level = Request.Form["sunk"];
            _Task.Theme = Request.Form["tema"];

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

                if(pods == null)
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
