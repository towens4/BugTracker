namespace BugTracker.Models
{
    public class Error
    {
        public Guid ErrorId { get; set; }
        public Guid ApplicationId { get; set; }
        public string UserId { get; set; }
        public string Exception { get; set; }
        public string FileLocation { get; set; }
        public string ErrorDetails { get; set; }
        public bool Resolved { get; set; }
    }
}
