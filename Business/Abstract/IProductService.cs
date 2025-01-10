using Core.Utilities.Dynamic;
using Core.Utilities.Paging;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs.Products;

namespace Business.Abstract
{
    public interface IProductService
    {
        #region Queries
        IDataResult<Product> Get(int id);
        Task<IDataResult<IPaginate<Product>>> GetAllAsync(int index, int size);
        IDataResult<Product> GetMostExpensiveProduct();
        IResult AddWithImage(AddProductWithImageDto addProductWithImageDto);
        IDataResult<IPaginate<Product>> GetListDynamic(int index, int size, Dynamic dynamic);
        Task<IDataResult<IPaginate<ProductDetailDto>>> GetProductDetailDtoAsync(int index, int size);
        Task<IDataResult<IPaginate<ProductDetailDto>>> GetProductDetailDtoByCategoryIdAsync(string categoryId, int index, int size);
        Task<IDataResult<IPaginate<ProductDetailDto>>> GetProductDetailDtoByRelatedCategoryIdAsync(string categoryId, int index, int size);
        int GetProductsCountFromDal();
        int GetProductsCountFromBussines();
        Task<IDataResult<IPaginate<ProductDetailDto>>> GetRelatedProductsByProductId(int productId, int index = 0, int size = 20);
        Task<IDataResult<IPaginate<ProductDetailDto>>> GetRelatedProductsByCategoryId(string categoryId, int index = 0, int size = 20);
        Task<List<ProductDetailDto>> GetPopularProducts(int index = 0, int size = 20);
        Task<IDataResult<ProductDetailDto>> GetProductDetailByIdAsync(int id);
        #endregion
        #region Commands
        IResult Add(AddProductDto addProductDto);
        IResult AddRange(List<Product> products);
        IResult Update(Product product);
        IResult Delete(int id);
        #endregion     
    }
}
