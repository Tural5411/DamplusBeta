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
    public class TeamManager : ITeamService
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TeamManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IDataResult<TeamDto>> Add(TeamAddDto teamAddDto, string createdByName)
        {
            var team = _mapper.Map<Teams>(teamAddDto);
            team.CreatedByName = createdByName;
            team.ModifiedByName = createdByName;
            var addedteam=await _unitOfWork.Teams.AddAsync(team);
            await _unitOfWork.SaveAsync();
            return new DataResult<TeamDto>(ResultStatus.Succes, Messages.Team.Add(addedteam.Fullname), new TeamDto
                {
                    Team = addedteam,
                    ResultStatus=ResultStatus.Succes,
                    Message = Messages.Team.Add(addedteam.Fullname)
            });
        }

        public async Task<IDataResult<TeamDto>> Delete(int teamId, string modifiedByName)
        {
            var team = await _unitOfWork.Teams.GetAsync(c => c.Id == teamId);
            if (team != null)
            {
                team.IsActive = false;
                team.IsDeleted = true;
                team.ModifiedByName = modifiedByName;
                team.ModifiedDate = DateTime.Now;

                var deletedteam = await _unitOfWork.Teams.UpdateAsync(team);
                await _unitOfWork.SaveAsync();
                return new DataResult<TeamDto>(ResultStatus.Succes, 
                    Messages.Team.Delete(deletedteam.Fullname), new TeamDto
                    {
                        Team = deletedteam,
                        Message = Messages.Team.Delete(deletedteam.Fullname),
                        ResultStatus = ResultStatus.Succes
                    });
            }
            else
            {
                return new DataResult<TeamDto>(ResultStatus.Error, Messages.Team.NotFound(isPlural: false), new TeamDto
                    {
                        Team = null,
                        Message = Messages.Team.NotFound(isPlural: false),
                        ResultStatus = ResultStatus.Error
                    });
            }
        }

        public async Task<IDataResult<TeamDto>> Get(int teamId)
        {
            var team =await _unitOfWork.Teams.GetAsync(c => c.Id == teamId);
            if (team != null)
            {
                return new DataResult<TeamDto>(ResultStatus.Succes,new TeamDto 
                {
                    Team =team,
                    ResultStatus=ResultStatus.Succes
                });
            }
            else
            {
                return new DataResult<TeamDto>(ResultStatus.Error, Messages.Team.NotFound(isPlural:false), new TeamDto { 
                Team=null,
                ResultStatus=ResultStatus.Error,
                Message= Messages.Team.NotFound(isPlural: false)
                });
            }
        }

        public async Task<IDataResult<TeamListDto>> GetAll()
        {
            var teams =await _unitOfWork.Teams.GetAllAsync(null);
            if (teams.Count>-1)
            {
                return new DataResult<TeamListDto>(ResultStatus.Succes,new TeamListDto 
                {
                Teams=teams,
                ResultStatus=ResultStatus.Succes
                });
            }
            return new DataResult<TeamListDto>(ResultStatus.Error, Messages.Team.NotFound(isPlural: true),
                new TeamListDto {
                    Message = Messages.Team.NotFound(isPlural: true),
                    Teams =null,
                    ResultStatus=ResultStatus.Error
                }) ;
        }

        public async Task<IDataResult<TeamListDto>> GetAllByDelete()
        {
            var teams = await _unitOfWork.Teams.GetAllAsync(c => c.IsDeleted);
            if (teams.Count > -1)
            {
                return new DataResult<TeamListDto>(ResultStatus.Succes, new TeamListDto
                {
                    Teams = teams,
                    ResultStatus=ResultStatus.Succes
                });
            }
            else
            {
                return new DataResult<TeamListDto>(ResultStatus.Error, new TeamListDto
                {
                    Teams = null,
                    ResultStatus = ResultStatus.Error,
                    Message=Messages.Team.NotFound(true)
                });
            }
        }

        public async Task<IDataResult<TeamListDto>> GetAllByNonDelete()
        {                                                                   //==false
            var teams = await _unitOfWork.Teams.GetAllAsync(c => !c.IsDeleted);
            if (teams.Count > -1)
            {
                return new DataResult<TeamListDto>(ResultStatus.Succes, new TeamListDto 
                { 
                Teams =teams,
                ResultStatus=ResultStatus.Succes
                });
            }
            else
            {
                return new DataResult<TeamListDto>(ResultStatus.Error, Messages.Team.NotFound(isPlural: true), new TeamListDto { 
                    Teams =null,
                    ResultStatus=ResultStatus.Error,
                    Message= Messages.Team.NotFound(isPlural: true)
                });
            }
        }

        public async Task<IDataResult<TeamListDto>> GetAllByNonDeleteAndActive()
        {
            var teams = await _unitOfWork.Teams.GetAllAsync(c => c.IsActive && !c.IsDeleted);
            if (teams.Count > -1)
            {
                return new DataResult<TeamListDto>(ResultStatus.Succes, new TeamListDto
                {
                    Teams = teams,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<TeamListDto>(ResultStatus.Error, Messages.Team.NotFound(isPlural: true), null);
        }

        public async Task<IDataResult<TeamUpdateDto>> GetUpdateDto(int teamId)
        {
            var result = await _unitOfWork.Teams.AnyAsync(c => c.Id == teamId);
            if (result)
            {
                var team = await _unitOfWork.Teams.GetAsync(c => c.Id == teamId);
                var teamUpdateDto = _mapper.Map<TeamUpdateDto>(team);
                return new DataResult<TeamUpdateDto>(ResultStatus.Succes, teamUpdateDto);
            }
            else
            {
                return new DataResult<TeamUpdateDto>(ResultStatus.Error, Messages.Team.NotFound(isPlural: false), null);
            }
        }

        public async Task<IResult> HardDelete(int teamId)
        {
            var team = await _unitOfWork.Teams.GetAsync(c => c.Id == teamId);
            if (team != null)
            {
                await _unitOfWork.Teams.DeleteAsync(team);
                await _unitOfWork.SaveAsync();

                return new Result(ResultStatus.Succes, Messages.Team.HardDelete(team.Fullname));
            }
            else
            {
                return new Result(ResultStatus.Error, message:
                   $"{team.Fullname} adlı team silinə bilmədi, təkrar yoxlayın");
            }
        }

        public async Task<IDataResult<TeamDto>> UndoDelete(int teamId, string modifiedByName)
        {
            var team = await _unitOfWork.Teams.GetAsync(c => c.Id == teamId);
            if (team != null)
            {
                team.IsDeleted = false;
                team.IsActive = true;
                team.ModifiedByName = modifiedByName;
                team.ModifiedDate = DateTime.Now;

                var deletedTeam = await _unitOfWork.Teams.UpdateAsync(team);
                await _unitOfWork.SaveAsync();
                return new DataResult<TeamDto>(ResultStatus.Succes, new TeamDto
                {
                    Team = deletedTeam,
                    Message = Messages.Team.UndoDelete(team.Fullname),
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<TeamDto>(ResultStatus.Error, new TeamDto
            {
                Team = null,
                Message = Messages.Team.NotFound(false),
                ResultStatus = ResultStatus.Error
            });
        }

        public async Task<IDataResult<TeamDto>> Update(TeamUpdateDto teamUpdateDto, string modifiedByName)
        {
            var oldTeam = await _unitOfWork.Teams.GetAsync(c => c.Id == teamUpdateDto.Id);
            var team =  _mapper.Map<TeamUpdateDto, Teams>(teamUpdateDto, oldTeam);
            team.ModifiedByName = modifiedByName;
            if (team != null)
            {
                var updatedTeam=await _unitOfWork.Teams.UpdateAsync(team);
                await _unitOfWork.SaveAsync();
                return new DataResult<TeamDto>(ResultStatus.Succes, Messages.Team.Add(updatedTeam.Fullname), new TeamDto { 
                    Team= updatedTeam,
                    Message= Messages.Team.Add(updatedTeam.Fullname),
                    ResultStatus=ResultStatus.Succes
                    });
            }
                return new DataResult<TeamDto>(ResultStatus.Error, message: "Xəta baş verdi", new TeamDto
                {
                    Team = null,
                    Message = "Xəta baş verdi",
                    ResultStatus = ResultStatus.Error
                });
        }
    }
}
