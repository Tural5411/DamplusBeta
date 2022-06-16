using Damplus.Data.Abstract;
using Damplus.Data.Abstract.UnitOfWorks;
using Damplus.Data.Concrete.EntityFramework.Context;
using Damplus.Data.Concrete.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Data.Concrete.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DamplusContext _context;
        private  ArticleRepository _articleRepository;
        private  PhotoRepository   _photoRepository;
        private  CommentRepository _commentRepository;
        private  CategoryRepository _categoryRepository;
        private  ProjectRepository _projectRepository;
        private  ProjectCategoryRepository _projectCategoryRepository;
        private  BusinessRepository _businessRepository;
        private  TeamRepository _teamRepository;
        private VideoRepository _videoRepository;
    
        public UnitOfWork(DamplusContext context)
        {
            _context = context;
        }
        public IArticleRepository Articles => _articleRepository ??= new ArticleRepository(_context);

        public ICategoryRepository Categories => _categoryRepository ??= new CategoryRepository(_context);

        public ICommentRepository Comments => _commentRepository ??= new CommentRepository(_context);

        public IProjectRepository Projects => _projectRepository ??= new ProjectRepository(_context);

        public IProjectCategoryRepository ProjectCategories => _projectCategoryRepository ??= new ProjectCategoryRepository(_context);

        public IBusinessRepository Business => _businessRepository ??= new BusinessRepository(_context);

        public ITeamRepository Teams => _teamRepository ??= new TeamRepository(_context);

        public IVideoRepository Videos => _videoRepository ??= new VideoRepository(_context);

        public IPhotoRepository Photos => _photoRepository ??= new PhotoRepository(_context);

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<int> SaveAsync()
        {
           return await  _context.SaveChangesAsync();
        }
    }
}
