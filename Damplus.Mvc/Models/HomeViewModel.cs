using Damplus.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Damplus.Mvc.Models
{
    public class HomeViewModel
    {
        public BusinessListDto  BusinessListDto{ get; set; }
        public ArticleListDto ArticleListDto { get; set; }
    }
}
