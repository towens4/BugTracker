using BugTrackerAPICall.Interfaces;

namespace BugTrackerAPICall.Models
{
    public class Error : IError
    {
        public Guid ErrorId { get; set; }
        public Guid ApplicationId { get; set; }
        public string Exception { get; set; }
        public string FileLocation { get; set; }
        public string MethodName { get; set; }
        public string FileLine { get; set; }
        public string ErrorDetails { get; set; }
        public bool Resolved { get; set; }
    }
}
