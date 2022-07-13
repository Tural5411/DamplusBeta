using Damplus.Entities.Concrete;
using Damplus.Mvc.Models;
using Damplus.Services.Abstract;
using Damplus.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Damplus.Mvc.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly ArticleRightSideBarWidgetOptions _articleRightSideBarWidgetOptions;

        public ArticleController(IArticleService articleService,ICategoryService categoryService,IOptionsSnapshot<ArticleRightSideBarWidgetOptions> articleRightSideBarWidgetOptions)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _articleRightSideBarWidgetOptions = articleRightSideBarWidgetOptions.Value;
        }
        [Route("Bloq")]
        [HttpGet]
        public async Task<IActionResult> Index(int? categoryId,int currentPage=1,int pageSize=6,bool isAscending=false)
        {
            var articleResult = await (categoryId == null
                ? _articleService.GetAllByPaging(null, currentPage, pageSize, isAscending)
                : _articleService.GetAllByPaging(categoryId, currentPage, pageSize, isAscending));
            return View(articleResult.Data);
        }
        [HttpGet]
        public async Task<IActionResult> Search(string keyword, int currentPage = 1, int pageSize = 5, bool isAscending = false)
        {
            var searchResult = await _articleService.SearchAsync(keyword, currentPage, pageSize, isAscending);
            if (searchResult.ResultStatus == ResultStatus.Succes)
            {
                return View(new ArticleSearchViewModel
                {
                    ArticleListDto = searchResult.Data,
                    Keyword = keyword
                });
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int articleId)
        {
            var articleResult = await _articleService.Get(articleId);
            var categories = await _categoryService.GetAllByNonDeleteAndActive();
            if (articleResult.ResultStatus == ResultStatus.Succes)
            {
                var userArticles = await _articleService.GetAllUserIdOnFilter(articleResult.Data.Article.UserId,
                    _articleRightSideBarWidgetOptions.FilterBy, _articleRightSideBarWidgetOptions.OrderBy,
                    _articleRightSideBarWidgetOptions.IsAscending, _articleRightSideBarWidgetOptions.TakeSize,
                    _articleRightSideBarWidgetOptions.CategoryId, _articleRightSideBarWidgetOptions.StartAt,
                    _articleRightSideBarWidgetOptions.EndAt, _articleRightSideBarWidgetOptions.MinViewCount,
                    _articleRightSideBarWidgetOptions.MaxViewCount, _articleRightSideBarWidgetOptions.MinCommentCount,
                    _articleRightSideBarWidgetOptions.MaxCommentCount);
                await _articleService.IncreaseViewCount(articleId);
                List<String> listStrLineElements;
                listStrLineElements = articleResult.Data.Article.SeoTags.Split(',').ToList();
                ViewBag.listTags = listStrLineElements;
                return View(new ArticleDetailViewModel
                {
                    ArticleDto = articleResult.Data,
                    ArticleDetailRightSideBarViewModel = new ArticleDetailRightSideBarViewModel
                    {
                        ArticleListDto = userArticles.Data,
                        Header = _articleRightSideBarWidgetOptions.Header,
                        User = articleResult.Data.Article.User,
                        Categories= categories.Data
                    }
                });
            }
            return NotFound();
        }
    }
}
