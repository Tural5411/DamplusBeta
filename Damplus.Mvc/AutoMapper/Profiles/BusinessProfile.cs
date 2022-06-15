using AutoMapper;
using Damplus.Entities.Concrete;
using Damplus.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Mvc.AutoMapper.Profiles
{
    public class BusinessProfile:Profile
    {
        public BusinessProfile()
        {
            CreateMap<Business, BusinessAddDto>().ReverseMap();
            CreateMap<Business, BusinessUpdateDto>().ReverseMap();
        }
    }
}
