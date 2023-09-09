using BugTrackerCore.Interfaces;
using BugTrackerCore.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BugTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        /**
         * Name added to lIst
         * The below variable resets the list to 0;
         * */
        
        private readonly IDbRepository _repository;
        private readonly ILocalRepository _localRepo;

        public ErrorController(IDbRepository repository, ILocalRepository localRepo)
        {
            
            _repository = repository;
            _localRepo = localRepo;
        }
        // GET: api/<ErrorController>
        [HttpGet("getApplications/{userId}")]
        public IEnumerable<Application> Get(string userId)
        {
            List<Application> apps = null;
            List<Application> tempApps = new List<Application>();
            var list = _localRepo.GetAppNames();
            try
            {
                foreach(string applicationName in _localRepo.GetAppNames())
                {
                    tempApps.Add(new Application()
                    {
                        ApplicationName = applicationName,
                        UserId = userId,
                        ApplicationId = Guid.NewGuid(),
                    });
                }

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
            return apps;
        }

        [HttpGet("{applicationId}/{userId}")]
        public IEnumerable<Error> Get(string applicationId, string userId)
        {

            Guid newApplicationId = Guid.Parse(applicationId);
            List<Error> errors = null;
            List<Error> tempErrors = new List<Error>();
            Application dbApp = _repository.GetApplication(userId, newApplicationId);
            List<ErrorPostModel> errorPostModels = _localRepo.GetErrorPostModels();
            foreach(var error in errorPostModels)
            {
                if(dbApp == null)
                {
                    throw new Exception("Application Not found in database");
                }
                if(error.ApplicationName != dbApp.ApplicationName && newApplicationId != dbApp.ApplicationId)
                {
                    continue;
                }
                error.ErrorModel.ApplicationId = newApplicationId;
                _repository.AddError(error.ErrorModel);
                
            }
            try
            {
                foreach (var item in errorPostModels)
                {
                    //Checks to see if error comes from a different application, if yes add to temp errors
                    if(item.ApplicationName == dbApp.ApplicationName && newApplicationId == dbApp.ApplicationId)
                    {
                        tempErrors.Add(new Error()
                        {
                            ApplicationId = newApplicationId,
                            ErrorId = item.ErrorModel.ErrorId,
                            ErrorDetails = item.ErrorModel.ErrorDetails,
                            Exception = item.ErrorModel.Exception,
                            FileLine = item.ErrorModel.FileLine,
                            MethodName = item.ErrorModel.MethodName,
                            FileLocation = item.ErrorModel.FileLocation,
                            Resolved = item.ErrorModel.Resolved
                        });
                    }
                    else
                    {
                        tempErrors.Add(new Error()
                        {
                            ApplicationId = item.ErrorModel.ApplicationId,
                            ErrorId = item.ErrorModel.ErrorId,
                            ErrorDetails = item.ErrorModel.ErrorDetails,
                            Exception = item.ErrorModel.Exception,
                            FileLine = item.ErrorModel.FileLine,
                            MethodName = item.ErrorModel.MethodName,
                            FileLocation = item.ErrorModel.FileLocation,
                            Resolved = item.ErrorModel.Resolved
                        });
                    }
                   
                }

                errors = _repository.GetErrors(newApplicationId, tempErrors);
                return errors;
            }
            catch (Exception ex)
            {

                throw;
            }
            return new List<Error>();
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
        public void post([FromBody] ErrorPostModel error)
        {
            _localRepo.AddErrorPostModel(error);
            _localRepo.setError(error.ErrorModel);
            //_repository.AddError(error.ErrorModel);
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
