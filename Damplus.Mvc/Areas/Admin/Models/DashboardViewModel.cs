using Damplus.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Damplus.Mvc.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public int CommentCount { get; set; }
        public int CategoryCount { get; set; }
        public int UserCount { get; set; }
        public int ArticleCount { get; set; }
        public ArticleListDto Articles { get; set; }
    }
}
