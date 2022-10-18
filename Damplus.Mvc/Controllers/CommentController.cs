using Microsoft.AspNetCore.Mvc;
using Damplus.Shared.Utilities.Extensions;
using Damplus.Shared.Utilities.Results.ComplexTypes;
using Damplus.Entities.DTOs;
using Damplus.Mvc.Models;
using Damplus.Services.Abstract;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Damplus.Mvc.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpPost]
        public async Task<JsonResult> Add(CommentAddDto commentAddDto)
        {
            if (ModelState.IsValid)
            {
                    var result = await _commentService.Add(commentAddDto);
                    if (result.ResultStatus == ResultStatus.Succes)
                    {
                        var commentAddAjaxViewModel = JsonSerializer.Serialize(new CommendAddAjaxViewModel
                        {
                            CommentDto = result.Data,
                            CommentAddPartial = await this.RenderViewToStringAsync("_CommentAddPartial", commentAddDto),
                        }, new JsonSerializerOptions
                        {
                            ReferenceHandler = ReferenceHandler.Preserve
                        });
                        return Json(commentAddAjaxViewModel);
                    }
                    ModelState.AddModelError("", result.Message);
            }
            var commentAddAjaxErrorViewModel = JsonSerializer.Serialize(new CommendAddAjaxViewModel
            {
                CommentAddDto = commentAddDto,
                CommentAddPartial = await this.RenderViewToStringAsync("_CommentAddPartial", commentAddDto),
            });
            return Json(commentAddAjaxErrorViewModel);
        }
    }
}
