using Damplus.Entities.DTOs;
using Damplus.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Services.Abstract
{
    public interface IProjectService
    {
        Task<IDataResult<ProjectDto>> Get(int projectId);
        Task<IDataResult<ProjectUpdateDto>> GetUpdateDto(int projectId);
        Task<IDataResult<ProjectListDto>> GetAll();
        Task<IDataResult<ProjectListDto>> GetAllByNonDelete();
        Task<IDataResult<ProjectListDto>> GetAllByNonDeleteAndActive();
        Task<IDataResult<ProjectListDto>> GetAllByDelete();
        Task<IDataResult<ProjectListDto>> GetAllByPaging(int? categoryId,
         int currentPage = 1, int pageSize = 4, bool isAscending = false);
        Task<IDataResult<ProjectDto>> Add(ProjectAddDto projectAddDto, string createdByName);
        Task<IDataResult<ProjectDto>> Update(ProjectUpdateDto projectUpdateDto, string modifiedByName);
        Task<IDataResult<ProjectDto>> Delete(int projectId, string modifiedByName);
        Task<IDataResult<ProjectDto>> UndoDelete(int projectId, string modifiedByName);
        Task<IResult> HardDelete(int projectId);
    }
}
