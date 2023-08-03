using BugTracker.Models;

namespace BugTracker.Interfaces
{
    public interface IDbRepository
    {
        List<Error> GetErrors(Guid appId);
        List<Application> GetApplications(string userId);
        void AddApplication(Application applicationName);
        void AddError(Exception exception);
        void UpdateApplication(Application application);
        void UpdateError(Exception exception);
    }
}
