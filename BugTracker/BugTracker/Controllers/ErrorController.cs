using BugTrackerCore.Helper;
using BugTrackerCore.Interfaces;
using BugTrackerCore.Models;
using BugTrackerCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using System;

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
            List<Application> apps = new List<Application>();
            List<Application> tempApps = new List<Application>();
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
                //Guid applicationId = Guid.Parse(applicationId);
                List<Error> errors = new List<Error>();

                Application dbApp = _repository.GetApplication(_localRepo.UserId, applicationId);
                List<ErrorPostModel> errorPostModels = _localRepo.GetErrorPostModels();

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
            
            if(!_localRepo.GetAppNames().Contains(applicationName))
                _localRepo.AddAppName(applicationName);
            
           
        }

        [HttpPost("{application}")]
        public void Post([FromBody] Application application)
        {

            _repository.AddApplication(application);
        }

        [HttpPost("addError/{error}")]
        public async Task post([FromBody] ErrorPostModel error)
        {
            Application dbApp = new Application();

            error.ErrorModel.ErrorId = Guid.NewGuid();
            //await _hubConnectionManager.StartConnectionAsync();

            bool appNameExists = _localRepo.GetAppNames().Contains(error.ApplicationName);
            bool userIdIsEmpty = string.IsNullOrWhiteSpace(_localRepo.UserId);
            /*
             * If user id is empty and the local app names list doesn't contain the app name
             * */
            
            if(userIdIsEmpty == false) 
                await _hubConnectionManager.StartConnectionAsync();
            else 
                await _hubConnectionManager.StopConnectionAsync();

            //if user id doesn't exist and appname doesn't exist add errot to local repo
            if (userIdIsEmpty == false && appNameExists == false)
            {
                _localRepo.AddErrorPostModel(error);
                _localRepo.AddAppName(error.ApplicationName);
                await _hubConnectionManager.SendAppSignal();
                return;
            }

            //checks if application name existss in appNameList
            if (appNameExists == false)
            {
                _localRepo.AddErrorPostModel(error);
                _localRepo.AddAppName(error.ApplicationName);
            }
                
               
            if (userIdIsEmpty == false)
            {
                //Add hubConnection send error
                dbApp = _repository.GetApplicationByName(_localRepo.UserId, error.ApplicationName);

                error.ErrorModel.ApplicationId = dbApp.ApplicationId;
                await _hubConnectionManager.SendError(error);
                _repository.AddError(error.ErrorModel);
                await _hubConnectionManager.SendAppSignal();
            }

            //_repository.AddError(error.ErrorModel);
        }

        [HttpPost("addUserId/{userId}")]
        public void post([FromBody] string userId)
        {
            _localRepo.UserId = userId;
        }

        [HttpPut("updateCompletionStatus/{completed}")]
        public void put([FromBody] CompletedModel completed)
        {
            Error error = _repository.GetError(completed.ErrorId);
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
