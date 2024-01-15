using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json;
using System.Collections;
using BugTrackerAPICall.Interfaces;
using System.Web;
using BugTrackerAPICall.Helper;
using BugTrackerAPICall.Models;
using Application = BugTrackerAPICall.Models.Application;

namespace BugTrackerAPICall.APICall
{
    public class ApiCallFunctions : IHttpMethods
    {


        public async Task<bool> PostAppName(IHttpClientFactory httpClient, string applicationName)
        {
            string url = "";

            if (applicationName != null)
                url = ($"https://localhost:7240/api/error/addApplication/applicationName");

            //var applilcationModelName = new BugTrackerUI.Models.ViewModels.ApplicationNameInputModel();

            var applicationJson = new StringContent(System.Text.Json.JsonSerializer.Serialize(applicationName), Encoding.UTF8, System.Net.Mime.MediaTypeNames.Application.Json);

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

        public async void AddApplication(IHttpClientFactory httpClient, IApplication application)
        {
            string url = "";

            if (application != null)
                url = ($"https://localhost:7240/api/error/application");

            var applicationJson = new StringContent(System.Text.Json.JsonSerializer.Serialize(application), Encoding.UTF8, System.Net.Mime.MediaTypeNames.Application.Json);

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            var client = httpClient.CreateClient();


            using var response = await client.PostAsync(url, applicationJson);

        }

        public async Task<IEnumerable<Application>> GetApplications(IHttpClientFactory httpClient, string userId)
        {
            List<Application> applications = new List<Application>();
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7240/api/error/getApplications/{userId}");

            var client = httpClient.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                applications = await response.Content.ReadFromJsonAsync<List<Application>>();
                return applications;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }

        }

        public async Task<bool> AddError(IHttpClientFactory httpClient, Exception exception, string applicationName, string callerMethod)
        {
            


            string url = "";

            if (exception != null)
                url = ($"https://localhost:7240/api/error/addError/exception");
            
            var newException = JsonConvert.SerializeObject(exception);
            
            var error = Helper.ConvertToErrorObject.Convert(exception);
            error.MethodName = callerMethod;
            var postModel = new ErrorPostModel()
            {
                ErrorModel = error,
                ApplicationName = applicationName
            };

            postModel.ErrorModel.FileLocation = StringExtractor.ExtractFilePath(postModel.ErrorModel.FileLocation, postModel.ApplicationName);
            
            var applicationJson = new StringContent(System.Text.Json.JsonSerializer.Serialize(postModel), Encoding.UTF8, System.Net.Mime.MediaTypeNames.Application.Json);

           
            
            var client = httpClient.CreateClient();
            client.DefaultRequestHeaders.Add("ApplicationName", applicationName);

            using var response = await client.PostAsync(url, applicationJson);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Succeed");
                return true;
            }
            else
            {
                Console.WriteLine("Error");
                return false;
            }
        }

        public async Task<List<Error>> GetErrors(IHttpClientFactory httpClient, Guid applicationId)
        {
           List<Error> errors = new List<Error>();
            
            
            string baseUrl = $"https://localhost:7240/api/error/getErrors/{applicationId}";
            
            
            
            

            var request = new HttpRequestMessage(HttpMethod.Get, baseUrl);

            var client = httpClient.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                errors = await response.Content.ReadFromJsonAsync<List<Error>>();
                return errors;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async void PostUserId(IHttpClientFactory httpClient, string userId)
        {
            //ERROR: id not adding. 505 status code

            string baseUrl = $"https://localhost:7240/api/error/addUserId/{userId}";

            var applicationJson = new StringContent(System.Text.Json.JsonSerializer.Serialize(userId), Encoding.UTF8, System.Net.Mime.MediaTypeNames.Application.Json);

            //var request = new HttpRequestMessage(HttpMethod.Post, baseUrl);

            var client = httpClient.CreateClient();


            using var response = await client.PostAsync(baseUrl, applicationJson);

        }

        public async void UpdateCompletionStatus(IHttpClientFactory httpClient, CompletedModel completed)
        {
            string url = $"https://localhost:7240/api/error/updateCompletionStatus/{completed}";

            var errorJson = new StringContent(System.Text.Json.JsonSerializer.Serialize(completed), Encoding.UTF8, System.Net.Mime.MediaTypeNames.Application.Json);

            var client = httpClient.CreateClient();
            using var response = await client.PutAsync(url, errorJson);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Success");
            }
            else
            {
                Console.WriteLine("Error");
            }
        }
    }
}
