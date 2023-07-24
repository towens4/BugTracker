﻿namespace BugTrackerUI.Models.ViewModels
{
    public class ErrorViewModel
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