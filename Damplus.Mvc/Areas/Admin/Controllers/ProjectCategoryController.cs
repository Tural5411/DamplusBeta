using AutoMapper;
using Damplus.Entities.Concrete;
using Damplus.Entities.DTOs;
using Damplus.Mvc.Areas.Admin.Helpers.Abstract;
using Damplus.Mvc.Areas.Admin.Models;
using Damplus.Services.Abstract;
using Damplus.Shared.Utilities.Extensions;
using Damplus.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Damplus.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectCategoryController : BaseController
    {
        private readonly IProjectCategoryService _ProjectCategoryService;

        public ProjectCategoryController(IProjectCategoryService ProjectCategoryService, UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper) : base(userManager, mapper, imageHelper)
        {
            _ProjectCategoryService = ProjectCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _ProjectCategoryService.GetAllByNonDeleteAndActive();
            return View(result.Data);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_ProjectCategoryAddPartial");
        }
        [HttpPost]
        public async Task<IActionResult> Add(ProjectCategoryAddDto ProjectCategoryAddDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _ProjectCategoryService.Add(ProjectCategoryAddDto, LoggedInUser.UserName);
                if (result.ResultStatus == ResultStatus.Succes)
                {
                    var ProjectCategoryAddAjaxModel = JsonSerializer.Serialize(new ProjectCategoryAjaxViewModel
                    {
                        ProjectCategoryDto = result.Data,
                        ProjectCategoryPartial = await this.RenderViewToStringAsync("_ProjectCategoryAddPartial", ProjectCategoryAddDto)
                    });
                    return Json(ProjectCategoryAddAjaxModel);
                }
            }
            var projectAddErrorAjaxModel = JsonSerializer.Serialize(new ProjectCategoryAjaxViewModel
            {
                ProjectCategoryPartial = await this.RenderViewToStringAsync("_ProjectCategoryAddPartial", ProjectCategoryAddDto)
            });
            return Json(projectAddErrorAjaxModel);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int ProjectCategoryId)
        {
            var result = await _ProjectCategoryService.GetUpdateDto(ProjectCategoryId);
            if (result.ResultStatus == ResultStatus.Succes)
            {
                return PartialView("_ProjectCategoryUpdatePartial", result.Data);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Update(ProjectCategoryUpdateDto ProjectCategoryUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _ProjectCategoryService.Update(ProjectCategoryUpdateDto, LoggedInUser.UserName);
                if (result.ResultStatus == ResultStatus.Succes)
                {
                    var ProjectCategoryUpdateAjaxModel = JsonSerializer.Serialize(new ProjectCategoryUpdateAjaxViewModel
                    {
                        ProjectCategoryDto = result.Data,
                        ProjectCategoryUpdatePartial = await this.RenderViewToStringAsync("_ProjectCategoryUpdatePartial", ProjectCategoryUpdateDto)
                    });
                    return Json(ProjectCategoryUpdateAjaxModel);
                }
            }
            var ProjectCategoryUpdateErrorAjaxModel = JsonSerializer.Serialize(new ProjectCategoryUpdateAjaxViewModel
            {
                ProjectCategoryUpdatePartial = await this.RenderViewToStringAsync("_ProjectCategoryUpdatePartial", ProjectCategoryUpdateDto)
            });
            return Json(ProjectCategoryUpdateErrorAjaxModel);
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int projectCategoryId)
        {
            var result = await _ProjectCategoryService.Delete(projectCategoryId, LoggedInUser.UserName);
            var deletedProjectCategory = JsonSerializer.Serialize(result.Data);
            return Json(deletedProjectCategory);
        }
    }
}
