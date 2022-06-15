using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using Damplus.Entities.ComplexTypes;
using Damplus.Entities.Concrete;
using Damplus.Entities.DTOs;
using Damplus.Mvc.Areas.Admin.Helpers.Abstract;
using Damplus.Mvc.Areas.Admin.Models;
using Damplus.Services.Abstract;
using Damplus.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Damplus.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PriceController : BaseController
    {
        private readonly IPriceService _priceService;
        private readonly IBusinessService _businessService;
        private readonly IToastNotification _toastNotification;
        public PriceController(IToastNotification toastNotification, IBusinessService businessService, IPriceService priceService, UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper) : base(userManager, mapper, imageHelper)
        {
            _priceService = priceService;
            _businessService = businessService;
            _toastNotification = toastNotification;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _priceService.GetAll();
            return View(result.Data);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var result = await _businessService.GetAllByNonDeleteAndActive();
            if (result.ResultStatus == ResultStatus.Succes)
            {
                return View(new PriceAddViewModel
                {
                    Business = result.Data.Businesses
                });
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Add(PriceAddViewModel priceAddViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var priceAddDto = Mapper.Map<PriceAddDto>(priceAddViewModel);
                    var result = await _priceService.Add(priceAddDto, "Damplus");
                    if (result.ResultStatus == ResultStatus.Succes)
                    {
                        _toastNotification.AddSuccessToastMessage(result.Message, new ToastrOptions
                        {
                            Title = "Uğurlu əməliyyat"
                        });
                        return RedirectToAction("Index", "Price");
                    }
                    else
                    {
                        ModelState.AddModelError("", result.Message);
                    }
                }
                catch (Exception ex)
                {

                    return View(ex.ToString());
                }

            }
            return View(priceAddViewModel);

        }
        [HttpGet]
        public async Task<IActionResult> Update(int priceId)
        {
            var result = await _priceService.GetUpdateDto(priceId);
            var businessResult = await _businessService.GetAllByNonDeleteAndActive();

            if (result.ResultStatus == ResultStatus.Succes)
            {
                var priceUpdateViewModel = Mapper.Map<PriceUpdateViewModel>(result.Data);
                priceUpdateViewModel.Business = businessResult.Data.Businesses;
                return View(priceUpdateViewModel);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Update(PriceUpdateViewModel priceUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                bool isNewThumbnailUploaded = false;
                var oldThumbnail = priceUpdateViewModel.Icon;
                if (priceUpdateViewModel.IconFile != null)
                {
                    var uploadedImageResult = await ImageHelper.UploadImage(priceUpdateViewModel.Header,
                        priceUpdateViewModel.IconFile, PictureType.Post);
                    priceUpdateViewModel.Icon = uploadedImageResult.ResultStatus
                        == ResultStatus.Succes ? uploadedImageResult.Data.FullName
                        : "postImages/defaultThumbnail.jpg";
                    if (oldThumbnail != "postImages/defaultThumbnail.jpg")
                    {
                        isNewThumbnailUploaded = true;
                    }
                }
                var priceUpdateDto = Mapper.Map<PriceUpdateDto>(priceUpdateViewModel);
                var result = await _priceService.Update(priceUpdateDto, LoggedInUser.UserName);
                if (result.ResultStatus == ResultStatus.Succes)
                {
                    if (isNewThumbnailUploaded)
                    {
                        ImageHelper.ImageDelete(oldThumbnail);
                    }
                    //_toastNotification.AddSuccessToastMessage(result.Message, new ToastrOptions
                    //{
                    //    Title = "Uğurlu əməliyyat",
                    //    CloseButton = true
                    //});
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }
            return View(priceUpdateViewModel);
        }
    }
}
