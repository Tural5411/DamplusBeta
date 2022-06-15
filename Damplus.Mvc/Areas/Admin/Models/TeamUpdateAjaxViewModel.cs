using Damplus.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Damplus.Mvc.Areas.Admin.Models
{
    public class TeamUpdateAjaxViewModel
    {
        public TeamUpdateDto  TeamUpdateDto { get; set; }
        public TeamDto  TeamDto  { get; set; }
        public string TeamUpdatePartial { get; set; }
    }
}
