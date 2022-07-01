using Damplus.Mvc.Models;
using Damplus.Services.Abstract;
using Damplus.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Damplus.Mvc.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IProjectCategoryService _projectCategoryService;

        public ProjectController(IProjectService projectService, IProjectCategoryService projectCategoryService)
        {
            _projectService = projectService;
            _projectCategoryService = projectCategoryService;
        }
        [Route("Portfolio")]
        [HttpGet]
        public async Task<IActionResult> Index(int? categoryId, int currentPage = 1, int pageSize = 5, bool isAscending = false)
        {
            var projectResult = await (categoryId == null
                ? _projectService.GetAllByPaging(null, currentPage, pageSize, isAscending)
                : _projectService.GetAllByPaging(categoryId, currentPage, pageSize, isAscending));
            return View(new ProjectViewModel
            {
                ProjectListDto = projectResult.Data
            });
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int projectId)
        {
            var projectResult = await _projectService.Get(projectId);
            //var categories = await _projectCategoryService.GetAllByNonDeleteAndActive();
            if (projectResult.ResultStatus == ResultStatus.Succes)
            {
                return View(new ProjectDetailViewModel
                {
                    ProjectDto = projectResult.Data
                });
            }
            return NotFound();
        }
    }
}
