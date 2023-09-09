using BugTrackerCore.Interfaces;

namespace BugTrackerCore.Models
{
    public class LocalRepository : ILocalRepository
    {
        private List<string> _appNameList = new List<string>();
        private List<Error> _exceptionList = new List<Error>();
        private Error _exception;
        private List<ErrorPostModel> _ErrorPostModel = new List<ErrorPostModel>()
        {
            new ErrorPostModel()
            {
                ErrorModel = new Error()
                {
                    ErrorId = new Guid(),
                    Exception = "SampleException",
                    FileLocation = "C:\\Sample\\Program.cs",
                    MethodName = "SampleMethod",
                    FileLine = "42",
                    ErrorDetails = "This is a sample error.",
                    Resolved = false
                },
                ApplicationName = "BugTrackerUI"
            }
        };
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

        public void RemoveErrorPostModel(ErrorPostModel errorPostModel)
        {
            _ErrorPostModel.Remove(errorPostModel);
        }
    }
}
