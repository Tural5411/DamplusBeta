using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Entities.Concrete
{
    public class SliderPageInfo
    {
        [DisplayName("Başlıq")]
        [Required(ErrorMessage = "{0} boş olmamalıdır.")]
        [MaxLength(300, ErrorMessage = "{0} {1} karakterdən böyük olmamalıdır. ")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterdən kiçik olmamalıdır. ")]
        public string Header { get; set; }
        [DisplayName("Kontent")]
        [Required(ErrorMessage = "{0} boş olmamalıdır.")]
        [MaxLength(500, ErrorMessage = "{0} {1} karakterdən böyük olmamalıdır. ")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterdən kiçik olmamalıdır. ")]
        public string Content { get; set; }
    }
}
