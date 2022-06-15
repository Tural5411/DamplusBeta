using Damplus.Shared.Data.Abstract;
using Damplus.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Data.Abstract
{
    public interface ICommentRepository:IEntityRepository<Comment>
    {
    }
}
