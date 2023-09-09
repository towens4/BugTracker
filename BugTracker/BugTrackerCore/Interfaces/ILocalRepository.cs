using BugTrackerCore.Models;

namespace BugTrackerCore.Interfaces
{
    public interface ILocalRepository
    {
        void AddAppName(string appName);
        List<string> GetAppNames();
        List<Error> GetExceptionList();
        List<ErrorPostModel> GetErrorPostModels();
        void AddErrorPostModel(ErrorPostModel errorPostModel);
        void setError(Error exception);
        Error GetError();
        void RemoveErrorPostModel(ErrorPostModel errorPostModel);
    }
}
