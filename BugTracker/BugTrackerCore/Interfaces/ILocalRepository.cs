using BugTrackerAPICall.Interfaces;
using BugTrackerAPICall.Models;

namespace BugTrackerAPICall.Interfaces
{
    public interface ILocalRepository
    {
        void AddAppName(string appName);
        List<string> GetAppNames();
        List<IError> GetExceptionList();
        List<IErrorPostModel> GetErrorPostModels();
        void AddErrorPostModel(IErrorPostModel errorPostModel);
        void setError(IError exception);
        IError GetError();
        void RemoveErrorPostModel(IErrorPostModel errorPostModel);
        public string UserId { get; set; }
    }
}
