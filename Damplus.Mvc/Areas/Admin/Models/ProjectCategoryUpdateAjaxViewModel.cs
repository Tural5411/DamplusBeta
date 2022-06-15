using Damplus.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Damplus.Mvc.Areas.Admin.Models
{
    public class ProjectCategoryUpdateAjaxViewModel
    {
        public ProjectCategoryDto ProjectCategoryDto { get; set; }
        public string ProjectCategoryUpdatePartial { get; set; }
        public ProjectCategoryUpdateDto ProjectCategoryUpdateDto { get; set; }

    }
}
