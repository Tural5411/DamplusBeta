using Damplus.Entities.DTOs;
using Damplus.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Services.Abstract
{
    public interface IBannerService
    {
        Task<IDataResult<BannerDto>> Get(int bannerId);
        Task<IDataResult<BannerListDto>> GetAll();
        Task<IDataResult<BannerDto>> Add(BannerAddDto bannerAddDto, string createdByName);
        Task<IDataResult<BannerDto>> Update(BannerUpdateDto bannerUpdateDto, string modifiedByName);
        Task<IDataResult<BannerUpdateDto>> GetUpdateDto(int bannerId);
        Task<IResult> HardDelete(int bannerId);
    }
}
