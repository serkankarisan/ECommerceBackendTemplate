using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
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
        [CacheAspect(60)]
        public async Task<IDataResult<CategoryDto>> GetAsync(Expression<Func<Category, bool>> filter)
        {
            CategoryDto result = _mapper.Map<CategoryDto>(await _categoryDal.GetAsync(filter));
            return result != null ? new SuccessDataResult<CategoryDto>(result, "") : new ErrorDataResult<CategoryDto>("Hata Oluştu");
        }
        [CacheAspect(60)]
        public async Task<IDataResult<Paginate<CategoryDto>>> GetAllAsync(int index, int size)
        {
            IPaginate<Category> result = await _categoryDal.GetListAsync(index: index, size: size);
            IDataResult<Paginate<CategoryDto>> dataResult = result != null ? new SuccessDataResult<Paginate<CategoryDto>>(result.ToMappedPaginate<Category, CategoryDto>(), Messages.Listed) : new ErrorDataResult<Paginate<CategoryDto>>(Messages.NotListed);
            return dataResult;
        }
        [CacheAspect(60)]
        public async Task<IDataResult<CategoryDto>> GetByCategoryIdAsync(int categoryId)
        {
            Category? result = await _categoryDal.GetAsync(p => p.Id == categoryId);
            return result != null ? new SuccessDataResult<CategoryDto>(_mapper.Map<CategoryDto>(result), Messages.Listed) : new ErrorDataResult<CategoryDto>(Messages.NotListed);
        }
        [CacheAspect(60)]
        public async Task<IDataResult<List<CategoryDto>>> GetChildCategoriesByCategoryId(int categoryId)
        {
            List<Category>? result = await _categoryDal.GetChildCategoriesByCategoryId(categoryId);
            return result != null ? new SuccessDataResult<List<CategoryDto>>(_mapper.Map<List<CategoryDto>>(result), Messages.Listed) : new ErrorDataResult<List<CategoryDto>>(_mapper.Map<List<CategoryDto>>(result), Messages.Error);
        }
        #endregion
        #region Commands
        [CacheRemoveAspect(@"
        Business.Abstract.ICategoryService.GetAsync,
        Business.Abstract.ICategoryService.GetAllAsync,
        Business.Abstract.ICategoryService.GetByCategoryIdAsync,
        Business.Abstract.ICategoryService.GetChildCategoriesByCategoryId
        ")]
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
            IResult result = Add(newCategory);
            if (result.Success)
            {
                return new SuccessDataResult<CategoryDto>(_mapper.Map<CategoryDto>(newCategory), result.Message);
            }
            else
            {
                return new ErrorDataResult<CategoryDto>(_mapper.Map<CategoryDto>(newCategory), result.Message);
            }
        }
        [CacheRemoveAspect(@"
        Business.Abstract.ICategoryService.GetAsync,
        Business.Abstract.ICategoryService.GetAllAsync,
        Business.Abstract.ICategoryService.GetByCategoryIdAsync,
        Business.Abstract.ICategoryService.GetChildCategoriesByCategoryId
        ")]
        public IResult Add(Category category)
        {
            bool result = _categoryDal.Add(category);
            return result ? new SuccessResult(Messages.Added) : new ErrorResult(Messages.NotAdded);
        }
        [CacheRemoveAspect(@"
        Business.Abstract.ICategoryService.GetAsync,
        Business.Abstract.ICategoryService.GetAllAsync,
        Business.Abstract.ICategoryService.GetByCategoryIdAsync,
        Business.Abstract.ICategoryService.GetChildCategoriesByCategoryId
        ")]
        public IResult Delete(int id)
        {
            bool result = false;
            Category category = _categoryDal.Get(p => p.Id == id);
            if (category != null)
            {
                result = _categoryDal.Delete(category);
            }
            return result ? new SuccessResult("Silindi") : new ErrorResult("Silinemedi hata oluştu");
        }
        [CacheRemoveAspect(@"
        Business.Abstract.ICategoryService.GetAsync,
        Business.Abstract.ICategoryService.GetAllAsync,
        Business.Abstract.ICategoryService.GetByCategoryIdAsync,
        Business.Abstract.ICategoryService.GetChildCategoriesByCategoryId
        ")]
        public IResult Update(Category category)
        {
            bool result = false;
            Category getCategory = _categoryDal.Get(p => p.Id == category.Id);
            if (getCategory != null)
            {
                result = _categoryDal.Update(category);
            }
            return result ? new SuccessResult("Güncellendi") : new ErrorResult("Güncellenemedi hata oluştu");
        }
        #endregion
    }
}