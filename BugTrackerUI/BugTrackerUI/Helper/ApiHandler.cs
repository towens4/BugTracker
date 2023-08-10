using BugTrackerUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace BugTrackerUI.Helper
{
    public class ApiHandler
    {

        public async Task<IEnumerable<ApplicationViewModel>> LoadApplications(string userId)
        {
            string url = "";

            if(!userId.Equals(null))
            {
                url = $"https://localhost:44379/api/error/{userId}";
            }

            using(HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if(response.IsSuccessStatusCode)
                {
                    List<ApplicationViewModel> applications = await response.Content.ReadAsAsync<List<ApplicationViewModel>>();
                    return applications;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

            
        }

        public static async void AddApplication(IHttpClientFactory httpClient, ApplicationViewModel application)
        {
            string url = "";

            if(application != null)
                url = ($"https://localhost:7240/api/error/application");

            var applicationJson = new StringContent(JsonSerializer.Serialize(application), Encoding.UTF8, Application.Json);

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            var client = httpClient.CreateClient();


            using var response = await client.PostAsync(url, applicationJson);
            return;
        }
    }
}
