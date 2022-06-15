using AutoMapper;
using Damplus.Entities.ComplexTypes;
using Damplus.Entities.Concrete;
using Damplus.Entities.DTOs;
using Damplus.Mvc.Areas.Admin.Helpers.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Damplus.Mvc.AutoMapper.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile(/*IImageHelper imageHelper*/)
        {
            CreateMap<UserAddDto, User>();
            //    .ForMember(dest=>dest.Picture,opt=>opt
            //.MapFrom(x=>imageHelper.UploadImage(x.UserName,x.PictureFile,PictureType.User,null)));  
            CreateMap<User,UserAddDto>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<User, UserUpdateDto>();
        }
    }
}
