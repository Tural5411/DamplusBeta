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
    public class VideoManager : IVideoService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public VideoManager(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IDataResult<VideoListDto>> GetAll()
        {
            var videos = await _unitOfWork.Videos.GetAllAsync(null);
            if (videos.Count > -1)
            {
                return new DataResult<VideoListDto>(ResultStatus.Succes, new VideoListDto
                {
                    Videos = videos,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<VideoListDto>(ResultStatus.Error, Messages.Video.NotFound(isPlural: true),
                new VideoListDto
                {
                    Message = Messages.Video.NotFound(isPlural: true),
                    Videos = null,
                    ResultStatus = ResultStatus.Error
                });
        }
        public async Task<IDataResult<VideoDto>> Get(int videoId)
        {
            var Video = await _unitOfWork.Videos.GetAsync(c => c.Id == videoId);
            if (Video != null)
            {
                return new DataResult<VideoDto>(ResultStatus.Succes, new VideoDto
                {
                    Video = Video,
                    ResultStatus = ResultStatus.Succes
                });
            }
            else
            {
                return new DataResult<VideoDto>(ResultStatus.Error, Messages.Video.NotFound(isPlural: false), new VideoDto
                {
                    Video = null,
                    ResultStatus = ResultStatus.Error,
                    Message = Messages.Video.NotFound(isPlural: false)
                });
            }
        }
        public async Task<IDataResult<VideoDto>> Add(VideoAddDto videoAddDto, string createdByName)
        {
            
            var video = _mapper.Map<Video>(videoAddDto);
            video.CreatedByName = createdByName;
            video.ModifiedByName = createdByName;
            var addedVideo = await _unitOfWork.Videos.AddAsync(video);
            await _unitOfWork.SaveAsync();
            return new DataResult<VideoDto>(ResultStatus.Succes, Messages.Video.Add(addedVideo.Title), new VideoDto
            {
                Video = addedVideo,
                ResultStatus = ResultStatus.Succes,
                Message =Messages.Video.Add(addedVideo.Title)
            });
        }
     
        public async Task<IDataResult<VideoUpdateDto>> GetVideoUpdateDto(int videoId)
        {
            var result = await _unitOfWork.Videos.AnyAsync(a => a.Id == videoId);
            if (result)
            {
                var video = await _unitOfWork.Videos.GetAsync(c => c.Id == videoId);
                var videoUpdateDto = _mapper.Map<VideoUpdateDto>(video);
                return new DataResult<VideoUpdateDto>(ResultStatus.Succes, videoUpdateDto);
            }
            else
            {
                return new DataResult<VideoUpdateDto>(ResultStatus.Error, Messages.Article.NotFound(isPlural: false), null);
            }
        }
        public async Task<IDataResult<VideoDto>> Update(VideoUpdateDto VideoUpdateDto, string modifiedByName)
        {
            var oldVideo = await _unitOfWork.Videos.GetAsync(c => c.Id == VideoUpdateDto.Id);
            var Video = _mapper.Map<VideoUpdateDto, Video>(VideoUpdateDto, oldVideo);
            Video.ModifiedByName = modifiedByName;
            if (Video != null)
            {
                var updatedVideo = await _unitOfWork.Videos.UpdateAsync(Video);
                await _unitOfWork.SaveAsync();
                return new DataResult<VideoDto>(ResultStatus.Succes, Messages.Video.Add(updatedVideo.Title), new VideoDto
                {
                    Video = updatedVideo,
                    Message = Messages.Video.Add(updatedVideo.Title),
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<VideoDto>(ResultStatus.Error, message: "Xəta baş verdi", new VideoDto
            {
                Video = null,
                Message = "Xəta baş verdi",
                ResultStatus = ResultStatus.Error
            });
        }
        public async Task<IResult> HardDelete(int VideoId)
        {
            var Video = await _unitOfWork.Videos.GetAsync(c => c.Id == VideoId);
            if (Video != null)
            {
                await _unitOfWork.Videos.DeleteAsync(Video);
                await _unitOfWork.SaveAsync();

                return new Result(ResultStatus.Succes, Messages.Video.HardDelete(Video.Title));
            }
            else
            {
                return new Result(ResultStatus.Error, message:
                   $"{Video.Title} adlı video silinə bilmədi, təkrar yoxlayın");
            }
        }
        public async Task<IDataResult<VideoListDto>> GetAllByPage(int pageSize = 4, int currentPage = 1, bool isAscending = false)
        {
            pageSize = pageSize > 20 ? 20 : pageSize;
            var videos = await _unitOfWork.Videos.GetAllAsync(a => a.IsActive && !a.IsDeleted);
            var sortedVideos = isAscending ? videos.OrderBy(a => a.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
                : videos.OrderByDescending(a => a.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return new DataResult<VideoListDto>(ResultStatus.Succes, new VideoListDto
            {
                Videos = sortedVideos,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = videos.Count,
                ResultStatus = ResultStatus.Succes,
                IsAscending = false
            });
        }
    }
}
