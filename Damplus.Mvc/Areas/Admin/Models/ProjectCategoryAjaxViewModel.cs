using Damplus.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Damplus.Mvc.Areas.Admin.Models
{
    public class ProjectCategoryAjaxViewModel
    {
        public ProjectCategoryDto ProjectCategoryDto { get; set; }
        public string ProjectCategoryPartial { get; set; }
        public ProjectCategoryAddDto ProjectCategoryAddDto { get; set; }

    }
}
