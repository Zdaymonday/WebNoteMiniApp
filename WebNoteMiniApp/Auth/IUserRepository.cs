namespace WebNoteMiniApp.Auth
{
    public interface IUserRepository
    {
        UserDto GetUser(UserModel userModel);
    }
}
