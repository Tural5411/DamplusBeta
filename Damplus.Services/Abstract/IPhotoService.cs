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
    public interface IPhotoService
    {
        //All Service Methods Are Async!
        Task<IDataResult<ArticleDto>> Get(int articleId);
        Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAndActive();
        Task<IResult> Add(ArticleAddDto articleAddDto, string createdByName);
        Task<IResult> HardDelete(int articleId);
    }
}
