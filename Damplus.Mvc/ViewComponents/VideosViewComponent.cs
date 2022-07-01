using Microsoft.AspNetCore.Mvc;
using Damplus.Mvc.Models;
using Damplus.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Damplus.Mvc.ViewComponents
{
    public class VideosViewComponent : ViewComponent
    {
        private readonly IVideoService _videoService;
        public VideosViewComponent(IVideoService videoService)
        {
            _videoService = videoService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var videosResult = await _videoService.GetAllByPage(3,1,true);
            return View(new VideoViewModel
            {
                VideoListDto = videosResult.Data
            });
        }
    }
}
