﻿using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Paging;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        private IBrandDal _brandDal;
        private IMapper _mapper;
        public BrandManager(IBrandDal brandDal, IMapper mapper)
        {
            _brandDal = brandDal;
            _mapper = mapper;
        }
        #region Queries
        public async Task<IDataResult<IPaginate<Brand>>> GetAllAsync(int index, int size)
        {
            IPaginate<Brand>? result = await _brandDal.GetListAsync(index: index, size: size);
            return result != null ? new SuccessDataResult<IPaginate<Brand>>(result, Messages.Listed) : new ErrorDataResult<IPaginate<Brand>>(result, Messages.NotListed);
        }
        public async Task<IDataResult<Brand>> GetByIdAsync(int id)
        {
            Brand? result = await _brandDal.GetAsync(p => p.Id == id);
            return result != null ? new SuccessDataResult<Brand>(result, Messages.Listed) : new ErrorDataResult<Brand>(result, Messages.NotListed);
        }
        #endregion
        #region Commands
        public async Task<IResult> UpdateAsync(Brand brand)
        {
            Task<IDataResult<Brand>> updatedBrand = GetByIdAsync(brand.Id);
            if (updatedBrand == null)
            {
                return new ErrorResult(Messages.NotFound);
            }
            Brand result = await _brandDal.UpdateAsync(brand);
            return result != null ? new SuccessResult(Messages.Added) : new ErrorResult(Messages.NotAdded);
        }
        public async Task<IResult> AddAsync(Brand brand)
        {
            Brand result = await _brandDal.AddAsync(brand);
            return result != null ? new SuccessResult(Messages.Added) : new ErrorResult(Messages.NotAdded);
        }
        public async Task<IResult> DeleteAsync(int id)
        {
            IDataResult<Brand> deletedBrand = await GetByIdAsync(id);
            if (deletedBrand == null)
            {
                return new ErrorResult(Messages.NotFound);
            }
            Brand result = await _brandDal.DeleteAsync(deletedBrand.Data);
            return result != null ? new SuccessResult(Messages.Added) : new ErrorResult(Messages.NotAdded);
        }
        #endregion
    }
}
