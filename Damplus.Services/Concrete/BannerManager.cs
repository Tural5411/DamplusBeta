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
    public class BannerManager:IBannerService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public BannerManager(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IDataResult<BannerListDto>> GetAll()
        {
            var Banners = await _unitOfWork.Banners.GetAllAsync(null);
            if (Banners.Count > -1)
            {
                return new DataResult<BannerListDto>(ResultStatus.Succes, new BannerListDto
                {
                    Banners = Banners,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<BannerListDto>(ResultStatus.Error, Messages.Video.NotFound(isPlural: true),
                new BannerListDto
                {
                    Message = Messages.Video.NotFound(isPlural: true),
                    Banners = null,
                    ResultStatus = ResultStatus.Error
                });
        }
        public async Task<IDataResult<BannerDto>> Get(int BannerId)
        {
            var Banner = await _unitOfWork.Banners.GetAsync(c => c.Id == BannerId);
            if (Banner != null)
            {
                return new DataResult<BannerDto>(ResultStatus.Succes, new BannerDto
                {
                    Banner = Banner,
                    ResultStatus = ResultStatus.Succes
                });
            }
            else
            {
                return new DataResult<BannerDto>(ResultStatus.Error, Messages.Video.NotFound(isPlural: false), new BannerDto
                {
                    Banner = null,
                    ResultStatus = ResultStatus.Error,
                    Message = Messages.Video.NotFound(isPlural: false)
                });
            }
        }
        public async Task<IDataResult<BannerDto>> Add(BannerAddDto BannerAddDto, string createdByName)
        {

            var Banner = _mapper.Map<Banner>(BannerAddDto);
            Banner.CreatedByName = createdByName;
            Banner.ModifiedByName = createdByName;
            var addedBanner = await _unitOfWork.Banners.AddAsync(Banner);
            await _unitOfWork.SaveAsync();
            return new DataResult<BannerDto>(ResultStatus.Succes, Messages.Video.Add(addedBanner.Title), new BannerDto
            {
                Banner = addedBanner,
                ResultStatus = ResultStatus.Succes,
                Message = Messages.Video.Add(addedBanner.Title)
            });
        }
        public async Task<IDataResult<BannerUpdateDto>> GetUpdateDto(int BannerId)
        {
            var result = await _unitOfWork.Banners.AnyAsync(c => c.Id == BannerId);
            if (result)
            {
                var Banner = await _unitOfWork.Banners.GetAsync(c => c.Id == BannerId);
                var BannerUpdateDto = _mapper.Map<BannerUpdateDto>(Banner);
                return new DataResult<BannerUpdateDto>(ResultStatus.Succes, BannerUpdateDto);
            }
            else
            {
                return new DataResult<BannerUpdateDto>(ResultStatus.Error, Messages.Video.NotFound(isPlural: false), null);
            }
        }
        public async Task<IDataResult<BannerDto>> Update(BannerUpdateDto BannerUpdateDto, string modifiedByName)
        {
            var oldBanner = await _unitOfWork.Banners.GetAsync(c => c.Id == BannerUpdateDto.Id);
            var Banner = _mapper.Map<BannerUpdateDto, Banner>(BannerUpdateDto, oldBanner);
            Banner.ModifiedByName = modifiedByName;
            if (Banner != null)
            {
                var updatedBanner = await _unitOfWork.Banners.UpdateAsync(Banner);
                await _unitOfWork.SaveAsync();
                return new DataResult<BannerDto>(ResultStatus.Succes, Messages.Video.Add(updatedBanner.Title), new BannerDto
                {
                    Banner = updatedBanner,
                    Message = Messages.Video.Add(updatedBanner.Title),
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<BannerDto>(ResultStatus.Error, message: "Xəta baş verdi", new BannerDto
            {
                Banner = null,
                Message = "Xəta baş verdi",
                ResultStatus = ResultStatus.Error
            });
        }
        public async Task<IResult> HardDelete(int BannerId)
        {
            var Banner = await _unitOfWork.Banners.GetAsync(c => c.Id == BannerId);
            if (Banner != null)
            {
                await _unitOfWork.Banners.DeleteAsync(Banner);
                await _unitOfWork.SaveAsync();

                return new Result(ResultStatus.Succes, Messages.Video.HardDelete(Banner.Title));
            }
            else
            {
                return new Result(ResultStatus.Error, message:
                   $"{Banner.Title} adlı Banner silinə bilmədi, təkrar yoxlayın");
            }
        }
    }
}
