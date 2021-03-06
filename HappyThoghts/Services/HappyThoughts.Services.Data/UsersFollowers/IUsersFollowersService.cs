﻿namespace HappyThoughts.Services.Data.UsersFollowers
{
    using System.Threading.Tasks;

    public interface IUsersFollowersService
    {
        Task FollowAsync(string followingUserId, string followedUserId);

        Task UnfollowAsync(string unfollowingUserId, string unfollowedUserId);

        bool IsFollowing(string followingUserId, string followedUserId);
    }
}
