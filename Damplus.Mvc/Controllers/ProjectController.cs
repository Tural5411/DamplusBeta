using Damplus.Mvc.Models;
using Damplus.Services.Abstract;
using Damplus.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Damplus.Mvc.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IPhotoService _photoService;
        private readonly IProjectCategoryService _projectCategoryService;

        public ProjectController(IPhotoService photoService,IProjectService projectService, IProjectCategoryService projectCategoryService)
        {
            _projectService = projectService;
            _projectCategoryService = projectCategoryService;
            _photoService = photoService;
        }
        [Route("Portfolio")]
        [HttpGet]
        public async Task<IActionResult> Index(int? categoryId, int currentPage = 1, int pageSize = 4, bool isAscending = false)
        {
            var categories = await _projectCategoryService.GetAllByNonDeleteAndActive();
            var projectResult = await (categoryId == null
                ? _projectService.GetAllByPaging(null, currentPage, pageSize, isAscending)
                : _projectService.GetAllByPaging(categoryId, currentPage, pageSize, isAscending));
            return View(new ProjectViewModel
            {
                ProjectListDto = projectResult.Data,
                ProjectCategoryListDto=categories.Data
            });
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int projectId)
        {
            var photos = await _photoService.GetAllByNonDeletedAndActive(projectId);
            var projectResult = await _projectService.Get(projectId);
            var similiarProjects = await _projectService.GetAllByNonDeleteAndActive();
            //var categories = await _projectCategoryService.GetAllByNonDeleteAndActive();
            if (projectResult.ResultStatus == ResultStatus.Succes)
            {
                return View(new ProjectDetailViewModel
                {
                    ProjectDto = projectResult.Data,
                    ProjectListDto = similiarProjects.Data,
                    PhotoListDto=photos.Data
                });
            }
            return NotFound();
        }
    }
}
