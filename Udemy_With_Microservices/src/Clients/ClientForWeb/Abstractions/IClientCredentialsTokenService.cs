namespace ClientForWeb.Abstractions
{
    public interface IClientCredentialsTokenService
    {
        Task<string> GetTokenAsync();
    }
}
