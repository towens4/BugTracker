using BugTrackerAPICall.Interfaces;
using BugTrackerAPICall.Models;
using BugTrackerAPICall.Interfaces;
//using Microsoft.AspNetCore.Mvc;

namespace BugTrackerAPICall.Models
{
    public class BugTrackerDbRepository : IDbRepository
    {
        private readonly BugTrackerDbContext _dbContext;
        public BugTrackerDbRepository(BugTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public void AddApplication(IApplication applicationToAdd)
        {
            
             _dbContext.Application.Add((Application)applicationToAdd);
             _dbContext.SaveChanges();
            
            
        }

        public async void AddError(IError exception)
        {
            bool exists = _dbContext.Error.Any(error => error.ErrorId == exception.ErrorId &&
            error.ApplicationId == exception.ApplicationId && error.MethodName == exception.MethodName);

            
            if (!exists)
            {
                
                
                _dbContext.Error.Add((Error)exception);
                _dbContext.SaveChanges();
            }
            
            return;
            
        }

        public List<Error> GetErrors(Guid appId, IEnumerable<Error> OutErrors)
        {
            
            var errors = _dbContext.Error.Where(x => x.ApplicationId == appId).ToList();

            foreach(var outerErrors in OutErrors)
            {
                if (!errors.Any(x => x.ApplicationId == outerErrors.ApplicationId && x.ErrorId == outerErrors.ErrorId && x.MethodName == outerErrors.MethodName))
                {
                    errors.Add(outerErrors);
                    AddError(outerErrors);
                }
            }

            return errors;
        }

        public List<Application> GetApplications(string userId, IEnumerable<IApplication> outApplications)
        {
            var applications = _dbContext.Application.Where(id => id.UserId == userId).ToList();

            //Loops through outer application List.
            //Checks to see if any outApplication Exists in the database, if not add to database 

            foreach(var outerApplication in outApplications)
            {
                //adds application to applications list if it doesn't exist
                if (!applications.Any(apps => apps.UserId == outerApplication.UserId && apps.ApplicationName == outerApplication.ApplicationName))
                {
                    applications.Add((Application)outerApplication);
                    AddApplication(outerApplication);
                }
            }
           
            return applications;
        }

        public void UpdateApplication(IApplication application)
        {
            throw new NotImplementedException();
        }

        public void UpdateError(IError exception)
        {
            _dbContext.Error.Update((Error)exception);
            _dbContext.SaveChanges();
        }

        public Application GetApplication(string userId, Guid appId)
        {
            
            return _dbContext.Application.FirstOrDefault(app => app.ApplicationId == appId && app.UserId == userId);
        }

        public Application GetApplicationByName(string userId, string appName)
        {
            return _dbContext.Application.FirstOrDefault(app => app.UserId == userId && app.ApplicationName == appName);
        }

        public Error GetError(Guid errorId)
        {
            return _dbContext.Error.FirstOrDefault(error => error.ErrorId == errorId);
        }
    }
}
