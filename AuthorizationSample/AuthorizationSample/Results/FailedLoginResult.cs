namespace AuthorizationSample.Results
{
    public class FailedLoginResult : ILoginResult
    {
        public bool IsSuccessful => false;
        public string Token => string.Empty;
    }
}
