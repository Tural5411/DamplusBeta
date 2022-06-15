using Damplus.Entities.ComplexTypes;
using Damplus.Entities.DTOs;
using Damplus.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Services.Abstract
{
    public interface IArticleService
    {
        //All Service Methods Are Async!
        Task<IDataResult<ArticleDto>> Get(int articleId);
        Task<IDataResult<ArticleListDto>> GetAll();
        Task<IDataResult<ArticleListDto>> GetAllByDeleted();
        Task<IDataResult<ArticleListDto>> GetAllNonDeleted();
        Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAndActive();
        Task<IDataResult<ArticleListDto>> GetAllByViewCount(int takeSize = 3);
        Task<IDataResult<ArticleListDto>> GetAllByCategory(int categoryId);
        Task<IDataResult<ArticleListDto>> GetAllByPaging(int? categoryId,
            int currentPage = 1, int pageSize = 4, bool isAscending = false);
        Task<IDataResult<ArticleListDto>> GetAllUserIdOnFilter(int userId, FilterBy filterBy,
            OrderBy orderBy, bool isAscending, int takeSize, int categoryId,
            DateTime startAt, DateTime endAt, int minViewCount, int maxViewCount, int minCommentCount, int maxCommentCount);
        Task<IDataResult<ArticleListDto>> SearchAsync(string keyword, int currentPage = 1,
            int pageSize = 4, bool isAscending = false);
        Task<IDataResult<ArticleUpdateDto>> GetArticleUpdateDto(int articleId);
        Task<IResult> Add(ArticleAddDto articleAddDto, string createdByName, int userId);
        Task<IResult> Update(ArticleUpdateDto articleUpdateDto, string modifiedByName);
        Task<IResult> Delete(int articleId, string modifiedByName);
        Task<IResult> IncreaseViewCount(int articleId);
        Task<IDataResult<ArticleDto>> UndoDelete(int articleId, string modifiedByName);
        Task<IResult> HardDelete(int articleId);
        Task<IDataResult<int>> Count();
        Task<IDataResult<int>> CountByNonDeleted();
    }
}
