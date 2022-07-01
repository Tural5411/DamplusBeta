using Damplus.Mvc.Models;
using Damplus.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Damplus.Mvc.ViewComponents
{
    public class RightSidebarViewComponent:ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;
        public RightSidebarViewComponent(ICategoryService categoryService, IArticleService articleService)
        {
            _categoryService = categoryService;
            _articleService = articleService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categoriesResult = await _categoryService.GetAllByNonDeleteAndActive();
            var articlesResult = await _articleService.GetAllByViewCount(takeSize: 3);
            return View(new RightSidebarViewModel
            {
                Articles = articlesResult.Data.Articles,
                Categories = categoriesResult.Data.Categories
            });
        }
    }
}
