using BugTrackerCore.Interfaces;
//using Microsoft.AspNetCore.Mvc;

namespace BugTrackerCore.Models
{
    public class BugTrackerDbRepository : IDbRepository
    {
        private readonly BugTrackerDbContext _dbContext;
        public BugTrackerDbRepository(BugTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public void AddApplication(Application applicationToAdd)
        {
            
             _dbContext.Application.Add(applicationToAdd);
             _dbContext.SaveChanges();
            
            
        }

        public async void AddError(Error exception)
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
            /*OutErrors.ForEach(outerErrors =>
            {
                if (!errors.Any(x => x.ApplicationId == outerErrors.ApplicationId && x.ErrorId == outerErrors.ErrorId && x.MethodName == outerErrors.MethodName))
                {
                    errors.Add(outerErrors);
                    AddError(outerErrors);
                }
            });*/


            /*foreach (var error in OutErrors)
            {
                //Check if any erros in the database match these conditions: ApplicationId, ErrorId and MethodName
                if (errors.Any(x => x.ApplicationId == error.ApplicationId && x.ErrorId == error.ErrorId && x.MethodName == error.MethodName))
                    continue;
                errors.Add(error);
                AddError(error);
            }*/

            return errors;
        }

        public List<Application> GetApplications(string userId, IEnumerable<Application> outApplications)
        {
            var applications = _dbContext.Application.Where(id => id.UserId == userId).ToList();

            //Loops through outer application List.
            //Checks to see if any outApplication Exists in the database, if not add to database 

            foreach(var outerApplication in outApplications)
            {
                //adds application to applications list if it doesn't exist
                if (!applications.Any(apps => apps.UserId == outerApplication.UserId && apps.ApplicationName == outerApplication.ApplicationName))
                {
                    applications.Add(outerApplication);
                    AddApplication(outerApplication);
                }
            }
            /*outApplications.ToList().ForEach(outerApplication =>
            {
                //adds application to applications list if it doesn't exist
                if (!applications.Any(apps => apps.UserId == outerApplication.UserId && apps.ApplicationName == outerApplication.ApplicationName))
                {
                    applications.Add(outerApplication);
                    AddApplication(outerApplication);
                }
            });*/
           
            return applications;
        }

        public void UpdateApplication(Application application)
        {
            throw new NotImplementedException();
        }

        public void UpdateError(Error exception)
        {
            _dbContext.Error.Update(exception);
            _dbContext.SaveChanges();
        }

        public Application GetApplication(string userId, Guid appId)
        {
            //var currentApp = _dbContext.Application.FirstOrDefault(app => app.ApplicationId == appId && app.UserId == userId);
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
