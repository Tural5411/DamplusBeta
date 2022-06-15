using Microsoft.AspNetCore.Mvc;
using Damplus.Mvc.Areas.Admin.Models;
using Damplus.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Damplus.Mvc.Areas.Admin.ViewComponents
{
    public class NotificationViewComponent:ViewComponent
    {
        private readonly ICommentService _commentService;
        public NotificationViewComponent(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var messages = await _commentService.GetAllByNonDeleted();
            var messagesCount = await _commentService.CountByNonDeleted();
            if (messages == null)
                return Content("Şərh tapılmadı.");
            return View(new NotificationViewModel
            {
                Comments = messages.Data.Comments,
                Count = messagesCount.Data
            });
        }
    }
}
