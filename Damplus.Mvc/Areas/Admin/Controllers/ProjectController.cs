﻿using AutoMapper;
using Damplus.Entities.ComplexTypes;
using Damplus.Entities.Concrete;
using Damplus.Entities.DTOs;
using Damplus.Mvc.Areas.Admin.Helpers.Abstract;
using Damplus.Mvc.Areas.Admin.Models;
using Damplus.Services.Abstract;
using Damplus.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Damplus.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectController : BaseController
    {
        private readonly IProjectService _projectService;
        private readonly IProjectCategoryService _projectCategoryService;
        private readonly IToastNotification _toastNotification;

        public ProjectController(IProjectService projectService, IProjectCategoryService projectCategoryService, IToastNotification toastNotification,UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper) : base(userManager, mapper, imageHelper)
        {
            _projectService = projectService;
            _projectCategoryService = projectCategoryService;
            _toastNotification = toastNotification;
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
        public async Task<IActionResult> Add(ProjectAddViewModel  projectAddViewModel)
        {
            if (ModelState.IsValid)
            {
                var projectAddDto = Mapper.Map<ProjectAddDto>(projectAddViewModel);
                var imageResult = await ImageHelper.UploadImage(projectAddDto.Name, projectAddViewModel.Photo, PictureType.Project);
                projectAddDto.Photo = imageResult.Data.FullName;
                var result = await _projectService.Add(projectAddDto, LoggedInUser.UserName);
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
    }
}