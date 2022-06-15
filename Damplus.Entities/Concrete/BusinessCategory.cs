using Damplus.Shared.Entities.Abstract;
using Damplus.Shared.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Entities.Concrete
{
    public class BusinessCategory:EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string Link { get; set; }
        public int UpperCategoryId { get; set; }
        public ICollection<Business> Businesses { get; set; }
    }
}
