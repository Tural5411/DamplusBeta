using Damplus.Shared.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Entities.Concrete
{
    public class Banner: EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string  Thumbnail { get; set; }
        public bool IsMain { get; set; }
    }
}
