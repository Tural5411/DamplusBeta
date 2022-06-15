using Damplus.Data.Abstract;
using Damplus.Entities.Concrete;
using Damplus.Shared.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Data.Concrete.EntityFramework.Repositories
{
    public class ProjectCategoryRepository : EfEntityRepositoryBase<ProjectCategory>, IProjectCategoryRepository
    {
        public ProjectCategoryRepository(DbContext Context) : base(Context)
        {
        }
    }
}
