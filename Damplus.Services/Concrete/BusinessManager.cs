using AutoMapper;
using Damplus.Shared.Utilities.Results.Abstract;
using Damplus.Shared.Utilities.Results.ComplexTypes;
using Damplus.Shared.Utilities.Results.Concrete;
using Damplus.Data.Abstract.UnitOfWorks;
using Damplus.Entities.Concrete;
using Damplus.Entities.DTOs;
using Damplus.Services.Abstract;
using Damplus.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Services.Concrete
{
    public class BusinessManager : IBusinessService
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BusinessManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IDataResult<BusinessDto>> Add(BusinessAddDto BusinessAddDto, string createdByName)
        {
            var Business = _mapper.Map<Business>(BusinessAddDto);
            Business.CreatedByName = createdByName;
            Business.ModifiedByName = createdByName;
            var addedBusiness = await _unitOfWork.Business.AddAsync(Business);
            await _unitOfWork.SaveAsync();
            return new DataResult<BusinessDto>(ResultStatus.Succes, Messages.Business.Add(addedBusiness.Title), new BusinessDto
            {
                Business = addedBusiness,
                ResultStatus = ResultStatus.Succes,
                Message = Messages.Business.Add(addedBusiness.Title)
            });
        }
        public async Task<IDataResult<BusinessDto>> Update(BusinessUpdateDto BusinessUpdateDto, string modifiedByName)
        {
            var oldBusiness = await _unitOfWork.Business.GetAsync(c => c.Id == BusinessUpdateDto.Id);
            var Business = _mapper.Map<BusinessUpdateDto, Business>(BusinessUpdateDto, oldBusiness);
            Business.ModifiedByName = modifiedByName;
            if (Business != null)
            {
                var updatedBusiness = await _unitOfWork.Business.UpdateAsync(Business);
                await _unitOfWork.SaveAsync();
                return new DataResult<BusinessDto>(ResultStatus.Succes, Messages.Business.Add(updatedBusiness.Title), new BusinessDto
                {
                    Business = updatedBusiness,
                    Message = Messages.Business.Add(updatedBusiness.Title),
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<BusinessDto>(ResultStatus.Error, message: "Xəta baş verdi", new BusinessDto
            {
                Business = null,
                Message = "Xəta baş verdi",
                ResultStatus = ResultStatus.Error
            });
        }
        public async Task<IDataResult<BusinessUpdateDto>> GetUpdateDto(int BusinessId)
        {
            var result = await _unitOfWork.Business.AnyAsync(c => c.Id == BusinessId);
            if (result)
            {
                var Business = await _unitOfWork.Business.GetAsync(c => c.Id == BusinessId);
                var BusinessUpdateDto = _mapper.Map<BusinessUpdateDto>(Business);
                await _unitOfWork.Business.UpdateAsync(Business);
                await _unitOfWork.SaveAsync();
                return new DataResult<BusinessUpdateDto>(ResultStatus.Succes, BusinessUpdateDto);
            }
            else
            {
                return new DataResult<BusinessUpdateDto>(ResultStatus.Error, Messages.Business.NotFound(isPlural: false), null);
            }
        }
        public async Task<IResult> HardDelete(int BusinessId)
        {
            var Business = await _unitOfWork.Business.GetAsync(c => c.Id == BusinessId);
            if (Business != null)
            {
                await _unitOfWork.Business.DeleteAsync(Business);
                await _unitOfWork.SaveAsync();

                return new Result(ResultStatus.Succes, Messages.Business.HardDelete(Business.Title));
            }
            else
            {
                return new Result(ResultStatus.Error, message:
                   $"{Business.Title} adlı Business silinə bilmədi, təkrar yoxlayın");
            }
        }

        public async Task<IDataResult<BusinessDto>> Get(int BusinessId)
        {
            var Business = await _unitOfWork.Business.GetAsync(c => c.Id == BusinessId);
            if (Business != null)
            {
                return new DataResult<BusinessDto>(ResultStatus.Succes, new BusinessDto
                {
                    Business = Business,
                    ResultStatus = ResultStatus.Succes
                });
            }
            else
            {
                return new DataResult<BusinessDto>(ResultStatus.Error, Messages.Business.NotFound(isPlural: false), new BusinessDto
                {
                    Business = null,
                    ResultStatus = ResultStatus.Error,
                    Message = Messages.Business.NotFound(isPlural: false)
                });
            }
        }
        public async Task<IDataResult<BusinessListDto>> GetAll()
        {
            var Business = await _unitOfWork.Business.GetAllAsync(null);
            if (Business.Count > -1)
            {
                return new DataResult<BusinessListDto>(ResultStatus.Succes, new BusinessListDto
                {
                    Businesses = Business,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<BusinessListDto>(ResultStatus.Error, Messages.Business.NotFound(isPlural: true),
                new BusinessListDto
                {
                    Message = Messages.Business.NotFound(isPlural: true),
                    Businesses = null,
                    ResultStatus = ResultStatus.Error
                });
        }
        public async Task<IDataResult<BusinessListDto>> GetAllByNonDeleteAndActive()
        {
            var Business = await _unitOfWork.Business.GetAllAsync(c => c.IsActive && !c.IsDeleted);
            if (Business.Count > -1)
            {
                return new DataResult<BusinessListDto>(ResultStatus.Succes, new BusinessListDto
                {
                    Businesses = Business,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<BusinessListDto>(ResultStatus.Error, Messages.Business.NotFound(isPlural: true), null);
        }
    }
}
