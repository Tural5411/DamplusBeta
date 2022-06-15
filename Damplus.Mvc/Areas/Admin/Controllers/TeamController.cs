using AutoMapper;
using Damplus.Entities.ComplexTypes;
using Damplus.Entities.Concrete;
using Damplus.Entities.DTOs;
using Damplus.Mvc.Areas.Admin.Helpers.Abstract;
using Damplus.Mvc.Areas.Admin.Models;
using Damplus.Services.Abstract;
using Damplus.Shared.Utilities.Extensions;
using Damplus.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Damplus.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : BaseController
    {
        private readonly ITeamService _teamService;
        private readonly IToastNotification _toastNotification;

        public TeamController(ITeamService teamService, IToastNotification toastNotification, UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper) : base(userManager, mapper, imageHelper)
        {
            _teamService = teamService;
            _toastNotification = toastNotification;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _teamService.GetAllByNonDeleteAndActive();
            return View(result.Data);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View(/*"_TeamAddPartial"*/);
        }
        [HttpPost]
        public async Task<IActionResult> Add(TeamAddViewModel teamAddViewModel)
        {
            if (ModelState.IsValid)
            {
                var teamAddDto = Mapper.Map<TeamAddDto>(teamAddViewModel);
                var imageResult = await ImageHelper.UploadImage(teamAddViewModel.Fullname,
                    teamAddViewModel.PictureFile, PictureType.Post);
                teamAddDto.Photo = imageResult.Data.FullName;
                var result = await _teamService.Add(teamAddDto, LoggedInUser.UserName);
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
            return View(teamAddViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int teamId)
        {
            var result = await _teamService.GetUpdateDto(teamId);
            if (result.ResultStatus == ResultStatus.Succes)
            {
                var teamUpdateViewModel= Mapper.Map<TeamUpdateViewModel>(result.Data);
                return View(teamUpdateViewModel);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Update(TeamUpdateViewModel teamUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                bool isNewThumbnailUploaded = false;
                var oldThumbnail = teamUpdateViewModel.Photo;
                if (teamUpdateViewModel.PictureFile != null)
                {
                    var uploadedImageResult = await ImageHelper.UploadImage(teamUpdateViewModel.Fullname,
                        teamUpdateViewModel.PictureFile, PictureType.Post);
                    teamUpdateViewModel.Photo = uploadedImageResult.ResultStatus
                        == ResultStatus.Succes ? uploadedImageResult.Data.FullName
                        : "postImages/defaultThumbnail.jpg";
                    if (oldThumbnail != "postImages/defaultThumbnail.jpg")
                    {
                        isNewThumbnailUploaded = true;
                    }
                }
                var teamUpdateDto = Mapper.Map<TeamUpdateDto>(teamUpdateViewModel);
                var result = await _teamService.Update(teamUpdateDto, LoggedInUser.UserName);
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
            return View(teamUpdateViewModel);
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int teamId)
        {
            var result = await _teamService.Delete(teamId, LoggedInUser.UserName);
            var deletedTeam = JsonSerializer.Serialize(result.Data);
            return Json(deletedTeam);
        }
    }
}
