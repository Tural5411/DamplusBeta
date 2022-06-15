using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
using System.Text.Json;
using System.Threading.Tasks;

namespace Damplus.Mvc.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize]
    public class BannerController : BaseController
    {
        private readonly IBannerService _bannerService;
        private readonly IToastNotification _toastNotification;

        public BannerController(IBannerService bannerService, IToastNotification toastNotification, UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper) : base(userManager, mapper, imageHelper)
        {
            _bannerService = bannerService;
            _toastNotification = toastNotification;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _bannerService.GetAll();
            return View(result.Data);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(BannerViewModel bannerViewModel)
        {
            if (ModelState.IsValid)
            {
                var bannerAddDto = Mapper.Map<BannerAddDto>(bannerViewModel);
                bannerAddDto.IsMain = false;
                var result = await _bannerService.Add(bannerAddDto, "Damplus");
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
            return View(bannerViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int bannerId)
        {
                var result = await _bannerService.GetUpdateDto(bannerId);
                if (result.ResultStatus == ResultStatus.Succes)
                {
                    var bannerUpdateViewModel = Mapper.Map<BannerUpdateViewModel>(result.Data);
                    return View(bannerUpdateViewModel);
                }
                return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Update(BannerUpdateViewModel bannerUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                var bannerUpdateDto = Mapper.Map<BannerUpdateDto>(bannerUpdateViewModel);
                var result = await _bannerService.Update(bannerUpdateDto, LoggedInUser.UserName);
                if (result.ResultStatus == ResultStatus.Succes)
                {
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
            return View(bannerUpdateViewModel);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int bannerId)
        {
            var result = await _bannerService.HardDelete(bannerId);
            var deletedBanner = JsonSerializer.Serialize(result);
            return Json(deletedBanner);
        }
    }
}
