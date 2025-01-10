using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Paging;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.Categories;
using System.Linq.Expressions;
using Newtonsoft.Json;


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
        public IDataResult<Category> Get(int id)
        {
            var result = _categoryDal.Get(p => p.Id == id);
            return result != null ? new SuccessDataResult<Category>(result, "") : new ErrorDataResult<Category>("Hata Oluştu");
        }
        public async Task<IDataResult<IPaginate<Category>>> GetAllAsync(int index, int size)
        {
            var result = await _categoryDal.GetListAsync(index: index, size: size);
            return result != null ? new SuccessDataResult<IPaginate<Category>>(result, Messages.Listed) : new ErrorDataResult<IPaginate<Category>>(Messages.NotListed);
        }
        public async Task<IDataResult<Category>> GetByCategoryIdAsync(string categoryId)
        {
            var result = await _categoryDal.GetAsync(p => p.CategoryId == categoryId);
            return result != null ? new SuccessDataResult<Category>(result, Messages.Listed) : new ErrorDataResult<Category>(Messages.NotListed);
        }
        public async Task<IDataResult<List<Category>>> GetChildCategoriesByCategoryId(string categoryId)
        {
            var result = await _categoryDal.GetChildCategoriesByCategoryId(categoryId);
            return result != null ? new SuccessDataResult<List<Category>>(result, Messages.Listed) : new ErrorDataResult<List<Category>>(result, Messages.Error);
        }
        #endregion
        #region Commands
        public IResult AddWithDto(AddCategoryDto addCategoryDto)
        {
            string categoryId = "";
            if (addCategoryDto.ParentCategoryId == "")
            {
                categoryId = (_categoryDal.GetAllParentCategoryCount() + 1).ToString();
            }
            else
            {
                Category parentCategory = _categoryDal.GetFirstOrDefault(k => k.CategoryId == addCategoryDto.ParentCategoryId);
                if (parentCategory != null)
                {
                    int numberOfChildCategories = getCategoryTableCount(k => k.CategoryId.StartsWith(addCategoryDto.ParentCategoryId));
                    categoryId = parentCategory.CategoryId + "-" + (numberOfChildCategories).ToString();
                    if (_categoryDal.IsExist(p => p.CategoryId == categoryId))
                    {
                        int controlValue = 0;
                        int period = 1;
                        while (controlValue < 1)
                        {
                            period++;
                            categoryId = parentCategory.CategoryId + "-" + (numberOfChildCategories + period).ToString();
                            if (!_categoryDal.IsExist(p => p.CategoryId == categoryId))
                            {
                                controlValue++;
                            }
                        }
                    }
                }
                else
                {
                    return new ErrorResult(Messages.ParentCategoryNotFound);
                }
            }

            Category newCategory = new Category
            {
                Name = addCategoryDto.Name,
                CategoryId = categoryId
            };
            return Add(newCategory);
        }
        public Category AddCategoryWithDto(AddCategoryDto addCategoryDto)
        {
            string categoryId = "";
            if (addCategoryDto.ParentCategoryId == "")
            {
                categoryId = (_categoryDal.GetAllParentCategoryCount() + 1).ToString();
            }
            else
            {
                Category parentCategory = _categoryDal.GetFirstOrDefault(k => k.CategoryId == addCategoryDto.ParentCategoryId);
                if (parentCategory != null)
                {
                    int numberOfChildCategories = getCategoryTableCount(k => k.CategoryId.StartsWith(addCategoryDto.ParentCategoryId));
                    categoryId = parentCategory.CategoryId + "-" + (numberOfChildCategories).ToString();
                    if (_categoryDal.IsExist(p => p.CategoryId == categoryId))
                    {
                        int controlValue = 0;
                        int period = 1;
                        while (controlValue < 1)
                        {
                            period++;
                            categoryId = parentCategory.CategoryId + "-" + (numberOfChildCategories + period).ToString();
                            if (!_categoryDal.IsExist(p => p.CategoryId == categoryId))
                            {
                                controlValue++;
                            }
                        }
                    }
                }
                else
                {
                    return null;
                }
            }

            Category newCategory = new Category
            {
                Name = addCategoryDto.Name,
                CategoryId = categoryId
            };
            Add(newCategory);
            return newCategory;
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
        public IResult AddChildCategory(AddChildCategoryDto addChildCategoryDto)
        {
            var category = _mapper.Map<Category>(addChildCategoryDto);
            var result = Add(category);
            return result.Success ? new SuccessResult(Messages.Added) : new ErrorResult(Messages.NotAdded);
        }
        #endregion
        private int getCategoryTableCount(Expression<Func<Category, bool>> filter = null)
        {
            return _categoryDal.GetTableCount(filter);
        }

        public bool CategoryIsExist(string categoryId)
        {
           return _categoryDal.CategoryIsExist(categoryId);
        }
    }
}