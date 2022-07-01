using Damplus.Shared.Entities.Abstract;
using Damplus.Shared.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Entities.Concrete
{
    public class Project:EntityBase,IEntity
    {
        public string Name { get; set; }
        public string Client { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Price { get; set; }
        public string Location { get; set; }
        public string Info { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public int ProjectCategoryId { get; set; }
        public ProjectCategory ProjectCategory { get; set; }
        public virtual ICollection<Photo> Images { get; set; }
    }
}
