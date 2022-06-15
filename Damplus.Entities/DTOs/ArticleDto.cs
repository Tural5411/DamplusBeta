using Damplus.Shared.Entities.Abstract;
using Damplus.Shared.Utilities.Results.ComplexTypes;
using Damplus.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Entities.DTOs
{
    public class ArticleDto:DtoGetBase
    {
        public Article Article { get; set; }
    }
}
