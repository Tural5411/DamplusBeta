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
    public class PriceRepository : EfEntityRepositoryBase<Price>, IPriceRepository
    {
        public PriceRepository(DbContext Context) : base(Context)
        {
        }
    }
}
