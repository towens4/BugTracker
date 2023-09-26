using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using BugTrackerAPICall.Models;
using Newtonsoft.Json;
using System.Collections;
using BugTrackerCore.Models;
using BugTrackerCore.Interfaces;
using System.Web;
using BugTrackerCore;

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

        public async void AddApplication(IHttpClientFactory httpClient, ApplicationViewModel application)
        {
            string url = "";

            if (application != null)
                url = ($"https://localhost:7240/api/error/application");

            var applicationJson = new StringContent(System.Text.Json.JsonSerializer.Serialize(application), Encoding.UTF8, System.Net.Mime.MediaTypeNames.Application.Json);

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            var client = httpClient.CreateClient();


            using var response = await client.PostAsync(url, applicationJson);

        }

        public async Task<IEnumerable<ApplicationViewModel>> GetApplications(IHttpClientFactory httpClient, string userId)
        {
            List<ApplicationViewModel> applications = new List<ApplicationViewModel>();
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7240/api/error/getApplications/{userId}");

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

        public async Task<bool> AddError(IHttpClientFactory httpClient, Exception exception, string applicationName, string callerMethod)
        {
            


            string url = "";

            if (exception != null)
                url = ($"https://localhost:7240/api/error/addError/exception");
            //var newException = new SerializableException(exception);
            var newException = JsonConvert.SerializeObject(exception);
            //var applilcationModelName = new BugTrackerUI.Models.ViewModels.ApplicationNameInputModel();
            //string json = JsonSerializer.Serialize(newException);
            var error = Helper.ConvertToErrorObject.Convert(exception);
            error.MethodName = callerMethod;
            var postModel = new ErrorPostModel()
            {
                ErrorModel = error,
                ApplicationName = applicationName
            };
            //var applicationJson = new StringContent(newException, Encoding.UTF8, System.Net.Mime.MediaTypeNames.Application.Json);
            var applicationJson = new StringContent(System.Text.Json.JsonSerializer.Serialize(postModel), Encoding.UTF8, System.Net.Mime.MediaTypeNames.Application.Json);

            //var request = new HttpRequestMessage(HttpMethod.Post, url);
            
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

        public async Task<List<ErrorViewModel>> GetErrors(IHttpClientFactory httpClient, Guid applicationId)
        {
           List<ErrorViewModel> errors = new List<ErrorViewModel>();
            
            //var userId = idHolder.UserId;
            string baseUrl = $"https://localhost:7240/api/error/getErrors/{applicationId}";
            
            //string queryString = $"?applicationId={HttpUtility.UrlEncode(idHolder.ApplicationId.ToString())}&userId={HttpUtility.UrlEncode(idHolder.UserId)}";
            
            

            var request = new HttpRequestMessage(HttpMethod.Get, baseUrl);

            var client = httpClient.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                errors = await response.Content.ReadFromJsonAsync<List<ErrorViewModel>>();
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
    }
}
