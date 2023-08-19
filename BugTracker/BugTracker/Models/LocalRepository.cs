using BugTracker.Interfaces;

namespace BugTracker.Models
{
    public class LocalRepository : ILocalRepository
    {
        private List<string> _appNameList;
        private Exception _exception;
        public LocalRepository(List<string> appNameList, Exception exception)
        {
            _appNameList = appNameList;
            _exception = exception;
        }
        public void AddAppName(string appName)
        {
            _appNameList.Add(appName);
        }

        public List<string> GetAppNames()
        {
            return _appNameList;
        }

        public Exception GetError()
        {
            return _exception;
        }

        public void setError(Exception exception)
        {
            _exception = exception;    
        }
    }
}
