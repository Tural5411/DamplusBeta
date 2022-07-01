using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Damplus.Data.Abstract.UnitOfWorks;
using Damplus.Entities.ComplexTypes;
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
using System.Linq.Expressions;

namespace Damplus.Services.Concrete
{
    public class PhotoManager : IPhotoService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        public PhotoManager(IMapper mapper, IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<IResult> Add(PhotoAddDto PhotoAddDto, string createdByName)
        {
            var Photo = _mapper.Map<Photo>(PhotoAddDto);
            Photo.CreatedByName = createdByName;
            Photo.ModifiedByName = createdByName;
            Photo.IsActive = true;
            await _unitOfWork.Photos.AddAsync(Photo);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Succes, Messages.Article.Add(Photo.URL));
        }
        public async Task<IResult> HardDelete(int PhotoId)
        {
            var result = await _unitOfWork.Photos.AnyAsync(a => a.Id == PhotoId);
            if (result)
            {
                var Photo = await _unitOfWork.Photos.GetAsync(a => a.Id == PhotoId);

                await _unitOfWork.Photos.DeleteAsync(Photo);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Succes, Messages.Article.Add(Photo.URL));
            }
            return new Result(ResultStatus.Error, Messages.Article.NotFound(false));
        }
        public async Task<IDataResult<PhotoDto>> Get(int PhotoId)
        {
            var Photo = await _unitOfWork.Photos.GetAsync(a => a.Id == PhotoId, a => a.Project);
            if (Photo != null)
            {
                return new DataResult<PhotoDto>(ResultStatus.Succes, new PhotoDto
                {
                    Photo = Photo,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<PhotoDto>(ResultStatus.Error, new PhotoDto
            {
                Photo = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.Article.NotFound(false)
            });
        }
        public async Task<IDataResult<PhotoListDto>> GetAllByNonDeletedAndActive(int projectId)
        {
            var Photos = await _unitOfWork.Photos.GetAllAsync(a=>a.ProjectId==projectId,
                 a => a.Project);
            if (Photos.Count > -1)
            {
                return new DataResult<PhotoListDto>(ResultStatus.Succes, new PhotoListDto
                {
                    Photos = Photos,
                    ResultStatus = ResultStatus.Succes,
                });
            }
            return new DataResult<PhotoListDto>(ResultStatus.Error, Messages.Article.NotFound(false), null);
        }
    }
}
