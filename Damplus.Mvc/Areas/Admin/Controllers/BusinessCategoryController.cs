using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using Damplus.Entities.ComplexTypes;
using Damplus.Entities.Concrete;
using Damplus.Entities.DTOs;
using Damplus.Mvc.Areas.Admin.Helpers.Abstract;
using Damplus.Mvc.Areas.Admin.Models;
using Damplus.Services.Abstract;
using Damplus.Shared.Utilities.Extensions;
using Damplus.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Damplus.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BusinessCategoryController : BaseController
    {
        private readonly IBusinessCategoryService _businessCategoryService;
        private readonly IToastNotification _toastNotification;

        public BusinessCategoryController(IBusinessCategoryService businessCategoryService, IToastNotification toastNotification, UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper) : base(userManager, mapper, imageHelper)
        {
            _businessCategoryService = businessCategoryService;
            _toastNotification = toastNotification;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _businessCategoryService.GetAllByNonDeleteAndActive();
            return View(data.Data);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View(/*"_TeamAddPartial"*/);
        }
        [HttpPost]
        public async Task<IActionResult> Add(BusinessCategoryViewModel BusinessCategoryAddViewModel)
        {
            if (ModelState.IsValid)
            {
                var BusinessCategoryAddDto = Mapper.Map<BusinessCategoryAddDto>(BusinessCategoryAddViewModel);
                BusinessCategoryAddDto.UpperCategoryId = 0;
                var imageResult = await ImageHelper.UploadImage(BusinessCategoryAddViewModel.Name,
                    BusinessCategoryAddViewModel.PictureFile, PictureType.Post);
                BusinessCategoryAddDto.Icon = imageResult.Data.FullName;
                var result = await _businessCategoryService.Add(BusinessCategoryAddDto, "Damplus");
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
            return View(BusinessCategoryAddViewModel);
        }
        [HttpGet]
        public IActionResult AddParentCategory()
        {
            var category = _businessCategoryService.GetAll();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in category.Result.Data.BusinessCategories)
            {
                items.Add(new SelectListItem
                {
                    Selected = false,
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }
            ViewBag.category = items;
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> AddParentCategory(BusinessCategoryViewModel businessCategoryAddViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var businessCategoryAddDto = Mapper.Map<BusinessCategoryAddDto>(businessCategoryAddViewModel);
        //        businessCategoryAddDto.UpperCategoryId = businessCategoryAddViewModel.Id    ;
        //        var imageResult = await ImageHelper.UploadImage(businessCategoryAddViewModel.Name,
        //            businessCategoryAddViewModel.PictureFile, PictureType.Post);
        //        businessCategoryAddDto.Icon = imageResult.Data.FullName;
        //        var result = await _businessCategoryService.Add(businessCategoryAddDto, "Damplus");
        //        if (result.ResultStatus == ResultStatus.Succes)
        //        {
        //            _toastNotification.AddSuccessToastMessage(result.Message, new ToastrOptions
        //            {
        //                Title = "Uğurlu əməliyyat"
        //            });
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", result.Message);
        //        }
        //    }
        //    return View(businessCategoryAddViewModel);
        //}
        //[HttpGet]
        //public async Task<IActionResult> Update(int BusinessCategoryId)
        //{
        //    var result = await _businessCategoryService.GetBusinessCategoryUpdateDto(BusinessCategoryId);
        //    if (result.ResultStatus == ResultStatus.Succes)
        //    {
        //        var BusinessCategoryUpdateViewModel = Mapper.Map<BusinessCategoryUpdateViewModel>(result.Data);
        //        return View(BusinessCategoryUpdateViewModel);
        //    }
        //    return NotFound();
        //}
        //[HttpPost]
        //public async Task<IActionResult> Update(BusinessCategoryUpdateViewModel BusinessCategoryUpdateViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        bool isNewThumbnailUploaded = false;
        //        var oldThumbnail = BusinessCategoryUpdateViewModel.Thumbnail;
        //        if (BusinessCategoryUpdateViewModel.PictureFile != null)
        //        {
        //            var uploadedImageResult = await ImageHelper.UploadImage(BusinessCategoryUpdateViewModel.Title,
        //                BusinessCategoryUpdateViewModel.PictureFile, PictureType.Post);
        //            BusinessCategoryUpdateViewModel.Thumbnail = uploadedImageResult.ResultStatus
        //                == ResultStatus.Succes ? uploadedImageResult.Data.FullName
        //                : "postImages/defaultThumbnail.jpg";
        //            if (oldThumbnail != "postImages/defaultThumbnail.jpg")
        //            {
        //                isNewThumbnailUploaded = true;
        //            }
        //        }
        //        var BusinessCategoryUpdateDto = Mapper.Map<BusinessCategoryUpdateDto>(BusinessCategoryUpdateViewModel);
        //        var result = await _businessCategoryService.Update(BusinessCategoryUpdateDto, LoggedInUser.UserName);
        //        if (result.ResultStatus == ResultStatus.Succes)
        //        {
        //            if (isNewThumbnailUploaded)
        //            {
        //                ImageHelper.ImageDelete(oldThumbnail);
        //            }
        //            _toastNotification.AddSuccessToastMessage(result.Message, new ToastrOptions
        //            {
        //                Title = "Uğurlu əməliyyat",
        //                CloseButton = true
        //            });
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", result.Message);
        //        }
        //    }
        //    return View(BusinessCategoryUpdateViewModel);
        //}
        [HttpPost]
        public async Task<JsonResult> Delete(int BusinessCategoryId)
        {
            var result = await _businessCategoryService.HardDelete(BusinessCategoryId);
            var deletedBusinessCategory = JsonSerializer.Serialize(result);
            return Json(deletedBusinessCategory);
        }
    }
}
