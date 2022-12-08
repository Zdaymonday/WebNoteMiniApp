using Microsoft.AspNetCore.Identity;

namespace WebNoteMiniApp.Auth
{
    public class UserService
    {
        private readonly HttpContext httpContext;
        private readonly UserManager<NoteUser> userManager;

        public UserService(IHttpContextAccessor contextAccessor, UserManager<NoteUser> manager) =>
            (httpContext, userManager) = (contextAccessor.HttpContext, manager);

        public async Task<string> GetUserId()
        {
            var user = await userManager.Users
                .FirstOrDefaultAsync(u => u.UserName == httpContext.User.Identity.Name);
            return user.Id;
        }
    }
}
