﻿using BugTrackerCore.Models;

namespace BugTrackerCore.Interfaces
{
    public interface IDbRepository
    {
        List<Error> GetErrors(Guid appId, IEnumerable<Error> errors);
        List<Application> GetApplications(string userId,IEnumerable<Application> applications);
        Application GetApplication(string userId, Guid appId);
        Application GetApplicationByName(string userId, string appName);
        Error GetError(Guid errorId);
        void AddApplication(Application applicationName);
        void AddError(Error exception);
        void UpdateApplication(Application application);
        void UpdateError(Error exception);
    }
}
