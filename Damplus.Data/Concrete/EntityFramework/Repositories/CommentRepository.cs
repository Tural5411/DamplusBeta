using Damplus.Data.Abstract;
using Damplus.Shared.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Damplus.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Data.Concrete.EntityFramework.Repositories
{
    public class CommentRepository : EfEntityRepositoryBase<Comment>, ICommentRepository
    {
        public CommentRepository(DbContext Context) : base(Context)
        {
        }
    }
}
