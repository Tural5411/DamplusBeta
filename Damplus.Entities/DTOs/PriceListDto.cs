using Damplus.Entities.Concrete;
using Damplus.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Entities.DTOs
{
    public class PriceListDto:DtoGetBase
    {
        public IList<Price> Prices { get; set; }
    }
}
