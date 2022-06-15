using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Damplus.Entities.DTOs;
using Damplus.Mvc.Areas.Admin.Helpers.Abstract;
using Damplus.Shared.Utilities.Extensions;
using Damplus.Shared.Utilities.Results.Abstract;
using Damplus.Shared.Utilities.Results.ComplexTypes;
using Damplus.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Damplus.Mvc.Areas.Admin.Helpers.Concrete
{
    public class FileHelper : IFileHelper
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _wwwroot;
        private readonly string imgFolder = "files";
        private const string postImagesFolder = "examPdf";
        public FileHelper(IWebHostEnvironment env)
        {
            _env = env;
            _wwwroot = _env.WebRootPath;
        }


        public async Task<IDataResult<FileUploadDto>> UploadFile(string name, IFormFile pdfFile, string folderName = null)
        {
            folderName = postImagesFolder;
            if (!Directory.Exists($"{_wwwroot}/{imgFolder}/{folderName}"))
            {
                Directory.CreateDirectory($"{_wwwroot}/{imgFolder}/{folderName}");
            }
            string oldFileName = Path.GetFileNameWithoutExtension(pdfFile.FileName);
            string fileExtension = Path.GetExtension(pdfFile.FileName);

            Regex regex = new Regex("[*/:'\", .|_&#^@]");
            name = regex.Replace(name, string.Empty);

            DateTime dateTime = DateTime.Now;
            //TuralAbdulxaliqov/542_5_10_12_2020.png
            string newFileName = $"{name}_{dateTime.FullDateAndStringWithUnderScore()}_{fileExtension}";
            string path = Path.Combine($"{_wwwroot}/{imgFolder}/{folderName}", newFileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await pdfFile.CopyToAsync(stream);
            }
            string message = $"Pdf uğurla yükləndi.";
            return new DataResult<FileUploadDto>(ResultStatus.Succes, message, new FileUploadDto
            {
                FullName = $"{folderName}/{newFileName}",
                OldName = oldFileName,
                Extension = fileExtension,
                FolderName = folderName,
                Path = path,
                Size = pdfFile.Length
            });
        }

        public IDataResult<FileDeletedDto> FileDelete(string fileName)
        {
            var fileToDelete = Path.Combine($"{_wwwroot}/{imgFolder}", fileName);
            if (System.IO.File.Exists(fileToDelete))
            {
                var fileInfo = new FileInfo(fileToDelete);
                var fileDeletedDto = new FileDeletedDto
                {
                    FullName = fileName,
                    Extension = fileInfo.Extension,
                    Path = fileInfo.FullName,
                    Size = fileInfo.Length
                };
                System.IO.File.Delete(fileToDelete);
                return new DataResult<FileDeletedDto>(ResultStatus.Succes, fileDeletedDto);
            }
            return new DataResult<FileDeletedDto>(ResultStatus.Error, "Pdf Tapılmadı", null);
        }
    }
}
