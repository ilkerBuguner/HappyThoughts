namespace HappyThoughts.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HappyThoughts.Data.Models;
    using HappyThoughts.Data.Repositories;
    using HappyThoughts.Services.Data.Tests.Common;
    using HappyThoughts.Services.Data.Topics;
    using HappyThoughts.Web.ViewModels.InputModels;
    using HappyThoughts.Web.ViewModels.Topics;
    using Xunit;

    public class TopicsServiceTests
    {
        public TopicsServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async Task CreateAsync_ShouldSuccessfullyCreate()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var topicRepository = new EfDeletableEntityRepository<Topic>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var topicsService = new TopicsService(topicRepository, userRepository);

            var inputModel = new CreateTopicInputModel()
            {
                Title = "TestTitle",
                Content = "TestContent_TestContent_TestContent_TestContent_TestContent_TestContent",
            };

            // Act
            var expectedTopicsCount = 1;
            await topicsService.CreateAsync(inputModel);

            // Assert
            Assert.Equal(expectedTopicsCount, topicRepository.All().Count());
        }

        //[Fact]
        //public async Task GetByIdAsViewModelAsync_WithCorrectData_ShouldReturnCorrectResult()
        //{
        //    var testTitle = "TestTitle";

        //    // Arrange
        //    var serviceFactory = new ServiceFactory();
        //    var topicRepository = new EfDeletableEntityRepository<Topic>(serviceFactory.Context);
        //    var userRepository = new EfDeletableEntityRepository<ApplicationUser>(serviceFactory.Context);
        //    var topicsService = new TopicsService(topicRepository, userRepository);

        //    var topic = new Topic()
        //    {
        //        Title = testTitle,
        //    };

        //    await topicRepository.AddAsync(topic);
        //    await topicRepository.SaveChangesAsync();

        //    // Act
        //    var topicId = topicRepository.All().FirstOrDefault(t => t.Title == testTitle).Id;
        //    var expectedTitle = testTitle;
        //    var actualTopic = await topicsService.GetByIdAsViewModelAsync(topicId);

        //    // Assert
        //    Assert.Equal(expectedTitle, actualTopic.Title);
        //    Assert.Equal(actualTopic.GetType().ToString(), typeof(TopicDetailsViewModel).ToString());
        //}

        [Fact]
        public async Task DeleteByIdAsync_WithCorrectData_ShouldSuccessfullyDelete()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var topicRepository = new EfDeletableEntityRepository<Topic>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var topicsService = new TopicsService(topicRepository, userRepository);

            var inputModel = new CreateTopicInputModel()
            {
                Title = "TestTitle",
                Content = "TestContent_TestContent_TestContent_TestContent_TestContent_TestContent",
            };

            await topicsService.CreateAsync(inputModel);
            var topic = topicRepository.All().FirstOrDefault(t => t.Title == "TestTitle");

            // Act
            var expectedTopicsCount = 0;
            await topicsService.DeleteByIdAsync(topic.Id);

            // Assert
            Assert.Equal(expectedTopicsCount, topicRepository.All().Count());
        }

        [Fact]
        public async Task DeleteByIdAsync_WithIncorrectData_ShouldThrowArgumentNullException()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid().ToString();

            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var topicRepository = new EfDeletableEntityRepository<Topic>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var topicsService = new TopicsService(topicRepository, userRepository);

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await topicsService.DeleteByIdAsync(nonExistentId);
            });
        }

        [Fact]
        public async Task EditAsync_WithCorrectData_ShouldSuccessfullyEdit()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var topicRepository = new EfDeletableEntityRepository<Topic>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var topicsService = new TopicsService(topicRepository, userRepository);

            var createInputModel = new CreateTopicInputModel()
            {
                Title = "TestTitle",
                Content = "TestContent_TestContent_TestContent_TestContent_TestContent_TestContent",
            };

            await topicsService.CreateAsync(createInputModel);
            var topic = topicRepository.All().FirstOrDefault(t => t.Title == "TestTitle");

            var editInputModel = new TopicEditViewModel()
            {
                Id = topic.Id,
                Title = "Edited_TestTitle",
                Content = "Edited_TestContent_TestContent_TestContent_TestContent_TestContent_TestContent",
            };

            // Act
            var expectedTopicTitle = "Edited_TestTitle";
            var expectedTopicContent = "Edited_TestContent_TestContent_TestContent_TestContent_TestContent_TestContent";
            await topicsService.EditAsync(editInputModel);

            // Assert
            topic = await topicRepository.GetByIdWithDeletedAsync(editInputModel.Id);
            Assert.Equal(expectedTopicTitle, topic.Title);
            Assert.Equal(expectedTopicContent, topic.Content);
        }

        [Fact]
        public async Task EditAsync_WithIncorrectData_ShouldThrowArgumentNullException()
        {
            // Arrange
            var incorrentInputModelId = "IncorrentId";
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var topicRepository = new EfDeletableEntityRepository<Topic>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var topicsService = new TopicsService(topicRepository, userRepository);

            var editInputModel = new TopicEditViewModel()
            {
                Id = incorrentInputModelId,
                Title = "Edited_TestTitle",
                Content = "Edited_TestContent_TestContent_TestContent_TestContent_TestContent_TestContent",
            };

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await topicsService.EditAsync(editInputModel);
            });
        }

        //[Fact]
        //public async Task GetAllAsync_ShouldReturnCorrectResult()
        //{
        //    // Arrange
        //    var context = ApplicationDbContextInMemoryFactory.InitializeContext();
        //    var topicRepository = new EfDeletableEntityRepository<Topic>(context);
        //    var topicsService = new TopicsService(topicRepository);

        //    for (int i = 0; i < 5; i++)
        //    {
        //        var inputModel = new CreateTopicInputModel()
        //        {
        //            Title = $"TestTitle{i}",
        //            Content = $"TestContent{i}_TestContent_TestContent_TestContent_TestContent_TestContent",
        //        };

        //        await topicsService.CreateAsync(inputModel);
        //    }

        //    // Act
        //    var expectedResultDataType = typeof(TopicInfoViewModel);
        //    var expectedTopicsCount = 5;
        //    var topics = await topicsService.GetAllAsync<TopicInfoViewModel>();
        //    var actualTopicsCount = topics.Count();
        //    var firstTopic = topics[0];

        //    // Assert
        //    Assert.True(expectedResultDataType == firstTopic.GetType());
        //    Assert.Equal(expectedTopicsCount, actualTopicsCount);
        //}

        [Fact]
        public async Task IncreaseViewsAsync_ShouldIncreaseTopicViews()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var topicRepository = new EfDeletableEntityRepository<Topic>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var topicsService = new TopicsService(topicRepository, userRepository);

            var inputModel = new CreateTopicInputModel()
            {
                Title = "TestTitle",
                Content = "TestContent_TestContent_TestContent_TestContent_TestContent_TestContent",
            };

            await topicsService.CreateAsync(inputModel);
            var topicId = topicRepository.All().FirstOrDefault(t => t.Title == "TestTitle").Id;

            // Act
            var expectedViews = 1;
            await topicsService.IncreaseViewsAsync(topicId);
            var actualViews = topicRepository.All().FirstOrDefault(t => t.Title == "TestTitle").Views;

            // Assert
            Assert.Equal(expectedViews, actualViews);
        }

        [Fact]
        public async Task GetIdByTitle_WithCorrectData_ShouldReturnCorrectResult()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var topicRepository = new EfDeletableEntityRepository<Topic>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var topicsService = new TopicsService(topicRepository, userRepository);

            var inputModel = new CreateTopicInputModel()
            {
                Title = "TestTitle",
                Content = "TestContent_TestContent_TestContent_TestContent_TestContent_TestContent",
            };

            await topicsService.CreateAsync(inputModel);

            // Act
            var expectedId = topicRepository.All().FirstOrDefault(t => t.Title == "TestTitle").Id;
            var actualId = topicsService.GetIdByTitle(inputModel.Title);

            // Assert
            Assert.Equal(expectedId, actualId);
        }

        [Fact]
        public async Task GetIdByTitle_WithIncorrectData_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var invalidTitle = "InvalidTitle";
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var topicRepository = new EfDeletableEntityRepository<Topic>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var topicsService = new TopicsService(topicRepository, userRepository);

            // Act

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                topicsService.GetIdByTitle(invalidTitle);
            });
        }

        [Fact]
        public async Task GetTotalTopicsCount_ShouldReturnCorrectTotalTopicsCount()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var topicRepository = new EfDeletableEntityRepository<Topic>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var topicsService = new TopicsService(topicRepository, userRepository);
            await topicsService.CreateAsync(new CreateTopicInputModel());

            // Act
            var expectedTopicsCount = 1;
            var actualTopicsCount = topicsService.GetTotalTopicsCount();

            // Assert
            Assert.Equal(expectedTopicsCount, actualTopicsCount);
        }

        //[Fact]
        //public async Task GetTopicsByCategoryName_ShouldReturnOnlyGivenCategoryTopics()
        //{
        //    // Arrange
        //    var testCategoryName = "TestCategory";

        //    // Arrange
        //    var context = ApplicationDbContextInMemoryFactory.InitializeContext();
        //    var topicRepository = new EfDeletableEntityRepository<Topic>(context);
        //    var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
        //    var topicsService = new TopicsService(topicRepository, userRepository);

        //    var category = new Category()
        //    {
        //        Name = testCategoryName,
        //    };

        //    await context.Categories.AddAsync(category);
        //    await context.SaveChangesAsync();
        //    var categoryId = context.Categories.FirstOrDefault(c => c.Name == testCategoryName).Id;

        //    for (int i = 0; i < 5; i++)
        //    {
        //        await topicsService.CreateAsync(new CreateTopicInputModel()
        //        {
        //            CategoryId = categoryId,
        //        });
        //    }

        //    await topicsService.CreateAsync(new CreateTopicInputModel()
        //    {
        //        Title = "TestTitle",
        //    });

        //    // Act
        //    var expectedTopicsCount = 5;
        //    var actualTopicsCount = topicsService.GetTopicsByCategoryName(testCategoryName).Topics.Count();

        //    // Assert
        //    Assert.Equal(expectedTopicsCount, actualTopicsCount);
        //}

        [Fact]
        public async Task VoteTopicAsync_WithIsLikeTrue_ShouldSuccessfullyIncreaseTopicLikesWithOne()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var topicRepository = new EfDeletableEntityRepository<Topic>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var topicsService = new TopicsService(topicRepository, userRepository);

            var inputModel = new CreateTopicInputModel()
            {
                Title = "TestTitle",
                Content = "TestContent_TestContent_TestContent_TestContent_TestContent_TestContent",
            };

            await topicsService.CreateAsync(inputModel);
            var topic = topicRepository.All().FirstOrDefault(t => t.Title == "TestTitle");

            // Act
            var expectedTopicLikes = 1;
            await topicsService.VoteTopicAsync(topic.Id, true);
            var actualTopicLikes = topicRepository.All().FirstOrDefault(t => t.Title == "TestTitle").Likes;

            // Assert
            Assert.Equal(expectedTopicLikes, actualTopicLikes);
        }

        [Fact]
        public async Task VoteTopicAsync_WithIsLikeFalse_ShouldSuccessfullyIncreaseTopicDislikesWithOne()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var topicRepository = new EfDeletableEntityRepository<Topic>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var topicsService = new TopicsService(topicRepository, userRepository);

            var inputModel = new CreateTopicInputModel()
            {
                Title = "TestTitle",
                Content = "TestContent_TestContent_TestContent_TestContent_TestContent_TestContent",
            };

            await topicsService.CreateAsync(inputModel);
            var topic = topicRepository.All().FirstOrDefault(t => t.Title == "TestTitle");

            // Act
            var expectedTopicDislikes = 1;
            await topicsService.VoteTopicAsync(topic.Id, false);
            var actualTopicDislikes = topicRepository.All().FirstOrDefault(t => t.Title == "TestTitle").Dislikes;

            // Assert
            Assert.Equal(expectedTopicDislikes, actualTopicDislikes);
        }

        [Fact]
        public async Task CancelVoteAsync_WithIsLikeTrue_ShouldSuccessfullyDecreaseTopicLikesWithOne()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var topicRepository = new EfDeletableEntityRepository<Topic>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var topicsService = new TopicsService(topicRepository, userRepository);

            var inputModel = new CreateTopicInputModel()
            {
                Title = "TestTitle",
                Content = "TestContent_TestContent_TestContent_TestContent_TestContent_TestContent",
            };

            await topicsService.CreateAsync(inputModel);
            var topic = topicRepository.All().FirstOrDefault(t => t.Title == "TestTitle");
            await topicsService.VoteTopicAsync(topic.Id, true);

            // Act
            var expectedTopicLikes = 0;
            await topicsService.CancelVoteAsync(topic.Id, true);
            var actualTopicLikes = topicRepository.All().FirstOrDefault(t => t.Title == "TestTitle").Likes;

            // Assert
            Assert.Equal(expectedTopicLikes, actualTopicLikes);
        }

        [Fact]
        public async Task CancelVoteAsync_WithIsLikeFalse_ShouldSuccessfullyDecreaseTopicDislikesWithOne()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var topicRepository = new EfDeletableEntityRepository<Topic>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var topicsService = new TopicsService(topicRepository, userRepository);

            var inputModel = new CreateTopicInputModel()
            {
                Title = "TestTitle",
                Content = "TestContent_TestContent_TestContent_TestContent_TestContent_TestContent",
            };

            await topicsService.CreateAsync(inputModel);
            var topic = topicRepository.All().FirstOrDefault(t => t.Title == "TestTitle");
            await topicsService.VoteTopicAsync(topic.Id, false);

            // Act
            var expectedTopicDislikes = 0;
            await topicsService.CancelVoteAsync(topic.Id, false);
            var actualTopicDislikes = topicRepository.All().FirstOrDefault(t => t.Title == "TestTitle").Dislikes;

            // Assert
            Assert.Equal(expectedTopicDislikes, actualTopicDislikes);
        }

        [Fact]
        public async Task GetTopicTotalLikes_WithCorrectTopicId_ShouldReturnCorrectTotalLikes()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var topicRepository = new EfDeletableEntityRepository<Topic>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var topicsService = new TopicsService(topicRepository, userRepository);

            var inputModel = new CreateTopicInputModel()
            {
                Title = "TestTitle",
                Content = "TestContent_TestContent_TestContent_TestContent_TestContent_TestContent",
            };

            await topicsService.CreateAsync(inputModel);
            var topic = topicRepository.All().FirstOrDefault(t => t.Title == "TestTitle");

            topic.Likes = 15;
            topicRepository.Update(topic);
            await topicRepository.SaveChangesAsync();

            // Act
            var expectedTotalTopicLikes = 15;
            var actualTotalTopicLikes = topicsService.GetTopicTotalLikes(topic.Id);

            // Assert
            Assert.Equal(expectedTotalTopicLikes, actualTotalTopicLikes);
        }

        [Fact]
        public async Task GetTopicTotalLikes_WithIncorrectTopicId_ShouldThrowArgumentException()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var topicRepository = new EfDeletableEntityRepository<Topic>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var topicsService = new TopicsService(topicRepository, userRepository);

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                topicsService.GetTopicTotalLikes("InvalidId");
            });
        }

        [Fact]
        public async Task GetTopicTotalDisikes_WithCorrectTopicId_ShouldReturnCorrectTotalLikes()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var topicRepository = new EfDeletableEntityRepository<Topic>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var topicsService = new TopicsService(topicRepository, userRepository);

            var inputModel = new CreateTopicInputModel()
            {
                Title = "TestTitle",
                Content = "TestContent_TestContent_TestContent_TestContent_TestContent_TestContent",
            };

            await topicsService.CreateAsync(inputModel);
            var topic = topicRepository.All().FirstOrDefault(t => t.Title == "TestTitle");

            topic.Dislikes = 15;
            topicRepository.Update(topic);
            await topicRepository.SaveChangesAsync();

            // Act
            var expectedTotalTopicDislikes = 15;
            var actualTotalTopicDislikes = topicsService.GetTopicTotalDislikes(topic.Id);

            // Assert
            Assert.Equal(expectedTotalTopicDislikes, actualTotalTopicDislikes);
        }

        //[Fact]
        //public async Task GetLatestTopics_ShouldReturnCorrectCountOfTopics()
        //{
        //    // Arrange
        //    var serviceFactory = new ServiceFactory();
        //    var topicRepository = new EfDeletableEntityRepository<Topic>(serviceFactory.Context);
        //    var userRepository = new EfDeletableEntityRepository<ApplicationUser>(serviceFactory.Context);
        //    var topicsService = new TopicsService(topicRepository, userRepository);

        //    await topicRepository.AddAsync(new Topic()
        //    {
        //        Title = "testTitle",
        //    });

        //    await topicRepository.AddAsync(new Topic()
        //    {
        //        Title = "secondTestTitle",
        //    });

        //    await topicRepository.SaveChangesAsync();

        //    // Act
        //    var expectedTopicsCount = 2;
        //    var topics = topicsService.GetLatestTopics();
        //    var actualTopicsCount = topics.Count();

        //    // Assert
        //    Assert.Equal(expectedTopicsCount, actualTopicsCount);
        //}

        [Fact]
        public async Task GetTopicTotalDislikes_WithIncorrectTopicId_ShouldThrowArgumentException()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var topicRepository = new EfDeletableEntityRepository<Topic>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var topicsService = new TopicsService(topicRepository, userRepository);

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                topicsService.GetTopicTotalDislikes("InvalidId");
            });
        }

        [Fact]
        public async Task GetTopicsGetTopicsCountOfUser_ShouldReturnCorrectCount()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var topicRepository = new EfDeletableEntityRepository<Topic>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var topicsService = new TopicsService(topicRepository, userRepository);

            var user = new ApplicationUser()
            {
                UserName = "testUsername",
            };

            user.Topics.Add(new Topic()
            {
                Title = "testTitle",
            });

            user.Topics.Add(new Topic()
            {
                Title = "secondTestTitle",
            });

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();
            var userId = userRepository.All().FirstOrDefault(u => u.UserName == "testUsername").Id;

            // Act
            var expectedTopicsCount = 2;
            var actualTopicsCount = topicsService.GetTopicsCountOfUser(userId);

            // Assert
            Assert.Equal(expectedTopicsCount, actualTopicsCount);
        }

        [Fact]
        public async Task GetRemainingMinutesToCreateTopic_WhenThereIsOtherTopicWith30Minutes_ShouldReturnCorrectMinutes()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var topicRepository = new EfDeletableEntityRepository<Topic>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var topicsService = new TopicsService(topicRepository, userRepository);

            var user = new ApplicationUser()
            {
                UserName = "testUsername",
            };

            user.Topics.Add(new Topic()
            {
                Title = "testTitle",
            });

            user.Topics.Add(new Topic()
            {
                Title = "secondTestTitle",
            });

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();
            var userId = userRepository.All().FirstOrDefault(u => u.UserName == "testUsername").Id;

            // Act
            var expectedRemainingMinutes = 30;
            var actualRemainingMinutes = topicsService.GetRemainingMinutesToCreateTopic(userId);

            // Assert
            Assert.Equal(expectedRemainingMinutes, actualRemainingMinutes);
        }

        [Fact]
        public async Task GetRemainingMinutesToCreateTopic_WhenThereIsNoOtherTopic_ShouldReturnCorrectMinutes()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var topicRepository = new EfDeletableEntityRepository<Topic>(context);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var topicsService = new TopicsService(topicRepository, userRepository);

            var user = new ApplicationUser()
            {
                UserName = "testUsername",
            };

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();
            var userId = userRepository.All().FirstOrDefault(u => u.UserName == "testUsername").Id;

            // Act
            var expectedRemainingMinutes = 31;
            var actualRemainingMinutes = topicsService.GetRemainingMinutesToCreateTopic(userId);

            // Assert
            Assert.Equal(expectedRemainingMinutes, actualRemainingMinutes);
        }
    }
}
