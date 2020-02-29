namespace HappyThoughts.Services.Data.Categories
{
    using System.Threading.Tasks;
    using HappyThoughts.Web.ViewModels.Categories;
    using HappyThoughts.Web.ViewModels.InputModels.Categories;

    public interface ICategoriesService
    {
        Task CreateAsync(CreateCategoryInputModel input);

        Task<T[]> GetAllAsync<T>();

        string GetIdByNameAsync(string name);

        string GetNameById(string id);

        Task EditAsync(CategoryInfoViewModel categoryInfoViewModel);

        Task DeleteByIdAsync(string id);
    }
}
