using Damplus.Entities.Concrete;
using Damplus.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Damplus.Mvc.Areas.Admin.Models
{
    public class NotificationViewModel
    {
        public IList<Comment> Comments{ get; set; }
        public int Count { get; set; }
    }
}
