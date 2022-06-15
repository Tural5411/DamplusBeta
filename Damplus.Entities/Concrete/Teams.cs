using Damplus.Shared.Entities.Abstract;
using Damplus.Shared.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Entities.Concrete
{
    public class Teams: EntityBase, IEntity
    {
        public string Fullname { get; set; }
        public string Position { get; set; }
        public string Photo { get; set; }
        public string Content { get; set; }
    }
}
