using AutoMapper;
using Damplus.Entities.ComplexTypes;
using Damplus.Entities.Concrete;
using Damplus.Entities.DTOs;
using Damplus.Mvc.Areas.Admin.Helpers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Damplus.Mvc.AutoMapper.Profiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<TeamAddDto, Teams>();
               // ForMember(dest => dest.Photo, opt => opt
               //.MapFrom(x => imageHelper.UploadImage(x.Fullname, x.PictureFile, PictureType.User, null)));
            CreateMap<Teams, TeamAddDto>();

            CreateMap<Teams, TeamUpdateDto>();
            CreateMap<TeamUpdateDto, Teams>();
        }
    }
}
