using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Data.Abstract.UnitOfWorks
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IArticleRepository Articles { get; }
        IVideoRepository Videos { get; }
        IPriceRepository Prices { get; }
        IBannerRepository Banners { get; }
        IBusinessRepository Business { get;}
        IBusinessCategoryRepository  BusinessCategories {get;}
        ICategoryRepository Categories { get; }
        ICommentRepository Comments { get; }  //_unitOfWork.Categories.AddAsync()
        IProjectRepository Projects { get; }
        IProjectCategoryRepository ProjectCategories { get; }
        ITeamRepository Teams { get; }
        Task<int> SaveAsync();
    }
}
