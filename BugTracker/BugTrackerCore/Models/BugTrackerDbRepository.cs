using BugTrackerCore.Interfaces;
//using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BugTrackerCore.Models
{
    public class BugTrackerDbRepository : IDbRepository
    {
        private readonly BugTrackerDbContext _dbContext;
        public BugTrackerDbRepository(BugTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public void AddApplication(Application applicationName)
        {

            _dbContext.Application.Add(applicationName);
            _dbContext.SaveChanges();
        }

        public void AddError(Error exception)
        {
            bool exists = _dbContext.Error.Any(error => error.ErrorId == exception.ErrorId &&
            error.ApplicationId == exception.ApplicationId && error.MethodName == exception.MethodName);

            if (!exists)
            {
                _dbContext.Error.Add(exception);
                _dbContext.SaveChanges();
            }
            
            return;
            
        }

        public List<Error> GetErrors(Guid appId,List<Error> OutErrors)
        {
            
            var errors = _dbContext.Error.Where(x => x.ApplicationId == appId).ToList();

            foreach (var error in OutErrors)
            {
                //Check if any erros in the database match these conditions: ApplicationId, ErrorId and MethodName
                if (errors.Any(x => x.ApplicationId == error.ApplicationId && x.ErrorId == error.ErrorId && x.MethodName == error.MethodName))
                    continue;
                errors.Add(error);
                AddError(error);
            }

            return errors is null ? null : errors;
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

        public void UpdateError(Error exception)
        {
            throw new NotImplementedException();
        }

        public Application GetApplication(string userId, Guid appId)
        {
            var currentApp = _dbContext.Application.FirstOrDefault(app => app.ApplicationId == appId && app.UserId == userId);
            return currentApp;
        }
    }
}
