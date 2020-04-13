namespace HappyThoughts.Services.Data.UsersFollowers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HappyThoughts.Data.Common.Repositories;
    using HappyThoughts.Data.Models;

    public class UsersFollowersService : IUsersFollowersService
    {
        private const string InvalidUserAndFollowerErrorMessage = "Follower and Following user pair does not exist.";

        private readonly IDeletableEntityRepository<UserFollower> userFollowerRepository;

        public UsersFollowersService(IDeletableEntityRepository<UserFollower> userFollowerRepository)
        {
            this.userFollowerRepository = userFollowerRepository;
        }

        public async Task FollowAsync(string followingUserId, string followedUserId)
        {
            var doesFollowingAndFollowerExist = this.userFollowerRepository
                .AllWithDeleted()
                .Any(x => x.FollowingUserId == followingUserId
                    && x.FollowedUserId == followedUserId && x.IsDeleted == true);

            if (doesFollowingAndFollowerExist == true)
            {
                var existingUserFollower = this.userFollowerRepository
                    .AllWithDeleted()
                    .FirstOrDefault(x => x.FollowingUserId == followingUserId
                                    && x.FollowedUserId == followedUserId);

                this.userFollowerRepository.Undelete(existingUserFollower);
            }
            else
            {
                var userFollower = new UserFollower()
                {
                    FollowingUserId = followingUserId,
                    FollowedUserId = followedUserId,
                };

                await this.userFollowerRepository.AddAsync(userFollower);
            }

            await this.userFollowerRepository.SaveChangesAsync();
        }

        public async Task UnfollowAsync(string unfollowingUserId, string unfollowedUserId)
        {
            var userFollowerFromDb = this.userFollowerRepository
                .All().FirstOrDefault(x => x.FollowingUserId == unfollowingUserId
                    && x.FollowedUserId == unfollowedUserId);

            if (userFollowerFromDb == null)
            {
                throw new ArgumentException(InvalidUserAndFollowerErrorMessage);
            }

            this.userFollowerRepository.Delete(userFollowerFromDb);
            await this.userFollowerRepository.SaveChangesAsync();
        }

        public bool IsFollowing(string followingUserId, string followedUserId)
        {
            return this.userFollowerRepository
                .All()
                .Any(x => x.FollowingUserId == followingUserId
                    && x.FollowedUserId == followedUserId);
        }
    }
}
