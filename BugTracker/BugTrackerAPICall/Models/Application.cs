using BugTrackerAPICall.Interfaces;

namespace BugTrackerAPICall.Models
{
    public class Application : IApplication
    {
        public Guid ApplicationId { get; set; }
        public string UserId { get; set; }
        public string ApplicationName { get; set; }
    }
}
