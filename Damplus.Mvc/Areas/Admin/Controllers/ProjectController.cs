using AutoMapper;
using Damplus.Entities.ComplexTypes;
using Damplus.Entities.Concrete;
using Damplus.Entities.DTOs;
using Damplus.Mvc.Areas.Admin.Helpers.Abstract;
using Damplus.Mvc.Areas.Admin.Models;
using Damplus.Services.Abstract;
using Damplus.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Damplus.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectController : BaseController
    {
        private readonly IProjectService _projectService;
        private readonly IProjectCategoryService _projectCategoryService;
        private readonly IToastNotification _toastNotification;
        private readonly IPhotoService _photoService;

        public ProjectController(IPhotoService photoService, IProjectService projectService, IProjectCategoryService projectCategoryService, IToastNotification toastNotification, UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper) : base(userManager, mapper, imageHelper)
        {
            _projectService = projectService;
            _projectCategoryService = projectCategoryService;
            _toastNotification = toastNotification;
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _projectService.GetAllByNonDeleteAndActive();
            return View(result.Data);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var result = await _projectCategoryService.GetAllByNonDeleteAndActive();
            if (result.ResultStatus == ResultStatus.Succes)
            {
                return View(new ProjectAddViewModel
                {
                    ProjectCategories = result.Data.ProjectCategories
                });
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Add(ProjectAddViewModel projectAddViewModel)
        {
            if (ModelState.IsValid)
            {
                var projectAddDto = Mapper.Map<ProjectAddDto>(projectAddViewModel);

                var imageResult = await ImageHelper.UploadImage(projectAddDto.Name, projectAddViewModel.Photo, PictureType.Project);
                projectAddDto.Photo = imageResult.Data.FullName;

                var result = await _projectService.Add(projectAddDto, "Damplus");

                if (projectAddViewModel.ProjectPhotos != null)
                {
                    projectAddViewModel.Photos = new List<PhotoAddViewModel>();
                    foreach (var file in projectAddViewModel.ProjectPhotos)
                    {
                        var galleryResult = await ImageHelper.UploadImageV2(file);
                        var gallery = new PhotoAddDto()
                        {
                            ProjectId = result.Data.Project.Id,
                            URL = galleryResult
                        };
                        await _photoService.Add(gallery, "Damplus");
                    }
                }
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
            var projectcategories = await _projectCategoryService.GetAllByNonDeleteAndActive();
            projectAddViewModel.ProjectCategories = projectcategories.Data.ProjectCategories;
            return View(projectAddViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int projectId)
        {
            var projectResult = await _projectService.GetUpdateDto(projectId);
            var categoriesResult = await _projectCategoryService.GetAllByNonDeleteAndActive();
            var projectPhotosResult = await _photoService.GetAllByNonDeletedAndActive(projectId);
            if (projectResult.ResultStatus == ResultStatus.Succes && categoriesResult.ResultStatus == ResultStatus.Succes)
            {
                var projectUpdateViewModel = Mapper.Map<ProjectUpdateViewModel>(projectResult.Data);
                projectUpdateViewModel.ProjectCategories = categoriesResult.Data.ProjectCategories;
                projectUpdateViewModel.Images = projectPhotosResult.Data.Photos;
                return View(projectUpdateViewModel);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Update(ProjectUpdateViewModel projectUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                bool isNewThumbnailUploaded = false;
                var oldThumbnail = projectUpdateViewModel.Photo;
                if (projectUpdateViewModel.PhotoFile != null)
                {
                    var uploadedImageResult = await ImageHelper.UploadImage(projectUpdateViewModel.Name,
                        projectUpdateViewModel.PhotoFile, PictureType.Post);
                    projectUpdateViewModel.Photo = uploadedImageResult.ResultStatus
                        == ResultStatus.Succes ? uploadedImageResult.Data.FullName
                        : "postImages/defaultThumbnail.jpg";
                    if (oldThumbnail != "postImages/defaultThumbnail.jpg")
                    {
                        isNewThumbnailUploaded = true;
                    }
                }
                var articleUpdateDto = Mapper.Map<ProjectUpdateDto>(projectUpdateViewModel);
                var result = await _projectService.Update(articleUpdateDto, LoggedInUser.UserName);
                //Multi Image
                if (projectUpdateViewModel.ProjectPhotos != null)
                {
                    projectUpdateViewModel.Photos = new List<PhotoAddViewModel>();
                    foreach (var file in projectUpdateViewModel.ProjectPhotos)
                    {
                        var galleryResult = await ImageHelper.UploadImageV2(file);
                        var gallery = new PhotoAddDto()
                        {
                            ProjectId = result.Data.Project.Id,
                            URL = galleryResult
                        };
                        await _photoService.Add(gallery, "Damplus");
                    }
                }


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
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }
            }
            var categories = await _projectCategoryService.GetAllByNonDeleteAndActive();
            var projectPhotosResult = await _photoService.GetAllByNonDeletedAndActive(projectUpdateViewModel.Id);
            projectUpdateViewModel.ProjectCategories = categories.Data.ProjectCategories;

            projectUpdateViewModel.Images = projectPhotosResult.Data.Photos;
            return View(projectUpdateViewModel);
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int projectId)
        {
            var result = await _projectService.HardDelete(projectId);
            var deletedTeam = JsonSerializer.Serialize(result);
            return Json(deletedTeam);
        }
        [HttpPost]
        public async Task<JsonResult> DeletePhoto(int photoId)
        {
            var result = await _photoService.HardDelete(photoId);
            var deletedTeam = JsonSerializer.Serialize(result);
            return Json(deletedTeam);
        }

    }
}
