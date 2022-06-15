using AutoMapper;
using Damplus.Data.Abstract.UnitOfWorks;
using Damplus.Entities.Concrete;
using Damplus.Entities.DTOs;
using Damplus.Services.Abstract;
using Damplus.Services.Utilities;
using Damplus.Shared.Utilities.Results.Abstract;
using Damplus.Shared.Utilities.Results.ComplexTypes;
using Damplus.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Services.Concrete
{
    public class PriceManager : IPriceService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public PriceManager(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IDataResult<PriceDto>> GetByBusiness(int businessId)
        {
            var result = await _unitOfWork.Prices.AnyAsync(a => a.BusinessId == businessId);
            var booleanResult = Convert.ToBoolean(result);
            if (booleanResult)
            {
                var prices = await _unitOfWork.Prices.GetAsync(a => a.BusinessId == businessId, a => a.Business);
                if (prices!=null)
                {
                    return new DataResult<PriceDto>(ResultStatus.Succes, new PriceDto
                    {
                        Price = prices,
                        ResultStatus = ResultStatus.Succes
                    });
                }
                return new DataResult<PriceDto>(ResultStatus.Error, new PriceDto
                {
                    Price = null,
                    ResultStatus = ResultStatus.Error,
                    Message = Messages.Article.NotFound(isPlural: true)
                });
            }
            return new DataResult<PriceDto>(ResultStatus.Error, message: Messages.Article.NotFound(false), null);
        }
        public async Task<IDataResult<PriceListDto>> GetAll()
        {
            var Prices = await _unitOfWork.Prices.GetAllAsync(null,x=>x.Business);
            if (Prices.Count > -1)
            {
                return new DataResult<PriceListDto>(ResultStatus.Succes, new PriceListDto
                {
                    Prices = Prices,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<PriceListDto>(ResultStatus.Error, Messages.Video.NotFound(isPlural: true),
                new PriceListDto
                {
                    Message = Messages.Video.NotFound(isPlural: true),
                    Prices = null,
                    ResultStatus = ResultStatus.Error
                });
        }
        public async Task<IDataResult<PriceDto>> Get(int PriceId)
        {
            var Price = await _unitOfWork.Prices.GetAsync(c => c.Id == PriceId);
            if (Price != null)
            {
                return new DataResult<PriceDto>(ResultStatus.Succes, new PriceDto
                {
                    Price = Price,
                    ResultStatus = ResultStatus.Succes
                });
            }
            else
            {
                return new DataResult<PriceDto>(ResultStatus.Error, Messages.Video.NotFound(isPlural: false), new PriceDto
                {
                    Price = null,
                    ResultStatus = ResultStatus.Error,
                    Message = Messages.Video.NotFound(isPlural: false)
                });
            }
        }
        public async Task<IDataResult<PriceUpdateDto>> GetUpdateDto(int priceId)
        {
            var result = await _unitOfWork.Prices.AnyAsync(c => c.Id == priceId);
            if (result)
            {
                var Price = await _unitOfWork.Prices.GetAsync(c => c.Id == priceId);
                var PriceUpdateDto = _mapper.Map<PriceUpdateDto>(Price);
                return new DataResult<PriceUpdateDto>(ResultStatus.Succes, PriceUpdateDto);
            }
            else
            {
                return new DataResult<PriceUpdateDto>(ResultStatus.Error, Messages.Video.NotFound(isPlural: false), null);
            }
        }
        public async Task<IDataResult<PriceDto>> Update(PriceUpdateDto priceUpdateDto, string modifiedByName)
        {
            var oldPrice = await _unitOfWork.Prices.GetAsync(c => c.Id == priceUpdateDto.Id);
            var Price = _mapper.Map<PriceUpdateDto, Price>(priceUpdateDto, oldPrice);
            Price.ModifiedByName = modifiedByName;
            if (Price != null)
            {
                var updatedPrice = await _unitOfWork.Prices.UpdateAsync(Price);
                await _unitOfWork.SaveAsync();
                return new DataResult<PriceDto>(ResultStatus.Succes, Messages.Video.Add(updatedPrice.Header), new PriceDto
                {
                    Price = updatedPrice,
                    Message = Messages.Video.Add(updatedPrice.Header),
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<PriceDto>(ResultStatus.Error, message: "Xəta baş verdi", new PriceDto
            {
                Price = null,
                Message = "Xəta baş verdi",
                ResultStatus = ResultStatus.Error
            });
        }

        public async Task<IResult> Add(PriceAddDto priceAddDto, string createdByName)
        {
            var price = _mapper.Map<Price>(priceAddDto);
            price.CreatedByName = createdByName;
            price.ModifiedByName = createdByName;
            await _unitOfWork.Prices.AddAsync(price);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Succes, Messages.Article.Add(price.Header));
        }
    }
}
