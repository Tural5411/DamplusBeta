using Damplus.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Entities.DTOs
{
    public class PhotoAddDto
    {
        [DisplayName("Şəkillər")]
        public string URL { get; set; }
        [Required(ErrorMessage = "{0} boş olmamalıdır.")]
        public int ProjectId { get; set; }
    }
}
