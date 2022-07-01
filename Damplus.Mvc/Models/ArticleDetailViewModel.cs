using Damplus.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Damplus.Mvc.Models
{
    public class ArticleDetailViewModel
    {
        public ArticleDto ArticleDto { get; set; }
        public ArticleDetailRightSideBarViewModel ArticleDetailRightSideBarViewModel { get; set; }
    }
}
