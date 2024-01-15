
using BugTrackerAPICall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerAPICall.Interfaces
{
    public interface IHttpMethods
    {
        public void PostUserId(IHttpClientFactory httpClient, string userId);
        public Task<bool> PostAppName(IHttpClientFactory httpClient, string applicationName);
        public void AddApplication(IHttpClientFactory httpClient, IApplication application);
        public Task<IEnumerable<Application>> GetApplications(IHttpClientFactory httpClient, string userId);
        public Task<bool> AddError(IHttpClientFactory httpClient, Exception exception, string applicationName, string callerMethod);
        public Task<List<Error>> GetErrors(IHttpClientFactory httpClient, Guid applicationId);
        public void UpdateCompletionStatus(IHttpClientFactory httpClient, CompletedModel completed);
    }
}
