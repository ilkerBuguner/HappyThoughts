namespace HappyThoughts.Web.ViewModels.UsersFollowers
{
    using System;

    using AutoMapper;
    using HappyThoughts.Data.Models;
    using HappyThoughts.Services.Mapping;
    using HappyThoughts.Web.ViewModels.Users;

    public class UserFollowerViewModel : IMapFrom<UserFollower>
    {
        public string FollowedUserId { get; set; }

        public virtual ApplicationUserDetailsViewModel FollowedUser { get; set; }

        public string FollowingUserId { get; set; }

        public virtual ApplicationUserDetailsViewModel FollowingUser { get; set; }
    }
}
