using BugTrackerCore.Models;

namespace BugTrackerCore.Interfaces
{
    public interface IDbRepository
    {
        List<Error> GetErrors(Guid appId, List<Error> errors);
        List<Application> GetApplications(string userId,List<Application> applications);
        Application GetApplication(string userId, Guid appId);
        void AddApplication(Application applicationName);
        void AddError(Error exception);
        void UpdateApplication(Application application);
        void UpdateError(Error exception);
    }
}
