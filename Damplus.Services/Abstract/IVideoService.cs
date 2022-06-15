using Damplus.Entities.DTOs;
using Damplus.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Services.Abstract
{
    public interface IVideoService
    {
        Task<IDataResult<VideoDto>> Get(int videoId);
        Task<IDataResult<VideoListDto>> GetAll();
        Task<IDataResult<VideoDto>> Add(VideoAddDto VideoAddDto, string createdByName);
        Task<IDataResult<VideoListDto>> GetAllByPage(int pageSize = 4, int currentPage = 1, bool isAscending = false);
        Task<IDataResult<VideoUpdateDto>> GetVideoUpdateDto(int videoId);
        Task<IDataResult<VideoDto>> Update(VideoUpdateDto VideoUpdateDto, string modifiedByName);
        Task<IResult> HardDelete(int VideoId);
    }
}
