using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Entities.Concrete
{
    public class ChooseUsPageInfo
    {
        [DisplayName("Başlıq")]
        [Required(ErrorMessage = "{0} boş olmamalıdır.")]
        [MaxLength(150, ErrorMessage = "{0} {1} karakterdən böyük olmamalıdır. ")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterdən kiçik olmamalıdır. ")]
        public string Header { get; set; }
        [DisplayName("Kontent1")]
        [Required(ErrorMessage = "{0} boş olmamalıdır.")]
        [MaxLength(300, ErrorMessage = "{0} {1} karakterdən böyük olmamalıdır. ")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterdən kiçik olmamalıdır. ")]
        public string ContentFirst { get; set; }
        [DisplayName("Kontent2")]
        [Required(ErrorMessage = "{0} boş olmamalıdır.")]
        [MaxLength(300, ErrorMessage = "{0} {1} karakterdən böyük olmamalıdır. ")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterdən kiçik olmamalıdır. ")]
        public string ContentSecond { get; set; }
        [DisplayName("Kontent3")]
        [Required(ErrorMessage = "{0} boş olmamalıdır.")]
        [MaxLength(300, ErrorMessage = "{0} {1} karakterdən böyük olmamalıdır. ")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterdən kiçik olmamalıdır. ")]
        public string ContentThird { get; set; }
        [DisplayName("Kontent4")]
        [Required(ErrorMessage = "{0} boş olmamalıdır.")]
        [MaxLength(300, ErrorMessage = "{0} {1} karakterdən böyük olmamalıdır. ")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterdən kiçik olmamalıdır. ")]
        public string ContentFourth { get; set; }
    }
}
