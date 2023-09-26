using BugTrackerCore.Interfaces;

namespace BugTrackerCore.Models
{
    public class LocalRepository : ILocalRepository
    {
        private List<string> _appNameList = new List<string>();
        private List<Error> _exceptionList = new List<Error>();
        private Error _exception;
        private List<ErrorPostModel> _ErrorPostModel = new List<ErrorPostModel>();

        public string UserId { get; set; } = "";

        public void AddAppName(string appName)
        {
            _appNameList.Add(appName);
        }
        public void AddException(Error exception)
        {
            _exceptionList.Add(exception);
        }
        public List<string> GetAppNames()
        {
            return _appNameList;
        }
        public List<ErrorPostModel> GetErrorPostModels() 
        { 
            return _ErrorPostModel; 
        }
        public void AddErrorPostModel(ErrorPostModel errorPostModel)
        {

            _ErrorPostModel.Add(errorPostModel);
        }
        public Error GetError()
        {
            return _exception;
        }

        public void setError(Error exception)
        {
            _exception = exception;    
        }

        public List<Error> GetExceptionList()
        {
            return _exceptionList;
        }

        public void UpdateUserId(string currentId)
        {
            if(currentId != UserId)
            {
                UserId = currentId;
            }

        }

        public void RemoveErrorPostModel(ErrorPostModel errorPostModel)
        {
            _ErrorPostModel.Remove(errorPostModel);
        }
    }
}
