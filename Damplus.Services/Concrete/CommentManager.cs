using AutoMapper;
using Damplus.Shared.Utilities.Results.Abstract;
using Damplus.Shared.Utilities.Results.ComplexTypes;
using Damplus.Shared.Utilities.Results.Concrete;
using Damplus.Data.Abstract.UnitOfWorks;
using Damplus.Entities.Concrete;
using Damplus.Entities.DTOs;
using Damplus.Services.Abstract;
using Damplus.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Services.Concrete
{
    public class CommentManager : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CommentManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<CommentDto>> Add(CommentAddDto commentAddDto)
        {
            var article = await _unitOfWork.Articles.GetAsync(a => a.Id == commentAddDto.ArticleId);
            if (article == null)
            {
                return new DataResult<CommentDto>(ResultStatus.Error, Messages.Article.NotFound(false), null);
            }
            var comment = _mapper.Map<Comment>(commentAddDto);
            var addedComment = await _unitOfWork.Comments.AddAsync(comment);
            article.CommentCount += 1;
            await _unitOfWork.Articles.UpdateAsync(article);
            await _unitOfWork.SaveAsync();
            return new DataResult<CommentDto>(ResultStatus.Succes, Messages.Comment.Add(commentAddDto.CreatedByName), new CommentDto
            {
                Comment = addedComment,
                Message= Messages.Comment.Add(commentAddDto.CreatedByName)
            });
        }

        public async Task<IDataResult<CommentDto>> Approve(int commentId, string modifiedByName)
        {
            var comment = await _unitOfWork.Comments.GetAsync(c => c.Id == commentId, c => c.Article);
            if (comment != null)
            {
                var article = comment.Article;
                comment.IsActive = true;
                comment.ModifiedByName = modifiedByName;
                comment.ModifiedDate = DateTime.Now;
                var updatedComment = await _unitOfWork.Comments.UpdateAsync(comment);
                article.CommentCount = await _unitOfWork.Comments.CountAsync(c => c.ArticleId == article.Id && !c.IsDeleted);
                await _unitOfWork.Articles.UpdateAsync(article);
                await _unitOfWork.SaveAsync();
                return new DataResult<CommentDto>(ResultStatus.Succes, Messages.Comment.Approve(commentId), new CommentDto
                {
                    Comment = updatedComment,
                    Message= Messages.Comment.Approve(commentId)
                });
            }
            return new DataResult<CommentDto>(ResultStatus.Error, Messages.Comment.NotFound(false), null);
        }

        public async Task<IDataResult<int>> Count()
        {
            var commentCount = await _unitOfWork.Comments.CountAsync(x => x.IsActive);
            if (commentCount > -1)
            {
                return new DataResult<int>(ResultStatus.Succes, commentCount);
            }
            return new DataResult<int>(ResultStatus.Error, "Xəta baş verdi", -1);
        }

        public async Task<IDataResult<int>> CountByNonDeleted()
        {
            var commentCount = await _unitOfWork.Comments.CountAsync(c => !c.IsDeleted);
            if (commentCount > -1)
            {
                return new DataResult<int>(ResultStatus.Succes, commentCount);
            }
            return new DataResult<int>(ResultStatus.Error, "Xəta baş verdi", -1);
        }

        public async Task<IDataResult<CommentDto>> Delete(int commentId, string modifiendByName)
        {
            var Comment = await _unitOfWork.Comments.GetAsync(c => c.Id == commentId, c => c.Article);
            if (Comment != null)
            {
                var article = Comment.Article;
                Comment.IsDeleted = true;
                Comment.CreatedDate = DateTime.Now;
                Comment.ModifiedByName = modifiendByName;

                var deletedComment = await _unitOfWork.Comments.UpdateAsync(Comment);
                article.CommentCount -= 1;
                await _unitOfWork.Articles.UpdateAsync(article);
                await _unitOfWork.SaveAsync();
                return new DataResult<CommentDto>(ResultStatus.Succes, new CommentDto
                {
                    Comment = deletedComment,
                    Message= Messages.Comment.Delete(deletedComment.CreatedByName)
                });
            }
            else
            {
                return new DataResult<CommentDto>(ResultStatus.Succes, new CommentDto
                {
                    Comment = null,
                });
            }
        }

        public async Task<IDataResult<CommentDto>> Get(int commentId)
        {
            var comment = await _unitOfWork.Comments.GetAsync(c => c.Id == commentId, c => c.Article);
            if (comment != null)
            {
                return new DataResult<CommentDto>(ResultStatus.Succes, new CommentDto
                {
                    Comment = comment
                });
            }
            else
            {
                return new DataResult<CommentDto>(ResultStatus.Error, Messages.Comment.NotFound(false), new CommentDto
                {
                    Comment = null
                });
            }
        }

        public async Task<IDataResult<CommentListDto>> GetAll()
        {
            var comments = await _unitOfWork.Comments.GetAllAsync(null, c => c.Article);
            if (comments.Count > -1)
            {
                return new DataResult<CommentListDto>(ResultStatus.Succes, new CommentListDto
                {
                    ResultStatus = ResultStatus.Succes,
                    Comments = comments
                });
            }
            else
            {
                return new DataResult<CommentListDto>(ResultStatus.Error, new CommentListDto
                {
                    Comments = null,
                    ResultStatus = ResultStatus.Error,
                    Message = Messages.Comment.NotFound(false)
                });
            }
        }

        public async Task<IDataResult<CommentListDto>> GetAllByDeleted()
        {
            var comments = await _unitOfWork.Comments.GetAllAsync(c => c.IsDeleted, c => c.Article);
            if (comments.Count > -1)
            {
                return new DataResult<CommentListDto>(ResultStatus.Succes, new CommentListDto
                {
                    Comments = comments,
                    ResultStatus = ResultStatus.Succes
                });
            }
            else
            {
                return new DataResult<CommentListDto>(ResultStatus.Error, new CommentListDto
                {
                    Comments = null,
                    ResultStatus = ResultStatus.Error,
                    Message = Messages.Comment.NotFound(true)
                });
            }
        }

        public async Task<IDataResult<CommentListDto>> GetAllByNonDeleted()
        {
            var comments = await _unitOfWork.Comments.GetAllAsync(c => !c.IsDeleted, c => c.Article);
            if (comments.Count > -1)
            {
                return new DataResult<CommentListDto>(ResultStatus.Succes, new CommentListDto
                {
                    Comments = comments,
                    ResultStatus = ResultStatus.Succes
                });
            }
            else
            {
                return new DataResult<CommentListDto>(ResultStatus.Error, new CommentListDto
                {
                    Comments = null,
                    ResultStatus = ResultStatus.Error,
                    Message = Messages.Comment.NotFound(isPlural: true)
                });
            }
        }

        public async Task<IDataResult<CommentListDto>> GetAllByNonDeletedAndActive()
        {
            var comments = await _unitOfWork.Comments.GetAllAsync(c => c.IsActive && !c.IsDeleted, c => c.Article);
            if (comments.Count > -1)
            {
                return new DataResult<CommentListDto>(ResultStatus.Succes, new CommentListDto
                {
                    Comments = comments,
                    ResultStatus = ResultStatus.Succes
                });
            }
            else
            {
                return new DataResult<CommentListDto>(ResultStatus.Error, new CommentListDto
                {
                    Comments = null,
                    ResultStatus = ResultStatus.Error,
                    Message = Messages.Comment.NotFound(isPlural: true)
                });
            }
        }

        public async Task<IDataResult<CommentUpdateDto>> GetCommentUpdateDto(int commentId)
        {
            var result = await _unitOfWork.Comments.AnyAsync(c => c.Id == commentId);
            if (result)
            {
                var comment = await _unitOfWork.Comments.GetAsync(c => c.Id == commentId);
                var commentUpdateDto = _mapper.Map<CommentUpdateDto>(comment);
                return new DataResult<CommentUpdateDto>(ResultStatus.Succes, commentUpdateDto);
            }
            else
            {
                return new DataResult<CommentUpdateDto>(ResultStatus.Error, Messages.Comment.NotFound(isPlural: false), null);
            }
        }

        public async Task<IResult> HardDelete(int commentId)
        {
            var comment = await _unitOfWork.Comments.GetAsync(c => c.Id == commentId, c => c.Article);
            if (comment != null)
            {
                if (comment.IsDeleted)
                {
                    await _unitOfWork.Comments.DeleteAsync(comment);
                    await _unitOfWork.SaveAsync();
                    return new Result(ResultStatus.Succes, Messages.Comment.HardDelete(comment.CreatedByName));
                }
                var article = comment.Article;
                await _unitOfWork.Comments.DeleteAsync(comment);
                article.CommentCount = await _unitOfWork.Comments.CountAsync(c => c.ArticleId == article.Id && !c.IsDeleted);
                await _unitOfWork.Articles.UpdateAsync(article);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Succes, Messages.Comment.HardDelete(comment.CreatedByName));
            }
            return new Result(ResultStatus.Error, Messages.Comment.NotFound(false));
        }

        public async Task<IDataResult<CommentDto>> UndoDelete(int commentId, string modifiendByName)
        {
            var comment = await _unitOfWork.Comments.GetAsync(c => c.Id == commentId, c => c.Article);
            if (comment != null)
            {
                var article = comment.Article;
                comment.IsActive = true;
                comment.IsDeleted = false;
                comment.ModifiedByName = modifiendByName;
                comment.ModifiedDate = DateTime.Now;
                var deletedComment = await _unitOfWork.Comments.UpdateAsync(comment);
                article.CommentCount += 1;
                await _unitOfWork.Articles.UpdateAsync(article);
                await _unitOfWork.SaveAsync();

                return new DataResult<CommentDto>(ResultStatus.Succes,Messages.Comment.UndoDelete(deletedComment.CreatedByName),new CommentDto
                {
                    Comment = comment,
                });
            }
            return new DataResult<CommentDto>(ResultStatus.Error, new CommentDto
            {
                Comment = null,
            });
        }

        public async Task<IDataResult<CommentDto>> Update(CommentUpdateDto commentUpdateDto, string modifiedByName)
        {
            var oldComment = await _unitOfWork.Comments.GetAsync(c => c.Id == commentUpdateDto.Id);
            var comment = _mapper.Map<CommentUpdateDto, Comment>(commentUpdateDto, oldComment);
            comment.ModifiedByName = modifiedByName;
            var updatedComment = await _unitOfWork.Comments.UpdateAsync(comment);
            updatedComment.Article = await _unitOfWork.Articles.GetAsync(a => a.Id == updatedComment.ArticleId); ;
            await _unitOfWork.SaveAsync();
            return new DataResult<CommentDto>(ResultStatus.Succes, Messages.Comment.Update(comment.CreatedByName), new CommentDto
            {
                Comment = updatedComment,
            });
        }
    }
}

