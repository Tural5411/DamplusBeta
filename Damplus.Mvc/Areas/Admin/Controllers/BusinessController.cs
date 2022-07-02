using AutoMapper;
using Damplus.Entities.ComplexTypes;
using Damplus.Entities.Concrete;
using Damplus.Entities.DTOs;
using Damplus.Mvc.Areas.Admin.Helpers.Abstract;
using Damplus.Mvc.Areas.Admin.Models;
using Damplus.Services.Abstract;
using Damplus.Shared.Utilities.Extensions;
using Damplus.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Damplus.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BusinessController : BaseController
    {
        private readonly IBusinessService _businessService;
        private readonly IToastNotification _toastNotification;
        private readonly IFileHelper _fileHelper;
        [Obsolete]
        private IHostingEnvironment _environment;


        public BusinessController(IFileHelper fileHelper, IHostingEnvironment environment,IBusinessService businessService, IToastNotification toastNotification, UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper) : base(userManager, mapper, imageHelper)
        {
            _businessService = businessService;
            _toastNotification = toastNotification;
            _fileHelper = fileHelper;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _businessService.GetAllByNonDeleteAndActive();
            return View(result.Data);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(BusinessAddViewModel businessAddViewModel)
        {
            if (ModelState.IsValid)
            {
                var businessAddDto = Mapper.Map<BusinessAddDto>(businessAddViewModel);

                var imageResult = await ImageHelper.UploadImage(businessAddViewModel.Title,
                    businessAddViewModel.PictureFile, PictureType.Post);
                
                businessAddDto.Thumbnail = imageResult.Data.FullName;

                var result = await _businessService.Add(businessAddDto, LoggedInUser.UserName);


                if (result.ResultStatus == ResultStatus.Succes)
                {
                    _toastNotification.AddSuccessToastMessage(result.Message, new ToastrOptions
                    {
                        Title = "Uğurlu əməliyyat"
                    });
                    return RedirectToAction("Index");
                }

                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }
            return View(businessAddViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int businessId)
        {
            var result = await _businessService.GetUpdateDto(businessId);
            if (result.ResultStatus == ResultStatus.Succes)
            {
                var businessUpdateViewModel = Mapper.Map<BusinessUpdateViewModel>(result.Data);
                return View(businessUpdateViewModel);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Update(BusinessUpdateViewModel businessUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                bool isNewThumbnailUploaded = false;
                var oldThumbnail = businessUpdateViewModel.Thumbnail;
                if (businessUpdateViewModel.PictureFile != null)
                {
                    var uploadedImageResult = await ImageHelper.UploadImage(businessUpdateViewModel.Title,
                        businessUpdateViewModel.PictureFile, PictureType.Post);
                    businessUpdateViewModel.Thumbnail = uploadedImageResult.ResultStatus
                        == ResultStatus.Succes ? uploadedImageResult.Data.FullName
                        : "postImages/defaultThumbnail.jpg";
                    if (oldThumbnail != "postImages/defaultThumbnail.jpg")
                    {
                        isNewThumbnailUploaded = true;
                    }
                }
                if (businessUpdateViewModel.PdfFile !=null)
                {
                    //Pdf Upload
                    var pdfResult = await _fileHelper.UploadFile(businessUpdateViewModel.Title,
                        businessUpdateViewModel.PdfFile);
                    businessUpdateViewModel.Link = pdfResult.Data.FullName;
                }
                var businessUpdateDto = Mapper.Map<BusinessUpdateDto>(businessUpdateViewModel);
                var result = await _businessService.Update(businessUpdateDto, "Damplus");
                
                if (result.ResultStatus == ResultStatus.Succes)
                {
                    if (isNewThumbnailUploaded)
                    {
                        ImageHelper.ImageDelete(oldThumbnail);
                    }
                    _toastNotification.AddSuccessToastMessage(result.Message, new ToastrOptions
                    {
                        Title = "Uğurlu əməliyyat",
                        CloseButton = true
                    });
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }
            return View(businessUpdateViewModel);
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int businessId)
        {
            var result = await _businessService.HardDelete(businessId);
           
            var deletedBusiness = JsonSerializer.Serialize(result);
            return Json(deletedBusiness);
        }
        //[Obsolete]
        //public IActionResult DownloadFile(string filePath)
        //{
        //    string path = Path.Combine(this._environment.WebRootPath, "files/") + filePath;
        //    byte[] bytes = System.IO.File.ReadAllBytes(path);
        //    return File(bytes, "application/octet-stream", filePath);
        //}
    }
}
