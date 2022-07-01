using Microsoft.AspNetCore.Mvc;
using Damplus.Mvc.Models;
using Damplus.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Damplus.Mvc.ViewComponents
{
    public class ProjectsViewComponent:ViewComponent
    {
        private readonly IProjectService _articleService;
        public ProjectsViewComponent(IProjectService articleService)
        {
            _articleService = articleService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var articlesResult = await _articleService.GetAllByPaging(null,1,3,false);
            return View(new ProjectViewModel
            {
                ProjectListDto = articlesResult.Data
            });
        }
    }
}
