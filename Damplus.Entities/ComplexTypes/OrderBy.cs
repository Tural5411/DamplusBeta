using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Entities.ComplexTypes
{
    public enum OrderBy
    {
        [Display(Name = "Tarix")]
        Date = 0,
        [Display(Name = "Oxunma Sayı")]
        ViewCount = 1,
        [Display(Name = "Şərh Sayı")]
        CommentCount = 2
    }
}
