namespace AuthorizationSample.Results
{
    public class SuccessfulLoginResult : ILoginResult
    {
        public SuccessfulLoginResult(string token)
        {
            Token = token;
        }

        public bool IsSuccessful => true;
        public string Token { get; }
    }
}
