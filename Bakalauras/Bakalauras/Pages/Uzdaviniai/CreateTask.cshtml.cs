using System;
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

        public async Task<IActionResult> OnPostCreate(string plainText)
        {
            _Task.Id = Guid.NewGuid();
            _Task.TaskString = plainText;

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
                // Deserialize response and add to ViewData
                var result = await response.Content.ReadAsStringAsync();
                var deserializedResult = JsonConvert.DeserializeObject<JObject>(result);
                var pods = deserializedResult["queryresult"]["pods"];
                podsItems = new List<API_Pods>();

                foreach (var pod in pods)
                {
                    var subpods = pod["subpods"];
                    foreach (var sub in subpods)
                    {
                        API_Pods item = new API_Pods();

                        item.imgId = (string)pod["id"];
                        item.Title = (string)pod["title"];
                        item.Alt = (string)sub["img"]["alt"];
                        item.ImgUrl = (string)sub["img"]["src"];
                        item.ImgHeight = (string)sub["img"]["height"] + "px";
                        item.ImgWidth = (string)sub["img"]["width"] + "px";
                        item.IsSaved = false;

                        podsItems.Add(item);
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
            // Return view with results or error message
            return Page();
        }
    }
}
