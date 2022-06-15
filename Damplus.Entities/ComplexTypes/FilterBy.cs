using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Entities.ComplexTypes
{
    public enum FilterBy
    {
        [Display(Name ="Kateqoriya")]
        Category=0,
        [Display(Name = "Tarix")]
        Date =1,
        [Display(Name = "Oxunma Sayı")]
        ViewCount =2,
        [Display(Name = "Şərh Sayı")]
        CommentCount =3
    }
}
