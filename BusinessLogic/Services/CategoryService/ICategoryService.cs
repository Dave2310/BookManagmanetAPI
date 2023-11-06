
using BusinessLogic.Models.Categories;

namespace BusinessLogic.Services.CategoryService
{
    public interface ICategoryService
    {
        Task Create(CategoryRequestModel model);
        Task<CategoryResponseModel> GetById(Guid id);
        Task<List<CategoryResponseModel>> GetAll();
        Task DeleteById(Guid id);
    }
}
