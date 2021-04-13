using AuthorizationSample.Dtos;
using AuthorizationSample.Results;

namespace AuthorizationSample.Services
{
    public interface IUserAuthorizationService
    {
        ILoginResult Login(UserCredentialDataDto userCredentialDataDto);
    }
}
