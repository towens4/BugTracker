using BugTrackerAPICall.Interfaces;
using BugTrackerAPICall.Models;


namespace BugTrackerAPICall.Interfaces
{
    public interface IDbRepository
    {
        List<Error> GetErrors(Guid appId, IEnumerable<Error> errors);
        List<Application> GetApplications(string userId,IEnumerable<IApplication> applications);
        Application GetApplication(string userId, Guid appId);
        Application GetApplicationByName(string userId, string appName);
        Error GetError(Guid errorId);
        void AddApplication(IApplication applicationName);
        void AddError(IError exception);
        void UpdateApplication(IApplication application);
        void UpdateError(IError exception);
    }
}
