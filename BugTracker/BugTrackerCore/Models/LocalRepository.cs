using BugTrackerAPICall.Interfaces;
using BugTrackerAPICall.Interfaces;

namespace BugTrackerAPICall.Models
{
    public class LocalRepository : ILocalRepository
    {
        private List<string> _appNameList = new List<string>();
        private List<IError> _exceptionList = new List<IError>();
        private IError _exception;
        private List<IErrorPostModel> _ErrorPostModel = new List<IErrorPostModel>();

        public string UserId { get; set; } = "";

        public void AddAppName(string appName)
        {
            _appNameList.Add(appName);
        }
        public void AddException(IError exception)
        {
            _exceptionList.Add(exception);
        }
        public List<string> GetAppNames()
        {
            return _appNameList;
        }
        public List<IErrorPostModel> GetErrorPostModels() 
        { 
            return _ErrorPostModel; 
        }
        public void AddErrorPostModel(IErrorPostModel errorPostModel)
        {

            _ErrorPostModel.Add(errorPostModel);
        }
        public IError GetError()
        {
            return _exception;
        }

        public void setError(IError exception)
        {
            _exception = exception;    
        }

        public List<IError> GetExceptionList()
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

        public void RemoveErrorPostModel(IErrorPostModel errorPostModel)
        {
            _ErrorPostModel.Remove(errorPostModel);
        }
    }
}
