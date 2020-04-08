namespace HappyThoughts.Services.Data.UsersFollowers
{
    using HappyThoughts.Web.ViewModels.Users;
    using System.Threading.Tasks;

    public interface IUsersFollowersService
    {
        Task FollowAsync(string followingUserId, string followedUserId);

        Task Unfollow(string unfollowingUserId, string unfollowedUserId);

        bool IsFollowing(string followingUserId, string followedUserId);
    }
}
