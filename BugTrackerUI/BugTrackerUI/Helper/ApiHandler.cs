using BugTrackerUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
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

        public static async Task<bool> PostAppName(IHttpClientFactory httpClient, string applicationName)
        {
            string url = "";

            if (applicationName != null)
                url = ($"https://localhost:7240/api/error/addApplication/applicationName");

            //var applilcationModelName = new BugTrackerUI.Models.ViewModels.ApplicationNameInputModel();

            var applicationJson = new StringContent(System.Text.Json.JsonSerializer.Serialize(applicationName), Encoding.UTF8, Application.Json);

            //var request = new HttpRequestMessage(HttpMethod.Post, url);

            var client = httpClient.CreateClient();


            using var response = await client.PostAsync(url, applicationJson);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Succeed");
                return true;
            }
            else
            {
                Console.WriteLine("Error");
                return true;
            }
        }

        public static async void AddApplication(IHttpClientFactory httpClient, ApplicationViewModel application)
        {
            string url = "";

            if(application != null)
                url = ($"https://localhost:7240/api/error/application");

            var applicationJson = new StringContent(System.Text.Json.JsonSerializer.Serialize(application), Encoding.UTF8, Application.Json);

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            var client = httpClient.CreateClient();


            using var response = await client.PostAsync(url, applicationJson);
            
        }

        public static async Task<IEnumerable<ApplicationViewModel>> GetApplications(IHttpClientFactory httpClient, string userId)
        {
            List<ApplicationViewModel> applications = new List<ApplicationViewModel>();
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7240/api/error/{userId}");

            var client = httpClient.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                applications = await response.Content.ReadFromJsonAsync<List<ApplicationViewModel>>();
                return applications;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
            
        }

        public static async void AddError(IHttpClientFactory httpClient, Exception exception)
        {
            string url = "";

            if (exception != null)
                url = ($"https://localhost:7240/api/error/addError/exception");
            //var newException = new SerializableException(exception);
            var newException = JsonConvert.SerializeObject(exception);
            //var applilcationModelName = new BugTrackerUI.Models.ViewModels.ApplicationNameInputModel();
            //string json = JsonSerializer.Serialize(newException);
            var applicationJson = new StringContent(newException, Encoding.UTF8, Application.Json);

            //var request = new HttpRequestMessage(HttpMethod.Post, url);

            var client = httpClient.CreateClient();


            using var response = await client.PostAsync(url, applicationJson);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Succeed");
                //return true;
            }
            else
            {
                Console.WriteLine("Error");
                //return true;
            }
        }
    }
}
