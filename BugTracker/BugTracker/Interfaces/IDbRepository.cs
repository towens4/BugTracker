using BugTracker.Models;

namespace BugTracker.Interfaces
{
    public interface IDbRepository
    {
        List<Error> GetErrors(string userId, Guid applicationId);
        List<Application> GetApplications(string userId);
        void AddApplication(string applicationName);
        void AddError(Exception exception);
        void UpdateApplication(Application application);
        void UpdateError(Exception exception);
    }
}
