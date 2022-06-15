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
    public class ProjectCategoryProfile:Profile
    {
        public ProjectCategoryProfile()
        {
            CreateMap<ProjectCategory, ProjectCategoryAddDto>();
            CreateMap<ProjectCategoryAddDto, ProjectCategory>();

            CreateMap<ProjectCategory, ProjectCategoryUpdateDto>();
            CreateMap<ProjectCategoryUpdateDto, ProjectCategory>();
        }
    }
}
