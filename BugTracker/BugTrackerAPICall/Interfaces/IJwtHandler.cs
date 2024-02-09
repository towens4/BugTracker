using BugTrackerAPICall.Services;

namespace BugTrackerAPICall.Interfaces
{
    public interface IJwtHandler
    {
        string GetTokenValue();
        JwtHandler ParseToken(string token);
        byte[] ToTokenBytes();
        bool isTokenExpired();
    }
}