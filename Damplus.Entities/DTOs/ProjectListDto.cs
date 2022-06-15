using Damplus.Entities.Concrete;
using Damplus.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Entities.DTOs
{
    public class ProjectListDto : DtoGetBase
    {
        public IList<Project> Projects{ get; set; }
        public int? CategoryId { get; set; }
    }
}
