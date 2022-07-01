using Damplus.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Damplus.Mvc.Models
{
    public class ServiceDetailViewModel
    {
        public BusinessDto BusinessDto{ get; set; }
        public BusinessListDto BusinessListDto { get; set; }
    }
}
