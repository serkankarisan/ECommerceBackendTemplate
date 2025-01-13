using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.Utilities.File;
using Business.ValidationRules.FluentValidations;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Dynamic;
using Core.Utilities.Paging;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.Products;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        private readonly IProductImageService _productImageService;
        private readonly IProductImageUploadService _productImageUploadService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductManager(IProductDal productDal, IProductImageUploadService productImageUploadService, IProductImageService productImageService, IMapper mapper, ICategoryService categoryService)
        {
            _productDal = productDal;
            _productImageUploadService = productImageUploadService;
            _productImageService = productImageService;
            _mapper = mapper;
            _categoryService = categoryService;
        }
        #region Queries
        [TransactionScopeAspect]
        [CacheRemoveAspect("IProductService.Get")]
        public IDataResult<Product> Get(int id)
        {
            Product product = _productDal.Get(p => p.Id == id);
            return product != null ? new SuccessDataResult<Product>(product, "") : new ErrorDataResult<Product>("Hata Oluştu");
        }
        //[CacheAspect]
        //[SecuredOperation("getall")]
        public async Task<IDataResult<IPaginate<Product>>> GetAllAsync(int index = 0, int size = 50)
        {
            IPaginate<Product> result = await _productDal.GetListAsync(index: index, size: size);
            return result != null ? new SuccessDataResult<IPaginate<Product>>(result, "Listelendi") : new ErrorDataResult<IPaginate<Product>>("Hata Oluştu");
        }
        public IDataResult<Product> GetMostExpensiveProduct()
        {
            Product result = _productDal.GetMostExpensiveProduct();
            return result != null ? new SuccessDataResult<Product>(result, "") : new ErrorDataResult<Product>("Hata Oluştu");
        }
        public IDataResult<IPaginate<Product>> GetListDynamic(int index, int size, Dynamic dynamic)
        {
            return new SuccessDataResult<IPaginate<Product>>(_productDal.GetListByDynamic(size: size, index: index, dynamic: dynamic), Messages.Listed);
        }
        public async Task<IDataResult<IPaginate<ProductDetailDto>>> GetProductDetailDtoAsync(int index, int size)
        {
            IPaginate<ProductDetailDto> result = await _productDal.GetProductDetailDtoAsync(index, size);
            return new SuccessDataResult<IPaginate<ProductDetailDto>>(result, Messages.Listed);
        }
        public async Task<IDataResult<IPaginate<ProductDetailDto>>> GetProductDetailDtoByCategoryIdAsync(int categoryId, int index, int size)
        {
            IPaginate<ProductDetailDto> result = await _productDal.GetProductDetailDtoByCategoryIdAsync(categoryId, index, size);
            return new SuccessDataResult<IPaginate<ProductDetailDto>>(result, Messages.Listed);
        }
        public async Task<IDataResult<IPaginate<ProductDetailDto>>> GetRelatedProductsByProductId(int productId, int index = 0, int size = 20)
        {
            Product product = _productDal.Get(p => p.Id == productId);
            if (product == null)
                return new ErrorDataResult<IPaginate<ProductDetailDto>>(Messages.Error);

            IPaginate<ProductDetailDto> result = await _productDal.GetProductDetailDtoByCategoryIdAsync(categoryId: product.CategoryId, index: index, size: size);
            return new SuccessDataResult<IPaginate<ProductDetailDto>>(result);
        }
        public async Task<IDataResult<IPaginate<ProductDetailDto>>> GetRelatedProductsByCategoryId(string categoryName, int index = 0, int size = 20)
        {
            IDataResult<Entities.DTOs.Categories.CategoryDto> categoryResult = await _categoryService.GetAsync(q => q.Name == categoryName);
            if (categoryResult.Success || categoryResult.Data == null)
                return new ErrorDataResult<IPaginate<ProductDetailDto>>(Messages.Error);

            IPaginate<ProductDetailDto> result = await _productDal.GetProductDetailDtoByCategoryIdAsync(categoryId: categoryResult.Data.Id, index: index, size: size);
            return new SuccessDataResult<IPaginate<ProductDetailDto>>(result);
        }
        public Task<List<ProductDetailDto>> GetPopularProducts(int index = 0, int size = 20)
        {
            return _productDal.GetPopularProducts(index: index, size: size);
        }
        public async Task<IDataResult<ProductDetailDto>> GetProductDetailByIdAsync(int id)
        {
            ProductDetailDto result = await _productDal.GetProductDetailByIdAsync(id);
            return result != null ? new SuccessDataResult<ProductDetailDto>(result) : new ErrorDataResult<ProductDetailDto>(Messages.NotFound);
        }
        #endregion
        #region Commands
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(AddProductDto addProductDto)
        {
            Random rnd = new Random();
            Product product = _mapper.Map<Product>(addProductDto);
            bool result = false;
            //for (int i = 0; i <= 999; i++)
            {
                //product.Id = 0;
                //product.CategoryId = "1-2";
                //IResult bussinessRules = BusinessRules.Run(categoryIsExist(product.CategoryId));
                //if (bussinessRules != null)
                //{
                //    return bussinessRules;
                //}c
                result = _productDal.Add(product);
                if (result)
                    _productImageService.AddDefaultProductImageByProductId(product.Id);
            }
            return result ? new SuccessResult("eklendi") : new ErrorResult("eklenemedi hata oluştu");
        }
        public IResult AddRange(List<Product> products)
        {
            bool result = _productDal.AddRange(products);
            return result ? new SuccessResult("eklendi") : new ErrorResult("eklenemedi hata oluştu");
        }
        public IResult Delete(int id)
        {
            bool result = false;
            Product product = _productDal.Get(p => p.Id == id);
            if (product != null)
            {
                result = _productDal.Delete(product);
            }
            return result ? new SuccessResult("Silindi") : new ErrorResult("Silinemedi hata oluştu");
        }
        public IResult Update(Product product)
        {
            bool result = false;
            Product getProduct = _productDal.Get(p => p.Id == product.Id);
            if (getProduct != null)
            {
                result = _productDal.Update(product);
            }
            return result ? new SuccessResult("Güncellendi") : new ErrorResult("Güncellenemedi hata oluştu");
        }
        public IResult AddWithImage(AddProductWithImageDto addProductWithImageDto)
        {
            Product product = _mapper.Map<Product>(addProductWithImageDto);
            bool result = _productDal.Add(product);
            if (result)
            {
                for (int i = 0; i < addProductWithImageDto.IFormFiles.Count(); i++)
                {
                    (string, bool) imageResult = _productImageUploadService.AddImage(addProductWithImageDto.IFormFiles[i]);
                    if (imageResult.Item2)
                    {
                        IResult uploadingResult = _productImageService.Add(new ProductImage { ImageUrl = imageResult.Item1, Name = product.Name, ProductId = product.Id }); ;
                        //if (!uploadingResult.Success)
                        //{
                        //    return new ErrorResult(Messages.Error);
                        //}
                    }
                }
                return new SuccessResult(Messages.Added);
            }
            else
            {
                return new ErrorResult(Messages.NotAdded);
            }
        }
        #endregion
        #region Rules
        public int GetProductsCountFromDal()
        {
            int totalCount = _productDal.GetProductsCountFromDal();//46 ms avg.
            return totalCount;
        }
        public int GetProductsCountFromBussines()
        {
            int totalCount = _productDal.GetAll().Count();//1.234 ms avg.
            return totalCount;
        }
        #endregion
    }
}
