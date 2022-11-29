namespace WebNoteMiniApp.Auth
{
    public interface ITokenService
    {
        string BuildToken(string key, string issue, UserDto userDto);
    }
}
