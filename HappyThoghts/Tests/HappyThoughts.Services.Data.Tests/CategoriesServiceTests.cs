namespace HappyThoughts.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HappyThoughts.Data.Models;
    using HappyThoughts.Data.Repositories;
    using HappyThoughts.Services.Data.Categories;
    using HappyThoughts.Services.Data.Tests.Common;
    using HappyThoughts.Web.ViewModels.Categories;
    using HappyThoughts.Web.ViewModels.InputModels.Categories;
    using Xunit;

    public class CategoriesServiceTests
    {
        public CategoriesServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async Task CreateAsync_ShouldSuccessfullyCreate()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var categoriesService = new CategoriesService(categoryRepository);

            var inputModel = new CreateCategoryInputModel()
            {
                Name = "TestName",
            };

            // Act
            var expectedCategoriesCount = 1;
            await categoriesService.CreateAsync(inputModel);
            var actualCategoriesCount = categoryRepository.All().Count();

            // Assert
            Assert.Equal(expectedCategoriesCount, actualCategoriesCount);
        }

        [Fact]
        public async Task DeleteByIdAsync_WithCorrectData_ShouldSuccessfullyDelete()
        {
            var testName = "TestName";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var categoriesService = new CategoriesService(categoryRepository);

            var inputModel = new CreateCategoryInputModel()
            {
                Name = testName,
            };

            await categoriesService.CreateAsync(inputModel);
            var category = categoryRepository.All().FirstOrDefault(c => c.Name == testName);

            // Act
            var expectedCateogiesCount = 0;
            await categoriesService.DeleteByIdAsync(category.Id);
            var actualCategoriesCount = categoryRepository.All().Count();

            // Assert
            Assert.Equal(expectedCateogiesCount, actualCategoriesCount);
        }

        [Fact]
        public async Task DeleteByIdAsync_WithIncorrectData_ShouldThrowArgumentNullException()
        {
            var incorrectId = Guid.NewGuid().ToString();

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var categoriesService = new CategoriesService(categoryRepository);

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await categoriesService.DeleteByIdAsync(incorrectId);
            });
        }

        [Fact]
        public async Task EditAsync_WithCorrectData_ShouldSuccessfullyEdit()
        {
            var testName = "TestName";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var categoriesService = new CategoriesService(categoryRepository);

            var inputModel = new CreateCategoryInputModel()
            {
                Name = testName,
            };

            await categoriesService.CreateAsync(inputModel);
            var category = categoryRepository.All().FirstOrDefault(c => c.Name == testName);

            // Act
            var expectedCategoryName = "Edited_TestName";
            var editInputModel = new CategoryInfoViewModel()
            {
                Id = category.Id,
                Name = expectedCategoryName,
            };
            await categoriesService.EditAsync(editInputModel);
            var actualCategoryName = category.Name;

            // Assert
            category = await categoryRepository.GetByIdWithDeletedAsync(category.Id);
            Assert.Equal(expectedCategoryName, actualCategoryName);
        }

        [Fact]
        public async Task EditAsync_WithIncorrectData_ShouldThrowArgumentNullException()
        {
            var incorrectId = Guid.NewGuid().ToString();

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var categoriesService = new CategoriesService(categoryRepository);

            var editInputModel = new CategoryInfoViewModel()
            {
                Id = incorrectId,
                Name = "SomeTestName",
            };

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await categoriesService.EditAsync(editInputModel);
            });
        }

        [Fact]
        public async Task GetCategoryById_WithCorrectData_ShouldReturnCorrectResult()
        {
            var testName = "CategoryTestName";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var categoriesService = new CategoriesService(categoryRepository);

            var inputModel = new CreateCategoryInputModel()
            {
                Name = testName,
            };

            await categoriesService.CreateAsync(inputModel);

            var category = categoryRepository.All().FirstOrDefault(x => x.Name == testName);

            // Act
            var expectedCategoryId = category.Id;
            var expectedReturnType = typeof(CategoryInfoViewModel);
            var actualReturnType = categoriesService.GetCategoryByName(testName).GetType();
            var actualCategoryId = categoriesService.GetCategoryByName(testName).Id;

            // Assert
            Assert.True(expectedReturnType == actualReturnType);
            Assert.Equal(expectedCategoryId, actualCategoryId);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("InvlidCategoryId")]
        public async Task GetCategoryById_WithIncorrectData_ShouldThrowArgumentException(string invalidId)
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var categoriesService = new CategoriesService(categoryRepository);

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await categoriesService.GetCategoryByIdAsync(invalidId);
            });
        }

        [Fact]
        public async Task GetCategoryByName_WithCorrectData_ShouldReturnCorrectResult()
        {
            var testName = "CategoryTestName";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var categoriesService = new CategoriesService(categoryRepository);

            var inputModel = new CreateCategoryInputModel()
            {
                Name = testName,
            };

            await categoriesService.CreateAsync(inputModel);
            var category = categoryRepository.All().FirstOrDefault(c => c.Name == testName);

            // Act
            var expectedCategoryId = category.Id;
            var expectedReturnType = typeof(CategoryInfoViewModel);
            var actualReturnType = categoriesService.GetCategoryByName(testName).GetType();
            var actualCategoryId = categoriesService.GetCategoryByName(testName).Id;

            // Assert
            Assert.True(expectedReturnType == actualReturnType);
            Assert.Equal(expectedCategoryId, actualCategoryId);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("InvlidCategoryName")]
        public async Task GetCategoryByName_WithIncorrectData_ShouldThrowArgumentException(string invalidName)
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var categoriesService = new CategoriesService(categoryRepository);

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                categoriesService.GetCategoryByName(invalidName);
            });
        }

        [Fact]
        public async Task GetIdByName_WithCorrectData_ShouldReturnId()
        {
            var testName = "TestName";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var categoriesService = new CategoriesService(categoryRepository);

            var inputModel = new CreateCategoryInputModel()
            {
                Name = testName,
            };

            await categoriesService.CreateAsync(inputModel);
            var category = categoryRepository.All().FirstOrDefault(c => c.Name == testName);

            // Act
            var expectedCategoryId = category.Id;
            var actualCategoryId = categoriesService.GetIdByName(testName);

            // Assert
            Assert.Equal(expectedCategoryId, actualCategoryId);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("InvlidCategoryName")]
        public async Task GetIdByName_WithIncorrectData_ShouldThrowArgumentException(string categoryName)
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var categoriesService = new CategoriesService(categoryRepository);

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                categoriesService.GetIdByName(categoryName);
            });
        }

        [Fact]
        public async Task GetNameById_WithCorrectData_ShouldReturnName()
        {
            var testName = "TestName";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var categoriesService = new CategoriesService(categoryRepository);

            var inputModel = new CreateCategoryInputModel()
            {
                Name = testName,
            };

            await categoriesService.CreateAsync(inputModel);
            var category = categoryRepository.All().FirstOrDefault(c => c.Name == testName);

            // Act
            var expectedCategoryName = testName;
            var actualCategoryName = categoriesService.GetNameById(category.Id);

            // Assert
            Assert.Equal(expectedCategoryName, actualCategoryName);
        }

        [Fact]
        public async Task GetNameById_WithIncorrectData_ShouldThrowArgumentNullException()
        {
            var invalidId = "InvalidCategoryId";

            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var categoriesService = new CategoriesService(categoryRepository);

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                categoriesService.GetNameById(invalidId);
            });
        }
    }
}
