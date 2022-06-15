using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Entities.Concrete
{
    public class AboutUsPageInfo
    {
        [DisplayName("Başlıq")]
        [Required(ErrorMessage = "{0} boş olmamalıdır.")]
        [MaxLength(150, ErrorMessage = "{0} {1} karakterdən böyük olmamalıdır. ")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterdən kiçik olmamalıdır. ")]
        public string Header { get; set; }
        [DisplayName("Kontent")]
        [Required(ErrorMessage = "{0} boş olmamalıdır.")]
        [MaxLength(1500, ErrorMessage = "{0} {1} karakterdən böyük olmamalıdır. ")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterdən kiçik olmamalıdır. ")]
        public string Content { get; set; }
        [DisplayName("Seo Açıqlaması")]
        [Required(ErrorMessage = "{0} boş olmamalıdır.")]
        [MaxLength(150, ErrorMessage = "{0} {1} karakterdən böyük olmamalıdır. ")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterdən kiçik olmamalıdır. ")]
        public string SeoDescription { get; set; }
        [DisplayName("Seo Yazar")]
        [Required(ErrorMessage = "{0} boş olmamalıdır.")]
        [MaxLength(40, ErrorMessage = "{0} {1} karakterdən böyük olmamalıdır. ")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterdən kiçik olmamalıdır. ")]
        public string  SeoAuthor { get; set; }
        [DisplayName("Seo Etiket")]
        [Required(ErrorMessage = "{0} boş olmamalıdır.")]
        [MaxLength(100, ErrorMessage = "{0} {1} karakterdən böyük olmamalıdır. ")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterdən kiçik olmamalıdır. ")]
        public string SeoTags { get; set; }
    }
}
