namespace HappyThoughts.Services.Data.Categories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using HappyThoughts.Data.Common.Repositories;
    using HappyThoughts.Data.Models;
    using HappyThoughts.Services.Mapping;
    using HappyThoughts.Web.ViewModels.Categories;
    using HappyThoughts.Web.ViewModels.InputModels.Categories;
    using Microsoft.EntityFrameworkCore;

    public class CategoriesService : ICategoriesService
    {
        private const string InvalidCategoryIdErrorMessage = "Category with ID: {0} does not exist.";

        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task CreateAsync(CreateCategoryInputModel input)
        {
            var category = new Category()
            {
                Name = input.Name,
                PictureUrl = input.PictureUrl,
            };

            await this.categoryRepository.AddAsync(category);
            await this.categoryRepository.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id)
        {
            var categoryFromDb = await this.categoryRepository
                .GetByIdWithDeletedAsync(id);

            if (categoryFromDb == null)
            {
                throw new ArgumentNullException(
                    string.Format(InvalidCategoryIdErrorMessage, id));
            }

            this.categoryRepository.Delete(categoryFromDb);
            await this.categoryRepository.SaveChangesAsync();
        }

        public async Task EditAsync(CategoryInfoViewModel categoryInfoViewModel)
        {
            var categoryFromDb = await this.categoryRepository
                .GetByIdWithDeletedAsync(categoryInfoViewModel.Id);

            if (categoryFromDb == null)
            {
                throw new ArgumentNullException(
                    string.Format(InvalidCategoryIdErrorMessage, categoryInfoViewModel.Id));
            }

            categoryFromDb.Name = categoryInfoViewModel.Name;
            this.categoryRepository.Update(categoryFromDb);
            await this.categoryRepository.SaveChangesAsync();
        }

        public IQueryable<T> GetAllAsQueryable<T>()
        {
            return this.categoryRepository
                .AllAsNoTracking()
                .To<T>();
        }

        public async Task<T[]> GetAllAsync<T>()
        {
            return await this.categoryRepository
                .AllAsNoTracking()
                .To<T>()
                .ToArrayAsync();
        }

        public Task<CategoryInfoViewModel> GetCategoryById(string id)
        {
            var category = this.categoryRepository.All().To<CategoryInfoViewModel>().FirstOrDefaultAsync(c => c.Id == id);

            return category;
        }

        public CategoryInfoViewModel GetCategoryByName(string name)
        {
            return this.categoryRepository.All().To<CategoryInfoViewModel>().FirstOrDefault(c => c.Name == name);
        }

        public string GetIdByName(string name)
        {
            return this.categoryRepository.All().FirstOrDefault(c => c.Name == name).Id;
        }

        public string GetNameById(string id)
        {
            return this.categoryRepository.All().FirstOrDefault(c => c.Id == id).Name;
        }
    }
}
