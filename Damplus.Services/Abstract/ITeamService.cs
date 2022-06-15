using Damplus.Entities.DTOs;
using Damplus.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Services.Abstract
{
    public interface ITeamService
    {
        Task<IDataResult<TeamDto>> Get(int teamId);
        Task<IDataResult<TeamUpdateDto>> GetUpdateDto(int teamId);
        Task<IDataResult<TeamListDto>> GetAll();
        Task<IDataResult<TeamListDto>> GetAllByNonDelete();
        Task<IDataResult<TeamListDto>> GetAllByNonDeleteAndActive();
        Task<IDataResult<TeamListDto>> GetAllByDelete();
        Task<IDataResult<TeamDto>> Add(TeamAddDto teamAddDto, string createdByName);
        Task<IDataResult<TeamDto>> Update(TeamUpdateDto teamUpdateDto, string modifiedByName);
        Task<IDataResult<TeamDto>> Delete(int teamId, string modifiedByName);
        Task<IDataResult<TeamDto>> UndoDelete(int teamId, string modifiedByName);
        Task<IResult> HardDelete(int teamId);
    }
}
