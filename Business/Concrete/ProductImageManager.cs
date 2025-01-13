using Business.Abstract;
using Business.Constants;
using Business.Utilities.File;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.Products;

namespace Business.Concrete
{
    public class ProductImageManager : IProductImageService
    {
        private readonly IProductImageDal _productImageDal;
        private readonly IProductImageUploadService _productImageUploadService;

        public ProductImageManager(IProductImageDal productImageDal, IProductImageUploadService productImageUploadService)
        {
            _productImageDal = productImageDal;
            _productImageUploadService = productImageUploadService;
        }
        #region Queries
        public IDataResult<ProductImage> Get(int id)
        {
            ProductImage result = _productImageDal.Get(p => p.Id == id);
            return result != null ? new SuccessDataResult<ProductImage>(result, Messages.Listed) : new ErrorDataResult<ProductImage>(Messages.NotListed);
        }
        public IDataResult<List<ProductImage>> GetAll()
        {
            List<ProductImage> result = _productImageDal.GetAll();
            return result != null ? new SuccessDataResult<List<ProductImage>>(result, Messages.Listed) : new ErrorDataResult<List<ProductImage>>(Messages.NotListed);
        }
        #endregion
        #region Commands
        public IResult Add(ProductImage productImage)
        {
            bool result = _productImageDal.Add(productImage);
            return result ? new SuccessResult(Messages.Added) : new ErrorResult(Messages.NotAdded);
        }
        public IResult Delete(int id)
        {
            ProductImage productImage = _productImageDal.Get(p => p.Id == id);
            if (productImage == null)
                return new ErrorResult(Messages.NotFound);
            bool result = _productImageDal.Delete(productImage);
            return result ? new SuccessResult(Messages.Deleted) : new ErrorResult(Messages.NotDeleted);
        }
        public IResult Update(ProductImage productImage)
        {
            ProductImage getProductImage = _productImageDal.Get(p => p.Id == productImage.Id);
            if (getProductImage == null)
                return new ErrorResult(Messages.NotFound);
            bool result = _productImageDal.Update(productImage);
            return result ? new SuccessResult(Messages.Updated) : new ErrorResult(Messages.NotUpdated);
        }
        public IResult AddProductImageByProductId(AddProductImageDto addProductImageDto)
        {
            bool uploadingResult = false;
            for (int i = 0; i < addProductImageDto.IFormFiles.Count(); i++)
            {
                (string, bool) imageResult = _productImageUploadService.AddImage(addProductImageDto.IFormFiles[i]);
                if (imageResult.Item2)
                {
                    uploadingResult = Add(new ProductImage { ImageUrl = imageResult.Item1, Name = imageResult.Item1, ProductId = addProductImageDto.ProductId }).Success;
                }
            }
            return uploadingResult ? new SuccessResult(Messages.Added) : new ErrorResult(Messages.Error);
        }
        //Todo: image url config den alınabilir veya başka bir çözüm olabilir. değişecek.
        public IResult AddDefaultProductImageByProductId(int productId)
        {
            return Add(new ProductImage { ImageUrl = "\\ProductImages\\defaultFile.jpg", Name = "defaultFile.jpg", ProductId = productId });
        }
        #endregion
    }
}
