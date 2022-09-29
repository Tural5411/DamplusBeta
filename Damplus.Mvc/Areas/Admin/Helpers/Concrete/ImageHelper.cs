using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Damplus.Shared.Utilities.Extensions;
using Damplus.Shared.Utilities.Results.Abstract;
using Damplus.Shared.Utilities.Results.ComplexTypes;
using Damplus.Shared.Utilities.Results.Concrete;
using Damplus.Entities.ComplexTypes;
using Damplus.Entities.DTOs;
using Damplus.Mvc.Areas.Admin.Helpers.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ImageProcessor;
using ImageProcessor.Imaging.Formats;
using ImageProcessor.Plugins.WebP.Imaging.Formats;

namespace Damplus.Mvc.Areas.Admin.Helpers.Concrete
{
    public class ImageHelper : IImageHelper
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _wwwroot;
        private readonly string imgFolder = "img";
        private const string userImagesFolder = "userImages";
        private const string postImagesFolder = "postImages";
        public ImageHelper(IWebHostEnvironment env)
        {
            _env = env;
            _wwwroot = _env.WebRootPath;
        }


        public async Task<string> UploadImageV2(IFormFile file)
        {
            var wwwRootPath = _env.WebRootPath;
            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
            var extension = Path.GetExtension(file.FileName);
            var name = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            var path = Path.Combine(wwwRootPath + "/img/", fileName);
            await using var fileStream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return name;
        }
        public IDataResult<ImageDeletedDto> ImageDelete(string PictureName)
        {

            var fileToDelete = Path.Combine($"{_wwwroot}/{imgFolder}", PictureName);
            if (Directory.Exists(fileToDelete))
            {
                var fileInfo = new FileInfo(fileToDelete);
                var imageDeletedDto = new ImageDeletedDto
                {
                    FullName = PictureName,
                    Extension = fileInfo.Extension,
                    Path = fileInfo.FullName,
                    Size = fileInfo.Length
                };
                Directory.Delete(fileToDelete);
                return new DataResult<ImageDeletedDto>(ResultStatus.Succes, imageDeletedDto);
            }
            return new DataResult<ImageDeletedDto>(ResultStatus.Error, "Şəkil Tapılmadı", null);
        }
        

        public async Task<IDataResult<ImageUploadedDto>> UploadImage(string name, IFormFile pictureFile, PictureType pictureType, string folderName = null)
        {
            //Save image to wwwroot/image
            string wwwRootPath = _env.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(pictureFile.FileName);
            string extension = Path.GetExtension(pictureFile.FileName);
            name = fileName = fileName + DateTime.Now.ToString("yymmssfff") + ".webp";
            string path = Path.Combine(wwwRootPath + "/img/", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false))
                {
                    imageFactory.Load(pictureFile.OpenReadStream())
                        .Format(new WebPFormat())
                        .Quality(80)
                        .Save(fileStream);
                }
            }

            //Insert record
            string message = pictureType == PictureType.User ? $"{name} adlı istifadəçinin şəkli uğurla yükləndi."
                    : $"{name} adlı məqalənin şəkli uğurla yükləndi.";

            return new DataResult<ImageUploadedDto>(ResultStatus.Succes, message, new ImageUploadedDto
            {
                FullName =name,
                OldName = name,
                Extension = extension,
                FolderName = path,
                Path = path,
                Size = pictureFile.Length
            });

        }
    }
}
