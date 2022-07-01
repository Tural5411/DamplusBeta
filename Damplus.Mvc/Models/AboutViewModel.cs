using Damplus.Entities.Concrete;
using Damplus.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Damplus.Mvc.Models
{
    public class AboutViewModel
    {
        public  AboutUsPageInfo aboutUsPageInfo { get; set; }
        public TeamListDto TeamListDto { get; set; }
    }
}
