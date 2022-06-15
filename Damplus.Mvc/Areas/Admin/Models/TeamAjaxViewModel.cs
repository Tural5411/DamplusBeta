using Damplus.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Damplus.Mvc.Areas.Admin.Models
{
    public class TeamAjaxViewModel
    {
        public TeamAddDto TeamAddDto{ get; set; }
        public string TeamAddPartial { get; set; }
        public TeamDto TeamDto{ get; set; }
    }
}
