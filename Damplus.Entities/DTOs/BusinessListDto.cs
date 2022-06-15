using Damplus.Entities.Concrete;
using Damplus.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Entities.DTOs
{
    public class BusinessListDto:DtoGetBase
    {
        
        public IList<Business> Businesses { get; set; }
    }
}
