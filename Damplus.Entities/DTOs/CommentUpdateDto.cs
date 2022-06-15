using Damplus.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Entities.DTOs
{
    public class CommentUpdateDto:DtoGetBase
    {
        [Required(ErrorMessage = "{0} boş olmamalıdır.")]
        public int Id { get; set; }
        [DisplayName("Şərh")]
        [Required(ErrorMessage = "{0} boş olmamalıdır.")]
        [MaxLength(1000, ErrorMessage ="{0} {1} karakterdən böyük olmamalıdır. ")]
        [MinLength(2, ErrorMessage ="{0} {1} karakterdən kiçik olmamalıdır. ")]
        public string Text { get; set; }
        [DisplayName("Aktif Mi?")]
        [Required(ErrorMessage = "{0} boş olmamalıdır.")]
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "{0} boş olmamalıdır.")]
        public int ArticleId { get; set; }
       
    }
}
