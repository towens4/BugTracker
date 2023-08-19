using BugTracker.Interfaces;
using BugTracker.Models;
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
        [HttpGet("{userId}")]
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
                        ApplicationId = new Guid()
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

        // GET api/<ErrorController>/5
        /* [HttpGet("{appId}")]
         public IEnumerable<Error> Get(Guid appId)
         {
             return _repository.GetErrors(appId);
         }*/

        [HttpPost("addApplication/{applicationName}")]
        public void Post([FromBody] string applicationName)
        {
            //_localRepo.
            if(!_localRepo.GetAppNames().Contains(applicationName))
                _localRepo.AddAppName(applicationName);
            var list = _localRepo.GetAppNames();
           
        }

        [HttpPost("{application}")]
        public void Post([FromBody] Application application)
        {

            _repository.AddApplication(application);
        }

        [HttpPost("addError/{error}")]
        public void post([FromBody] Exception error)
        {
            _repository.AddError(error);
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
