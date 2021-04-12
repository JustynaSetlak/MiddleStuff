using AuthorizationSample.Models;

namespace AuthorizationSample.Services
{
    public interface IUserAuthorizationService
    {
        string Login(UserCredentialData userCredentialData);
    }
}
