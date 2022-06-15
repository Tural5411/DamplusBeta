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
    public class BusinessCategoryManager : IBusinessCategoryService
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BusinessCategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IDataResult<BusinessCategoryDto>> Add(BusinessCategoryAddDto BusinessCategoryAddDto, string createdByName)
        {
            var businessCategory = _mapper.Map<BusinessCategory>(BusinessCategoryAddDto);
            businessCategory.CreatedByName = createdByName;
            businessCategory.ModifiedByName = createdByName;
            var addedBusinessCategory = await _unitOfWork.BusinessCategories.AddAsync(businessCategory);
            await _unitOfWork.SaveAsync();
            return new DataResult<BusinessCategoryDto>(ResultStatus.Succes, Messages.Business.Add(addedBusinessCategory.Name), new BusinessCategoryDto
            {
                BusinessCategory = addedBusinessCategory,
                ResultStatus = ResultStatus.Succes,
                Message = Messages.Business.Add(addedBusinessCategory.Name)
            });
        }
        public async Task<IDataResult<BusinessCategoryDto>> Get(int BusinessCategoryId)
        {
            var BusinessCategory = await _unitOfWork.BusinessCategories.GetAsync(c => c.Id == BusinessCategoryId);
            if (BusinessCategory != null)
            {
                return new DataResult<BusinessCategoryDto>(ResultStatus.Succes, new BusinessCategoryDto
                {
                    BusinessCategory = BusinessCategory,
                    ResultStatus = ResultStatus.Succes
                });
            }
            else
            {
                return new DataResult<BusinessCategoryDto>(ResultStatus.Error, Messages.Business.NotFound(isPlural: false), new BusinessCategoryDto
                {
                    BusinessCategory = null,
                    ResultStatus = ResultStatus.Error,
                    Message = Messages.Business.NotFound(isPlural: false)
                });
            }
        }
        public async Task<IDataResult<BusinessCategoryListDto>> GetAll()
        {
            var BusinessCategorys = await _unitOfWork.BusinessCategories.GetAllAsync(null);
            if (BusinessCategorys.Count > -1)
            {
                return new DataResult<BusinessCategoryListDto>(ResultStatus.Succes, new BusinessCategoryListDto
                {
                    BusinessCategories = BusinessCategorys,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<BusinessCategoryListDto>(ResultStatus.Error, Messages.Business.NotFound(isPlural: true),
                new BusinessCategoryListDto
                {
                    Message = Messages.Business.NotFound(isPlural: true),
                    BusinessCategories = null,
                    ResultStatus = ResultStatus.Error
                });
        }
        public async Task<IDataResult<BusinessCategoryListDto>> GetAllByNonDeleteAndActive()
        {
            var BusinessCategorys = await _unitOfWork.BusinessCategories.GetAllAsync(c => c.IsActive && !c.IsDeleted);
            if (BusinessCategorys.Count > -1)
            {
                return new DataResult<BusinessCategoryListDto>(ResultStatus.Succes, new BusinessCategoryListDto
                {
                    BusinessCategories = BusinessCategorys,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<BusinessCategoryListDto>(ResultStatus.Error, Messages.Business.NotFound(isPlural: true), null);
        }
        public async Task<IDataResult<BusinessCategoryUpdateDto>> GetUpdateDto(int BusinessCategoryId)
        {
            var result = await _unitOfWork.BusinessCategories.AnyAsync(c => c.Id == BusinessCategoryId);
            if (result)
            {
                var BusinessCategory = await _unitOfWork.BusinessCategories.GetAsync(c => c.Id == BusinessCategoryId);
                var BusinessCategoryUpdateDto = _mapper.Map<BusinessCategoryUpdateDto>(BusinessCategory);
                return new DataResult<BusinessCategoryUpdateDto>(ResultStatus.Succes, BusinessCategoryUpdateDto);
            }
            else
            {
                return new DataResult<BusinessCategoryUpdateDto>(ResultStatus.Error, Messages.Business.NotFound(isPlural: false), null);
            }
        }
        public async Task<IResult> HardDelete(int BusinessCategoryId)
        {
            var BusinessCategory = await _unitOfWork.BusinessCategories.GetAsync(c => c.Id == BusinessCategoryId);
            if (BusinessCategory != null)
            {
                await _unitOfWork.BusinessCategories.DeleteAsync(BusinessCategory);
                await _unitOfWork.SaveAsync();

                return new Result(ResultStatus.Succes, Messages.Business.HardDelete(BusinessCategory.Name));
            }
            else
            {
                return new Result(ResultStatus.Error, message:
                   $"{BusinessCategory.Name} adlı kateqoriya silinə bilmədi, təkrar yoxlayın");
            }
        }
        public async Task<IDataResult<BusinessCategoryDto>> Update(BusinessCategoryUpdateDto BusinessCategoryUpdateDto, string modifiedByName)
        {
            var oldBusinessCategory = await _unitOfWork.BusinessCategories.GetAsync(c => c.Id == BusinessCategoryUpdateDto.Id);
            var BusinessCategory = _mapper.Map<BusinessCategoryUpdateDto, BusinessCategory>(BusinessCategoryUpdateDto, oldBusinessCategory);
            BusinessCategory.ModifiedByName = modifiedByName;
            if (BusinessCategory != null)
            {
                var updatedBusinessCategory = await _unitOfWork.BusinessCategories.UpdateAsync(BusinessCategory);
                await _unitOfWork.SaveAsync();
                return new DataResult<BusinessCategoryDto>(ResultStatus.Succes, Messages.Business.Add(updatedBusinessCategory.Name), new BusinessCategoryDto
                {
                    BusinessCategory = updatedBusinessCategory,
                    Message = Messages.Business.Add(updatedBusinessCategory.Name),
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<BusinessCategoryDto>(ResultStatus.Error, message: "Xəta baş verdi", new BusinessCategoryDto
            {
                BusinessCategory = null,
                Message = "Xəta baş verdi",
                ResultStatus = ResultStatus.Error
            });
        }
    }
}
