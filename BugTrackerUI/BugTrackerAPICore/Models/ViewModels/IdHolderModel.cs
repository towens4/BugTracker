using BugTrackerCore.Interfaces;

namespace BugTrackerUI.Models.ViewModels
{
    public class IdHolderModel : IIdHolderModel
    {
        public string UserId { get; set; }
        public Guid ApplicationId { get; set; }
    }
}
