using Damplus.Entities.DTOs;
using Damplus.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Services.Abstract
{
    public interface IPriceService
    {
        Task<IDataResult<PriceDto>> Get(int priceId);
        Task<IDataResult<PriceListDto>> GetAll();
        Task<IDataResult<PriceDto>> GetByBusiness(int businessId);
        Task<IResult> Add(PriceAddDto priceAddDto, string createdByName);
        Task<IDataResult<PriceDto>> Update(PriceUpdateDto priceUpdateDto, string modifiedByName);
        Task<IDataResult<PriceUpdateDto>> GetUpdateDto(int priceId);
    }
}
