using Damplus.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Damplus.Mvc.Areas.Admin.Models
{
    public class BusinessAjaxViewModel
    {
        public BusinessAddDto BusinessAddDto { get; set; }
        public string BusinessAddPartial { get; set; }
        public BusinessDto BusinessDto { get; set; }
    }
}
