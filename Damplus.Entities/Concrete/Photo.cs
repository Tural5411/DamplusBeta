using Damplus.Shared.Entities.Abstract;
using Damplus.Shared.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Entities.Concrete
{
    public class Photo : EntityBase, IEntity
    {
        public string URL { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project  { get; set; }
    }
}
