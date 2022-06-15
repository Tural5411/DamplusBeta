using Damplus.Entities.DTOs;
using Damplus.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Services.Abstract
{
    public interface ICommentService
    {
        Task<IDataResult<CommentDto>> Get(int commentId);
        Task<IDataResult<CommentUpdateDto>> GetCommentUpdateDto(int commentId);
        Task<IDataResult<CommentListDto>> GetAll();
        Task<IDataResult<CommentListDto>> GetAllByDeleted();
        Task<IDataResult<CommentListDto>> GetAllByNonDeleted();
        Task<IDataResult<CommentListDto>> GetAllByNonDeletedAndActive();
        Task<IDataResult<CommentDto>> Approve(int commentId, string modifiedByName);
        Task<IDataResult<CommentDto>> Add(CommentAddDto commentAddDto);
        Task<IDataResult<CommentDto>> Update(CommentUpdateDto commentUpdateDto, string modifiedByName);
        Task<IDataResult<CommentDto>> Delete(int commentId, string modifiendByName);
        Task<IDataResult<CommentDto>> UndoDelete(int commentId, string modifiendByName);
        Task<IResult> HardDelete(int commentId);
        Task<IDataResult<int>> Count();
        Task<IDataResult<int>> CountByNonDeleted();
    }
}
