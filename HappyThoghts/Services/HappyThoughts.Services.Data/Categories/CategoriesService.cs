using HappyThoughts.Data.Common.Repositories;
using HappyThoughts.Data.Models;
using HappyThoughts.Services.Mapping;
using HappyThoughts.Web.ViewModels.Categories;
using HappyThoughts.Web.ViewModels.InputModels.Categories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyThoughts.Services.Data.Categories
{
    public class CategoriesService : ICategoriesService
    {
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
            };

            await this.categoryRepository.AddAsync(category);
            await this.categoryRepository.SaveChangesAsync();
        }

        public async Task<T[]> GetAllAsync<T>()
        {
            return await this.categoryRepository
                .AllAsNoTracking()
                .To<T>()
                .ToArrayAsync();
        }

        public string GetIdByNameAsync(string name)
        {
            return this.categoryRepository.AllAsNoTracking().FirstOrDefault(c => c.Name == name).Id;
        }
    }
}
