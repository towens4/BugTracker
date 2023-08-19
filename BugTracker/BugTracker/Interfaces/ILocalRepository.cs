namespace BugTracker.Interfaces
{
    public interface ILocalRepository
    {
        void AddAppName(string appName);
        List<string> GetAppNames();
        void setError(Exception exception);
        Exception GetError();
    }
}
