using Microsoft.EntityFrameworkCore;
using Damplus.Data.Abstract;
using Damplus.Entities.Concrete;
using Damplus.Shared.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Data.Concrete.EntityFramework.Repositories
{
    public class BusinessCategoryRepository : EfEntityRepositoryBase<BusinessCategory>, IBusinessCategoryRepository
    {
        public BusinessCategoryRepository(DbContext Context) : base(Context)
        {
        }
    }
}
