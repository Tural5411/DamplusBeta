using Microsoft.AspNetCore.Http;
using Damplus.Shared.Utilities.Results.Abstract;
using Damplus.Entities.ComplexTypes;
using Damplus.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Damplus.Mvc.Areas.Admin.Helpers.Abstract
{
    public interface IImageHelper
    {
        //string UploadImage(string name, 
        //    IFormFile pictureFile,PictureType pictureType, string folderName=null);
        Task<IDataResult<ImageUploadedDto>> UploadImage(string name,
            IFormFile pictureFile, PictureType pictureType, string folderName = null);
        IDataResult<ImageDeletedDto> ImageDelete(string PictureName);
    }
}
