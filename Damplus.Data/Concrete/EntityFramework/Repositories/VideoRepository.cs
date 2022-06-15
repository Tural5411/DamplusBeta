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
    public class VideoRepository : EfEntityRepositoryBase<Video>, IVideoRepository
    {
        public VideoRepository(DbContext Context) : base(Context)
        {
        }
    }
}
