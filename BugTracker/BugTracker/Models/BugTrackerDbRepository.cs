using BugTracker.Interfaces;

namespace BugTracker.Models
{
    public class BugTrackerDbRepository : IDbRepository
    {
        public void AddApplication(string applicationName)
        {
            throw new NotImplementedException();
        }

        public void AddError(Exception exception)
        {
            throw new NotImplementedException();
        }

        public List<Error> GetErrors(string userId, Guid applicationId)
        {
            throw new NotImplementedException();
        }

        public List<Application> GetApplications(string userId)
        {
            throw new NotImplementedException();
        }

        public void UpdateApplication(Application application)
        {
            throw new NotImplementedException();
        }

        public void UpdateError(Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}
