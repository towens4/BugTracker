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

namespace BugTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly IDbRepository _repository;
        private readonly ILocalRepository _localRepo;
        private readonly IConnectionProcessingService _connectionProcessingService;
        private readonly HubConnectionManager _hubConnectionManager;
        private readonly IEncryptionService _encryptionService;
        private readonly IJwtHandler _jwtHandler;
        private readonly IConfiguration _configuration;

        public ErrorController(IDbRepository repository, ILocalRepository localRepo, HubConnectionManager hubConnectionManager, 
            IConnectionProcessingService connectionProcessingService, IEncryptionService encryptionService, IJwtHandler jwtHandler,
            IConfiguration configuration)
        {

            _repository = repository;
            _localRepo = localRepo;
            _hubConnectionManager = hubConnectionManager;
            _connectionProcessingService = connectionProcessingService;
            _encryptionService = encryptionService;
            _jwtHandler = jwtHandler;
            _configuration = configuration;
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

            _connectionProcessingService.ProcessError(error, appNameExists, userIdIsEmpty);

            
            
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

        [HttpPost("authenticateToken/{token}")]
        public void AuthenticateToken([FromBody]string token)
        {
            byte[] tokenValue = _jwtHandler.ParseToken(token).ToTokenBytes();
            string userId = _localRepo.UserId;

            if (_jwtHandler.isTokenExpired() == false)
            {
                userId = _encryptionService.DecryptData(tokenValue, _configuration["CryptographySettings:Key"]);
            }
            
            
            _localRepo.UserId = userId;
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
