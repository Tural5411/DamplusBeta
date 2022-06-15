using Damplus.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Damplus.Mvc.Areas.Admin.Models
{
    public class BusinessUpdateAjaxViewModel
    {
        public BusinessUpdateDto  BusinessUpdateDto { get; set; }
        public BusinessDto  BusinessDto  { get; set; }
        public string BusinessUpdatePartial { get; set; }
    }
}
