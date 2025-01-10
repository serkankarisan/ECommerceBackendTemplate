using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs.Products;

namespace Business.Abstract
{
    public interface IProductImageService
    {
        public IResult Add(ProductImage productImage);
        public IResult Update(ProductImage productImage);
        public IResult Delete(int id);
        public IDataResult<ProductImage> Get(int id);
        public IDataResult<List<ProductImage>> GetAll();
        public IResult AddProductImageByProductId(AddProductImageDto addProductImageDto);
        public IResult AddDefaultProductImageByProductId(int productId);
    }
}
