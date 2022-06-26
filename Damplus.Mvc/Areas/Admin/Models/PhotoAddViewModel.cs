using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Damplus.Mvc.Areas.Admin.Models
{
    public class PhotoAddViewModel
    {
        [DisplayName("Şəkillər")]
        public string URL { get; set; }
        [Required(ErrorMessage = "{0} boş olmamalıdır.")]
        public int ProjectId { get; set; }
    }
}
