using BugTrackerCore.Helper;
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

        [HttpGet("{applicationId}/{userId}")]
        public IEnumerable<Error> Get(string applicationId, string userId)
        {
            try
            {
                Guid newApplicationId = Guid.Parse(applicationId);
                List<Error> errors = null;

                Application dbApp = _repository.GetApplication(userId, newApplicationId);
                List<ErrorPostModel> errorPostModels = _localRepo.GetErrorPostModels();

                List<Error> tempErrors = ErrorFactory.CreateTempErrorList(dbApp, newApplicationId, errorPostModels);
              

                errors = _repository.GetErrors(newApplicationId, tempErrors);
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
