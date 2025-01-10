using Core.DataAccess;
using Core.Utilities.Paging;
using Entities.Concrete;
using Entities.DTOs.Products;

namespace DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        #region Queries
        Product GetMostExpensiveProduct();
        Task<IPaginate<ProductDetailDto>> GetProductDetailDtoAsync(int index, int size);
        Task<IPaginate<ProductDetailDto>> GetProductDetailDtoByCategoryIdAsync(string categoryId, int index, int size);
        Task<IPaginate<ProductDetailDto>> GetProductDetailDtoByRelatedCategoryIdAsync(string categoryId, int index, int size);
        int GetProductsCountFromDal();
        Task<List<ProductDetailDto>> GetPopularProducts(int index = 0, int size = 20);
        Task<ProductDetailDto> GetProductDetailByIdAsync(int id);
        #endregion
        #region Commands
        #endregion
    }
}
