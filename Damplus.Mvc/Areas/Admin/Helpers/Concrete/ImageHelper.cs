using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Damplus.Shared.Utilities.Results.Abstract;
using Damplus.Shared.Utilities.Results.ComplexTypes;
using Damplus.Shared.Utilities.Results.Concrete;
using Damplus.Entities.ComplexTypes;
using Damplus.Entities.DTOs;
using Damplus.Mvc.Areas.Admin.Helpers.Abstract;
using System;
using System.IO;
using System.Threading.Tasks;
using ImageProcessor;
using ImageProcessor.Plugins.WebP.Imaging.Formats;

namespace Damplus.Mvc.Areas.Admin.Helpers.Concrete
{
    public class ImageHelper : IImageHelper
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _wwwroot;
        private const string ImgFolder = "img";

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
        public IDataResult<ImageDeletedDto> ImageDelete(string pictureName)
        {

            var fileToDelete = Path.Combine($"{_wwwroot}/{ImgFolder}", pictureName);
            if (Directory.Exists(fileToDelete))
            {
                var fileInfo = new FileInfo(fileToDelete);
                var imageDeletedDto = new ImageDeletedDto
                {
                    FullName = pictureName,
                    Extension = fileInfo.Extension,
                    Path = fileInfo.FullName,
                    Size = fileInfo.Length
                };
                Directory.Delete(fileToDelete);
                return new DataResult<ImageDeletedDto>(ResultStatus.Succes, imageDeletedDto);
            }
            return new DataResult<ImageDeletedDto>(ResultStatus.Error, "Şəkil Tapılmadı", null);
        }
        

        public Task<IDataResult<ImageUploadedDto>> UploadImage(string name, IFormFile pictureFile, PictureType pictureType, string folderName = null)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            //Save image to wwwroot/image
            var wwwRootPath = _env.WebRootPath;
            var fileName = Path.GetFileNameWithoutExtension(pictureFile.FileName);
            var extension = Path.GetExtension(pictureFile.FileName);
            name = fileName = fileName + DateTime.Now.ToString("yymmssfff") + ".webp";
            var path = Path.Combine(wwwRootPath + "/img/", fileName);

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
            var message = pictureType == PictureType.User ? $"{name} adlı istifadəçinin şəkli uğurla yükləndi."
                    : $"{name} adlı məqalənin şəkli uğurla yükləndi.";

            return Task.FromResult<IDataResult<ImageUploadedDto>>(new DataResult<ImageUploadedDto>(ResultStatus.Succes, message, new ImageUploadedDto
            {
                FullName =name,
                OldName = name,
                Extension = extension,
                FolderName = path,
                Path = path,
                Size = pictureFile.Length
            }));

        }
    }
}
