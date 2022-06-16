using Damplus.Shared.Concrete.EntityFramework;
using Damplus.Data.Abstract;
using Damplus.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Damplus.Data.Concrete.EntityFramework.Repositories
{
    public class PhotoRepository : EfEntityRepositoryBase<Photo>, IPhotoRepository
    {
        public PhotoRepository(DbContext Context) : base(Context)
        {
        }
    }
}
