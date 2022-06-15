using Damplus.Entities.DTOs;
using Damplus.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Services.Abstract
{
    public interface IBusinessCategoryService
    {
        Task<IDataResult<BusinessCategoryDto>> Get(int BusinessCategoryId);
        Task<IDataResult<BusinessCategoryUpdateDto>> GetUpdateDto(int BusinessCategoryId);
        Task<IDataResult<BusinessCategoryListDto>> GetAll();
        Task<IDataResult<BusinessCategoryListDto>> GetAllByNonDeleteAndActive();
        Task<IDataResult<BusinessCategoryDto>> Add(BusinessCategoryAddDto BusinessCategoryAddDto, string createdByName);
        Task<IDataResult<BusinessCategoryDto>> Update(BusinessCategoryUpdateDto BusinessCategoryUpdateDto, string modifiedByName);
        Task<IResult> HardDelete(int BusinessCategoryId);
    }
}
