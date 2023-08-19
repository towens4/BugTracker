using BugTracker.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BugTracker.Models
{
    public class BugTrackerDbRepository : IDbRepository
    {
        private readonly BugTrackerDbContext _dbContext;
        public BugTrackerDbRepository(BugTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public void AddApplication([FromBody]Application applicationName)
        {

            _dbContext.Application.Add(applicationName);
            _dbContext.SaveChanges();
        }
        
        public void AddError(Exception exception)
        {
            Error error = new Error();
            {

            }
        }

        public List<Error> GetErrors(Guid appId)
        {
            throw new NotImplementedException();
        }

        public List<Application> GetApplications(string userId, List<Application> outApplications)
        {
            var applications = _dbContext.Application.Where(id => id.UserId == userId).ToList();
            

            foreach(var application in outApplications)
            {
                if (applications.Any(apps => apps.UserId == application.UserId && apps.ApplicationName == application.ApplicationName))
                    continue;
                applications.Add(application);
                AddApplication(application);
            }
            return applications is null ? null : applications;
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
