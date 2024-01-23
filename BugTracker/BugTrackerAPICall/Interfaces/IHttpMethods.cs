
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
        public void PostUserId(string userId);
        public Task<bool> PostAppName(string applicationName);
        public void AddApplication(IApplication application);
        public Task<IEnumerable<Application>> GetApplications(string userId);
        public Task<bool> AddError(Exception exception, string applicationName, string callerMethod);
        public Task<List<Error>> GetErrors(Guid applicationId);
        public void UpdateCompletionStatus(CompletedModel completed);
        public Task<bool> TestHttpClient();
    }
}
