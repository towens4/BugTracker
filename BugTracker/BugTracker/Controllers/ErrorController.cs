using BugTrackerAPICall.Interfaces;
using BugTrackerAPICall.Models;
using BugTrackerAPICall.Helper;
using BugTrackerAPICall.Interfaces;
using BugTrackerAPICall.Models;
using BugTrackerAPICall.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using IApplication = BugTrackerAPICall.Interfaces.IApplication;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BugTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        
        
        private readonly IDbRepository _repository;
        private readonly ILocalRepository _localRepo;
        private readonly HubConnectionManager _hubConnectionManager;

        public ErrorController(IDbRepository repository, ILocalRepository localRepo, HubConnectionManager hubConnectionManager)
        {
            
            _repository = repository;
            _localRepo = localRepo;
            _hubConnectionManager = hubConnectionManager;
            
        }
        // GET: api/<ErrorController>
        [HttpGet("getApplications/{userId}")]
        public IEnumerable<Application> Get(string userId)
        {
            List<Application> apps;
            List<IApplication> tempApps = new List<IApplication>();
            var appNames = _localRepo.GetAppNames();
            try
            {
                tempApps = ApplicationFactory.CreateApplicationList(appNames, userId);
                

                /**
                 *  Check if applications from tempApps exists in Database
                 **/
                apps = _repository.GetApplications(userId, tempApps);
                return apps;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        [HttpGet("getErrors/{applicationId}")]
        public IEnumerable<Error> Get(Guid applicationId)
        {
            try
            {
                List<Error> errors = new List<Error>();

                IApplication dbApp = _repository.GetApplication(_localRepo.UserId, applicationId);
                List<IErrorPostModel> errorPostModels = _localRepo.GetErrorPostModels();

                List<Error> tempErrors = ErrorFactory.CreateTempErrorList(dbApp, applicationId, errorPostModels);
              

                errors = _repository.GetErrors(applicationId, tempErrors);
                return errors;
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        [HttpPost("addApplication/{applicationName}")]
        public void Post([FromBody] string applicationName)
        {

            if (!_localRepo.GetAppNames().Contains(applicationName))
                _localRepo.AddAppName(applicationName);


        }

        [HttpPost("{application}")]
        public void Post([FromBody] IApplication application)
        {

            _repository.AddApplication(application);
        }

        [HttpPost("addError/{error}")]
        public async Task post([FromBody] ErrorPostModel error)
        {
            

            error.ErrorModel.ErrorId = Guid.NewGuid();
            

            bool appNameExists = _localRepo.GetAppNames().Contains(error.ApplicationName);
            bool userIdIsEmpty = string.IsNullOrWhiteSpace(_localRepo.UserId);
            /*
             * If user id is empty and the local app names list doesn't contain the app name
             * */
            
            if(userIdIsEmpty == false) 
                await _hubConnectionManager.StartConnectionAsync();
            else 
                await _hubConnectionManager.StopConnectionAsync();

            IConnectionProcessingService connectionProcessingService = new ConnectionProcessingService(_localRepo, _hubConnectionManager, _repository);
            connectionProcessingService.ProcessError(error, appNameExists, userIdIsEmpty);

            
            
        }

        [HttpPost("addUserId/{userId}")]
        public void post([FromBody] string userId)
        {
            _localRepo.UserId = userId;
        }

        [HttpPut("updateCompletionStatus/{completed}")]
        public void put([FromBody] CompletedModel completed)
        {
            IError error = _repository.GetError(completed.ErrorId);
            error.Resolved = completed.IsCompleted;
            _repository.UpdateError(error);
        }

        // PUT api/<ErrorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ErrorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
