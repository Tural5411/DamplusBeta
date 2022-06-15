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
    public class BusinessCategoryProfile:Profile
    {
        public BusinessCategoryProfile()
        {
            CreateMap<BusinessCategoryAddDto, BusinessCategory>().ReverseMap();
            CreateMap<BusinessCategoryUpdateDto, BusinessCategory>();
            CreateMap<BusinessCategory, BusinessCategoryUpdateDto>();
        }
       
    }
}
