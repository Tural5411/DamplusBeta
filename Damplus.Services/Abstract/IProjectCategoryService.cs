using Damplus.Entities.DTOs;
using Damplus.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Services.Abstract
{
    public interface IProjectCategoryService
    {
        Task<IDataResult<ProjectCategoryDto>> Get(int projectCategoryId);
        Task<IDataResult<ProjectCategoryUpdateDto>> GetUpdateDto(int projectCategoryId);
        Task<IDataResult<ProjectCategoryListDto>> GetAll();
        Task<IDataResult<ProjectCategoryListDto>> GetAllByNonDelete();
        Task<IDataResult<ProjectCategoryListDto>> GetAllByNonDeleteAndActive();
        Task<IDataResult<ProjectCategoryListDto>> GetAllByDelete();
        Task<IDataResult<ProjectCategoryDto>> Add(ProjectCategoryAddDto projectCategoryAddDto, string createdByName);
        Task<IDataResult<ProjectCategoryDto>> Update(ProjectCategoryUpdateDto projectCategoryUpdateDto, string modifiedByName);
        Task<IDataResult<ProjectCategoryDto>> Delete(int projectCategoryId, string modifiedByName);
        Task<IDataResult<ProjectCategoryDto>> UndoDelete(int projectCategoryId, string modifiedByName);
        Task<IResult> HardDelete(int projectCategoryId);
    }
}
