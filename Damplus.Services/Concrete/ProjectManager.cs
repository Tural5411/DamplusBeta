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
    public class ProjectManager : IProjectService
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProjectManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IDataResult<ProjectDto>> Add(ProjectAddDto ProjectAddDto, string createdByName)
        {
            var Project = _mapper.Map<Project>(ProjectAddDto);
            Project.CreatedByName = createdByName;
            Project.ModifiedByName = createdByName;
            var addedProject=await _unitOfWork.Projects.AddAsync(Project);
            await _unitOfWork.SaveAsync();
            return new DataResult<ProjectDto>(ResultStatus.Succes, Messages.Project.Add(addedProject.Name), new ProjectDto
                {
                    Project = addedProject,
                    ResultStatus=ResultStatus.Succes,
                    Message= Messages.Project.Add(addedProject.Name)
                });
        }

        public async Task<IDataResult<int>> Count()
        {
            var ProjectsCount = await _unitOfWork.Projects.CountAsync(c=>c.IsActive);
            if (ProjectsCount > -1)
            {
                return new DataResult<int>(ResultStatus.Succes, ProjectsCount);
            }
            return new DataResult<int>(ResultStatus.Error, "Xəta baş verdi", -1);
        }

        public async Task<IDataResult<int>> CountByNonDeleted()
        {
            var ProjectsCount = await _unitOfWork.Projects.CountAsync(c => !c.IsDeleted);
            if (ProjectsCount > -1)
            {
                return new DataResult<int>(ResultStatus.Succes, ProjectsCount);
            }
            return new DataResult<int>(ResultStatus.Error, "Xəta baş verdi", -1);
        }

        public async Task<IDataResult<ProjectDto>> Delete(int ProjectId, string modifiedByName)
        {
            var Project = await _unitOfWork.Projects.GetAsync(c => c.Id == ProjectId);
            if (Project != null)
            {
                Project.IsActive = false;
                Project.IsDeleted = true;
                Project.ModifiedByName = modifiedByName;
                Project.ModifiedDate = DateTime.Now;

                var deletedProject = await _unitOfWork.Projects.UpdateAsync(Project);
                await _unitOfWork.SaveAsync();
                return new DataResult<ProjectDto>(ResultStatus.Succes, 
                    Messages.Project.Delete(deletedProject.Name), new ProjectDto
                    {
                        Project = deletedProject,
                        Message = Messages.Project.Delete(deletedProject.Name),
                        ResultStatus = ResultStatus.Succes
                    });
            }
            else
            {
                return new DataResult<ProjectDto>(ResultStatus.Succes, Messages.Project.NotFound(isPlural: false), new ProjectDto
                    {
                        Project = null,
                        Message = Messages.Project.NotFound(isPlural: false),
                        ResultStatus = ResultStatus.Error
                    });
            }
        }

        public async Task<IDataResult<ProjectDto>> Get(int ProjectId)
        {
            var Project =await _unitOfWork.Projects.GetAsync(c => c.Id == ProjectId,c=>c.ProjectCategory,c=>c.Images);
            if (Project != null)
            {
                return new DataResult<ProjectDto>(ResultStatus.Succes,new ProjectDto 
                {
                    Project=Project,
                    ResultStatus=ResultStatus.Succes
                });
            }
            else
            {
                return new DataResult<ProjectDto>(ResultStatus.Error, Messages.Project.NotFound(isPlural:false), new ProjectDto { 
                Project=null,
                ResultStatus=ResultStatus.Error,
                Message= Messages.Project.NotFound(isPlural: false)
                });
            }
        }

        public async Task<IDataResult<ProjectListDto>> GetAll()
        {
            var Projects =await _unitOfWork.Projects.GetAllAsync(null,x=>x.ProjectCategory);
            if (Projects.Count>-1)
            {
                return new DataResult<ProjectListDto>(ResultStatus.Succes,new ProjectListDto 
                {
                Projects=Projects,
                ResultStatus=ResultStatus.Succes
                });
            }
            return new DataResult<ProjectListDto>(ResultStatus.Error, Messages.Project.NotFound(isPlural: true),
                new ProjectListDto {
                    Message = Messages.Project.NotFound(isPlural: true),
                    Projects=null,
                    ResultStatus=ResultStatus.Error
                }) ;
        }

        public async Task<IDataResult<ProjectListDto>> GetAllByDelete()
        {
            var Projects = await _unitOfWork.Projects.GetAllAsync(c => c.IsDeleted);
            if (Projects.Count > -1)
            {
                return new DataResult<ProjectListDto>(ResultStatus.Succes, new ProjectListDto
                {
                    Projects = Projects,
                    ResultStatus=ResultStatus.Succes
                });
            }
            else
            {
                return new DataResult<ProjectListDto>(ResultStatus.Error, new ProjectListDto
                {
                    Projects = null,
                    ResultStatus = ResultStatus.Error,
                    Message=Messages.Project.NotFound(true)
                });
            }
        }

        public async Task<IDataResult<ProjectListDto>> GetAllByNonDelete()
        {                                                                   //==false
            var Projects = await _unitOfWork.Projects.GetAllAsync(c => !c.IsDeleted);
            if (Projects.Count > -1)
            {
                return new DataResult<ProjectListDto>(ResultStatus.Succes, new ProjectListDto 
                { 
                Projects=Projects,
                ResultStatus=ResultStatus.Succes
                });
            }
            else
            {
                return new DataResult<ProjectListDto>(ResultStatus.Error, Messages.Project.NotFound(isPlural: true), new ProjectListDto { 
                    Projects=null,
                    ResultStatus=ResultStatus.Error,
                    Message= Messages.Project.NotFound(isPlural: true)
                });
            }
        }

        public async Task<IDataResult<ProjectListDto>> GetAllByNonDeleteAndActive()
        {
            var Projects = await _unitOfWork.Projects.GetAllAsync(c => c.IsActive && !c.IsDeleted,c=>c.ProjectCategory,c=>c.Images);
            if (Projects.Count > -1)
            {
                return new DataResult<ProjectListDto>(ResultStatus.Succes, new ProjectListDto
                {
                    Projects = Projects,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<ProjectListDto>(ResultStatus.Error, Messages.Project.NotFound(isPlural: true), null);
        }

        public async Task<IDataResult<ProjectUpdateDto>> GetUpdateDto(int ProjectId)
        {
            var result = await _unitOfWork.Projects.AnyAsync(c => c.Id == ProjectId);
            if (result)
            {
                var Project = await _unitOfWork.Projects.GetAsync(c => c.Id == ProjectId);
                var ProjectUpdateDto = _mapper.Map<ProjectUpdateDto>(Project);
                return new DataResult<ProjectUpdateDto>(ResultStatus.Succes, ProjectUpdateDto);
            }
            else
            {
                return new DataResult<ProjectUpdateDto>(ResultStatus.Error, Messages.Project.NotFound(isPlural: false), null);
            }
        }

        public async Task<IResult> HardDelete(int ProjectId)
        {
            var Project = await _unitOfWork.Projects.GetAsync(c => c.Id == ProjectId);
            if (Project != null)
            {
                await _unitOfWork.Projects.DeleteAsync(Project);
                await _unitOfWork.SaveAsync();

                return new Result(ResultStatus.Succes, Messages.Project.HardDelete(Project.Name));
            }
            else
            {
                return new Result(ResultStatus.Error, message:
                   $"{Project.Name} adlı kateqoriya silinə bilmədi, təkrar yoxlayın");
            }
        }

        public async Task<IDataResult<ProjectDto>> UndoDelete(int ProjectId, string modifiedByName)
        {
            var Project = await _unitOfWork.Projects.GetAsync(c => c.Id == ProjectId);
            if (Project != null)
            {
                Project.IsDeleted = false;
                Project.IsActive = true;
                Project.ModifiedByName = modifiedByName;
                Project.ModifiedDate = DateTime.Now;

                var deletedProject = await _unitOfWork.Projects.UpdateAsync(Project);
                await _unitOfWork.SaveAsync();
                return new DataResult<ProjectDto>(ResultStatus.Succes, new ProjectDto
                {
                    Project = Project,
                    Message = Messages.Project.UndoDelete(Project.Name),
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<ProjectDto>(ResultStatus.Error, new ProjectDto
            {
                Project = null,
                Message = Messages.Project.NotFound(false),
                ResultStatus = ResultStatus.Error
            });
        }

        public async Task<IDataResult<ProjectDto>> Update(ProjectUpdateDto ProjectUpdateDto, string modifiedByName)
        {
            var oldProject = await _unitOfWork.Projects.GetAsync(c => c.Id == ProjectUpdateDto.Id);
            var Project =  _mapper.Map<ProjectUpdateDto,Project>(ProjectUpdateDto,oldProject);
            Project.ModifiedByName = modifiedByName;
            if (Project != null)
            {
                var updatedProject=await _unitOfWork.Projects.UpdateAsync(Project);
                await _unitOfWork.SaveAsync();
                return new DataResult<ProjectDto>(ResultStatus.Succes, Messages.Project.Add(updatedProject.Name), new ProjectDto { 
                    Project=updatedProject,
                    Message= Messages.Project.Add(updatedProject.Name),
                    ResultStatus=ResultStatus.Succes
                    });
            }
                return new DataResult<ProjectDto>(ResultStatus.Error, message: "Xəta baş verdi", new ProjectDto
                {
                    Project = null,
                    Message = "Xəta baş verdi",
                    ResultStatus = ResultStatus.Error
                });
        }
        public async Task<IDataResult<ProjectListDto>> GetAllByPaging(int? categoryId, int currentPage = 1,
           int pageSize = 6, bool isAscending = false)
        {
            pageSize = pageSize > 20 ? 20 : pageSize;
            var projects = categoryId == null
                ? await _unitOfWork.Projects.GetAllAsync(a => a.IsActive && !a.IsDeleted, a => a.ProjectCategory)
                : await _unitOfWork.Projects.GetAllAsync(a => a.IsActive && !a.IsDeleted && a.ProjectCategoryId == categoryId, a => a.ProjectCategory);
            var sortedProjects = isAscending ? projects.OrderBy(a => a.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
                : projects.OrderByDescending(a => a.Id).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return new DataResult<ProjectListDto>(ResultStatus.Succes, new ProjectListDto
            {
                Projects = sortedProjects,
                CategoryId = categoryId == null ? null : categoryId.Value,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = projects.Count,
                ResultStatus = ResultStatus.Succes,
                IsAscending = false
            });
        }
    }
}
