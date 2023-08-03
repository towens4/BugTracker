using BugTrackerUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BugTrackerUI.Helper
{
    public class ApiHandler
    {

        public async Task<IEnumerable<Application>> LoadApplications(string userId)
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
                    List<Application> applications = await response.Content.ReadAsAsync<List<Application>>();
                    return applications;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

            
        }

        public static async void AddApplication(IHttpClientFactory httpClient, Application application)
        {
            string url = "";

            if(application != null)
                url = ($"https://localhost:44379/api/error/application");

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            var client = httpClient.CreateClient();
            //client.BaseAddress = new Uri(url);
            //HttpResponseMessage response = client.PostAsJsonAsync<Application>("application", application);
            var postTask = client.PostAsJsonAsync<Application>("application", application);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
                return;

            
        }
    }
}
