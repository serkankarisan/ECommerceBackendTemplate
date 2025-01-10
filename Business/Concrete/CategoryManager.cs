using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Extensions;
using Core.Utilities.Paging;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.Categories;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        private readonly IMapper _mapper;
        public CategoryManager(ICategoryDal categoryDal, IMapper mapper)
        {
            _categoryDal = categoryDal;
            _mapper = mapper;
        }
        #region Queries
        public IDataResult<CategoryDto> Get(Expression<Func<Category, bool>> filter)
        {
            var result = _mapper.Map<CategoryDto>(_categoryDal.Get(filter));
            return result != null ? new SuccessDataResult<CategoryDto>(result, "") : new ErrorDataResult<CategoryDto>("Hata Oluştu");
        }
        public async Task<IDataResult<Paginate<CategoryDto>>> GetAllAsync(int index, int size)
        {
            var result = await _categoryDal.GetListAsync(index: index, size: size);
            return result != null ? new SuccessDataResult<Paginate<CategoryDto>>(result.ToMappedPaginate<Category, CategoryDto>(), Messages.Listed) : new ErrorDataResult<Paginate<CategoryDto>>(Messages.NotListed);
        }
        public async Task<IDataResult<CategoryDto>> GetByCategoryIdAsync(int categoryId)
        {
            var result = await _categoryDal.GetAsync(p => p.Id == categoryId);
            return result != null ? new SuccessDataResult<CategoryDto>(_mapper.Map<CategoryDto>(result), Messages.Listed) : new ErrorDataResult<CategoryDto>(Messages.NotListed);
        }
        public async Task<IDataResult<List<CategoryDto>>> GetChildCategoriesByCategoryId(int categoryId)
        {
            var result = await _categoryDal.GetChildCategoriesByCategoryId(categoryId);
            return result != null ? new SuccessDataResult<List<CategoryDto>>(_mapper.Map<List<CategoryDto>>(result), Messages.Listed) : new ErrorDataResult<List<CategoryDto>>(_mapper.Map<List<CategoryDto>>(result), Messages.Error);
        }
        #endregion
        #region Commands
        public IDataResult<CategoryDto> AddWithDto(AddCategoryDto addCategoryDto)
        {
            if (addCategoryDto.ParentCategoryId != null)
            {
                if (!_categoryDal.IsExist(p => p.Id == addCategoryDto.ParentCategoryId))
                {
                    return new ErrorDataResult<CategoryDto>(Messages.ParentCategoryNotFound);
                }
            }

            Category newCategory = new Category
            {
                Name = addCategoryDto.Name,
                Description = addCategoryDto.Description,
                ParentCategoryId = addCategoryDto.ParentCategoryId
            };
            var result = Add(newCategory);
            if (result.Success)
            {
                return new SuccessDataResult<CategoryDto>(_mapper.Map<CategoryDto>(newCategory), result.Message);
            }
            else
            {
                return new ErrorDataResult<CategoryDto>(_mapper.Map<CategoryDto>(newCategory), result.Message);
            }
        }
        public IResult Add(Category category)
        {
            var result = _categoryDal.Add(category);
            return result ? new SuccessResult(Messages.Added) : new ErrorResult(Messages.NotAdded);
        }
        public IResult Delete(int id)
        {
            var result = false;
            var category = _categoryDal.Get(p => p.Id == id);
            if (category != null)
            {
                result = _categoryDal.Delete(category);
            }
            return result ? new SuccessResult("Silindi") : new ErrorResult("Silinemedi hata oluştu");
        }
        public IResult Update(Category category)
        {
            var result = false;
            var getCategory = _categoryDal.Get(p => p.Id == category.Id);
            if (getCategory != null)
            {
                result = _categoryDal.Update(category);
            }
            return result ? new SuccessResult("Güncellendi") : new ErrorResult("Güncellenemedi hata oluştu");
        }
        #endregion

        public bool CategoryIsExist(string name)
        {
            return _categoryDal.CategoryIsExist(name);
        }
    }
}