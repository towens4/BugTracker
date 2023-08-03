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
        private readonly IDbRepository _repository;
        public ErrorController(IDbRepository repository)
        {
            _repository = repository;
        }
        // GET: api/<ErrorController>
        [HttpGet("{userId}")]
        public IEnumerable<Application> Get(string userId)
        {
            List<Application> apps = null;
            try
            {
                apps = _repository.GetApplications(userId);
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

        [HttpPost("{application}")]
        public void Post([FromBody] Application application)
        {
            _repository.AddApplication(application);
        }

        // POST api/<ErrorController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
