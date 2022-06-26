using AutoMapper;
using Damplus.Entities.Concrete;
using Damplus.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Services.AutoMapper.Profiles
{
    public class PhotoProfile : Profile
    {
        public PhotoProfile()
        {
            CreateMap<PhotoAddDto, Photo>().ForMember(dest=>dest.CreatedDate,opt=>opt.MapFrom(x=>DateTime.Now)).ReverseMap();
        }
    }
}
