using Damplus.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Damplus.Mvc.Models
{
    public class HeaderViewModel
    {
        public ProjectCategoryListDto ProjectCategoryListDto{ get; set; }
        public BusinessListDto BusinessListDto { get; set; }
    }
}
