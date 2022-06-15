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
    public class ProjectProfile:Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectAddDto>();
            CreateMap<ProjectAddDto, Project>();

            CreateMap<Project, ProjectUpdateDto>();
            CreateMap<ProjectUpdateDto, Project>();
        }
    }
}
