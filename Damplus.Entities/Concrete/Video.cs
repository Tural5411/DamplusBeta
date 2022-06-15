using Damplus.Shared.Entities.Abstract;
using Damplus.Shared.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Entities.Concrete
{
    public class Video : EntityBase, IEntity
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Thumbnail { get; set; }
    }
}
