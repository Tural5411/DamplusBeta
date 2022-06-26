using AutoMapper;
using Damplus.Entities.Concrete;
using Damplus.Mvc.Areas.Admin.Models;
using Damplus.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Damplus.Mvc.Models;
using Damplus.Mvc.Areas.Admin.Helpers.Abstract;
using Damplus.Entities.ComplexTypes;

namespace Damplus.Mvc.AutoMapper.Profiles
{
    public class ViewModelsProfile:Profile
    {
        public ViewModelsProfile(/*IImageHelper imageHelper*/)  
        {
            CreateMap<TeamAddViewModel, TeamAddDto>();
            CreateMap<TeamAddDto, TeamAddViewModel>();


            CreateMap<PhotoAddDto, PhotoAddViewModel>().ReverseMap();

            CreateMap<TeamUpdateViewModel, TeamUpdateDto>();
            CreateMap<TeamUpdateDto, TeamUpdateViewModel>();

            CreateMap<ProjectAddViewModel, ProjectAddDto>();
            CreateMap<ProjectUpdateViewModel, ProjectUpdateDto>().ReverseMap();
            CreateMap<BusinessAddViewModel, BusinessAddDto>();
            CreateMap<BusinessUpdateDto, BusinessUpdateViewModel>();
            CreateMap<BusinessUpdateViewModel, BusinessUpdateDto>();
            CreateMap<ArticleAddViewModel, ArticleAddDto>();
               // ForMember(dest => dest.Thumbnail, opt => opt
               //.MapFrom(x => imageHelper.UploadImage(x.Title, x.ThumbnailFile, PictureType.Post, null)));
            CreateMap<ArticleUpdateViewModel, ArticleUpdateDto>();
            CreateMap<ArticleUpdateDto,ArticleUpdateViewModel>();

            CreateMap<VideoAddViewModel, VideoAddDto>();
            CreateMap<VideoAddDto, VideoAddViewModel>();

            CreateMap<VideoUpdateViewModel, VideoUpdateDto>();
            CreateMap<VideoUpdateDto, VideoUpdateViewModel>();


            CreateMap<ArticleRightSideBarWidgetOptions, ArticleRightSideBarWidgetOptionsViewModel>();
        }
    }
}
