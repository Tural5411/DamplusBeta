using Damplus.Shared.Entities.Abstract;
using Damplus.Shared.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Entities.Concrete
{
    public class ProjectCategory : EntityBase,IEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}
