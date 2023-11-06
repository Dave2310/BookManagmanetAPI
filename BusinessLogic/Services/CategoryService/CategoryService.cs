
using AutoMapper;
using BusinessLogic.Models.Categories;
using DataAccess.Entities;
using DataAccess.Repositories.BaseRepository;
using Shared.Dtos;

namespace BusinessLogic.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(IMapper mapper, IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task Create(CategoryRequestModel model)
        {
            try
            {
                var categoryDto = _mapper.Map<CategoryDto>(model);

                if(categoryDto == null)
                {
                    throw new AutoMapperMappingException();
                }

                var categoryEntity = _mapper.Map<Category>(categoryDto);
                await _categoryRepository.Create(categoryEntity);
                await _categoryRepository.SaveChanges();
            }
            catch
            {
                throw;
            }

        }

        public async Task DeleteById(Guid id)
        {
            try
            {
                var categoryEntity = await _categoryRepository.GetById(id);

                if(categoryEntity == null)
                {
                    throw new InvalidOperationException();
                }

                var categoryDto = _mapper.Map<CategoryDto>(categoryEntity);

                if(categoryDto == null)
                {
                    throw new AutoMapperMappingException();
                }

                 var categoryModel = _mapper.Map<CategoryResponseModel>(categoryDto);

                _categoryRepository.DeleteById(id);
                await _categoryRepository.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<CategoryResponseModel>> GetAll()
        {
            try
            {
                var categoryEntity = await _categoryRepository.GetAll();
                var categoryDto = _mapper.Map<List<CategoryDto>>(categoryEntity);
                var categoryModel = _mapper.Map<List<CategoryResponseModel>>(categoryDto);

                return categoryModel;
            }
            catch(InvalidOperationException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CategoryResponseModel> GetById(Guid id)
        {
            try
            {
                var categoryEntity = await _categoryRepository.GetById(id);

                if(categoryEntity == null)
                {
                    throw new Exception("Invalid id");
                }

                var categoryDto = _mapper.Map<CategoryDto>(categoryEntity);
                var categoryModel = _mapper.Map<CategoryResponseModel>(categoryDto);

                return categoryModel;
            }
            catch{ throw; }
        }
    }
}
