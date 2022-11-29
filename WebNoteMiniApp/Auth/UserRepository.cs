namespace WebNoteMiniApp.Auth
{
    public class UserRepository : IUserRepository
    {
        private List<UserDto> users = new()
        {
            new("Anton", "qwe"),
            new("Aizek", "asd"),
            new("Tom", "zxc"),
        };

        public UserDto GetUser(UserModel userModel)
        {
            return users.FirstOrDefault(u =>
                string.Equals(u.password, userModel.Password) &&
                string.Equals(u.username, userModel.UserName, StringComparison.CurrentCultureIgnoreCase)) ??
                throw new Exception("User not found exception");
        }
    }
}
