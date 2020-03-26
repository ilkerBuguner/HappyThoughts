namespace HappyThoughts.Services.Data.Categories
{
    using System.Linq;
    using System.Threading.Tasks;

    using HappyThoughts.Web.ViewModels.Categories;
    using HappyThoughts.Web.ViewModels.InputModels.Categories;

    public interface ICategoriesService
    {
        Task CreateAsync(CreateCategoryInputModel input);

        Task<T[]> GetAllAsync<T>();

        string GetIdByName(string name);

        string GetNameById(string id);

        CategoryInfoViewModel GetCategoryByName(string name);

        Task EditAsync(CategoryInfoViewModel categoryInfoViewModel);

        Task DeleteByIdAsync(string id);

        Task<CategoryInfoViewModel> GetCategoryById(string id);
    }
}
