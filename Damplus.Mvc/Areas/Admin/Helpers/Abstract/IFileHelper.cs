using Microsoft.AspNetCore.Http;
using Damplus.Entities.DTOs;
using Damplus.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Damplus.Mvc.Areas.Admin.Helpers.Abstract
{
    public interface IFileHelper
    {
        Task<IDataResult<FileUploadDto>> UploadFile(string name,
            IFormFile pdfFile, string folderName = null);
        IDataResult<FileDeletedDto> FileDelete(string fileName);
    }
}
