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
    public class ProjectCategoryManager : IProjectCategoryService
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProjectCategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IDataResult<ProjectCategoryDto>> Add(ProjectCategoryAddDto ProjectCategoryAddDto, string createdByName)
        {
            var ProjectCategory = _mapper.Map<ProjectCategory>(ProjectCategoryAddDto);
            ProjectCategory.CreatedByName = createdByName;
            ProjectCategory.ModifiedByName = createdByName;
            var addedProjectCategory=await _unitOfWork.ProjectCategories.AddAsync(ProjectCategory);
            await _unitOfWork.SaveAsync();
            return new DataResult<ProjectCategoryDto>(ResultStatus.Succes, Messages.ProjectCategory.Add(addedProjectCategory.Name), new ProjectCategoryDto
                {
                    ProjectCategory = addedProjectCategory,
                    ResultStatus=ResultStatus.Succes,
                    Message= Messages.ProjectCategory.Add(addedProjectCategory.Name)
                });
        }

        public async Task<IDataResult<ProjectCategoryDto>> Delete(int ProjectCategoryId, string modifiedByName)
        {
            var ProjectCategory = await _unitOfWork.ProjectCategories.GetAsync(c => c.Id == ProjectCategoryId);
            if (ProjectCategory != null)
            {
                ProjectCategory.IsActive = false;
                ProjectCategory.IsDeleted = true;
                ProjectCategory.ModifiedByName = modifiedByName;
                ProjectCategory.ModifiedDate = DateTime.Now;

                var deletedProjectCategory = await _unitOfWork.ProjectCategories.UpdateAsync(ProjectCategory);
                await _unitOfWork.SaveAsync();
                return new DataResult<ProjectCategoryDto>(ResultStatus.Succes, 
                    Messages.ProjectCategory.Delete(deletedProjectCategory.Name), new ProjectCategoryDto
                    {
                        ProjectCategory = deletedProjectCategory,
                        Message = Messages.ProjectCategory.Delete(deletedProjectCategory.Name),
                        ResultStatus = ResultStatus.Succes
                    });
            }
            else
            {
                return new DataResult<ProjectCategoryDto>(ResultStatus.Succes, Messages.ProjectCategory.NotFound(isPlural: false), new ProjectCategoryDto
                    {
                        ProjectCategory = null,
                        Message = Messages.ProjectCategory.NotFound(isPlural: false),
                        ResultStatus = ResultStatus.Error
                    });
            }
        }

        public async Task<IDataResult<ProjectCategoryDto>> Get(int ProjectCategoryId)
        {
            var ProjectCategory =await _unitOfWork.ProjectCategories.GetAsync(c => c.Id == ProjectCategoryId);
            if (ProjectCategory != null)
            {
                return new DataResult<ProjectCategoryDto>(ResultStatus.Succes,new ProjectCategoryDto 
                {
                    ProjectCategory=ProjectCategory,
                    ResultStatus=ResultStatus.Succes
                });
            }
            else
            {
                return new DataResult<ProjectCategoryDto>(ResultStatus.Error, Messages.ProjectCategory.NotFound(isPlural:false), new ProjectCategoryDto { 
                ProjectCategory=null,
                ResultStatus=ResultStatus.Error,
                Message= Messages.ProjectCategory.NotFound(isPlural: false)
                });
            }
        }

        public async Task<IDataResult<ProjectCategoryListDto>> GetAll()
        {
            var ProjectCategorys =await _unitOfWork.ProjectCategories.GetAllAsync(null);
            if (ProjectCategorys.Count>-1)
            {
                return new DataResult<ProjectCategoryListDto>(ResultStatus.Succes,new ProjectCategoryListDto 
                {
                ProjectCategories = ProjectCategorys,
                ResultStatus=ResultStatus.Succes
                });
            }
            return new DataResult<ProjectCategoryListDto>(ResultStatus.Error, Messages.ProjectCategory.NotFound(isPlural: true),
                new ProjectCategoryListDto {
                    Message = Messages.ProjectCategory.NotFound(isPlural: true),
                    ProjectCategories = null,
                    ResultStatus=ResultStatus.Error
                }) ;
        }

        public async Task<IDataResult<ProjectCategoryListDto>> GetAllByDelete()
        {
            var ProjectCategorys = await _unitOfWork.ProjectCategories.GetAllAsync(c => c.IsDeleted);
            if (ProjectCategorys.Count > -1)
            {
                return new DataResult<ProjectCategoryListDto>(ResultStatus.Succes, new ProjectCategoryListDto
                {
                    ProjectCategories = ProjectCategorys,
                    ResultStatus=ResultStatus.Succes
                });
            }
            else
            {
                return new DataResult<ProjectCategoryListDto>(ResultStatus.Error, new ProjectCategoryListDto
                {
                    ProjectCategories = null,
                    ResultStatus = ResultStatus.Error,
                    Message=Messages.ProjectCategory.NotFound(true)
                });
            }
        }

        public async Task<IDataResult<ProjectCategoryListDto>> GetAllByNonDelete()
        {                                                                   //==false
            var ProjectCategorys = await _unitOfWork.ProjectCategories.GetAllAsync(c => !c.IsDeleted);
            if (ProjectCategorys.Count > -1)
            {
                return new DataResult<ProjectCategoryListDto>(ResultStatus.Succes, new ProjectCategoryListDto 
                {
                ProjectCategories = ProjectCategorys,
                ResultStatus=ResultStatus.Succes
                });
            }
            else
            {
                return new DataResult<ProjectCategoryListDto>(ResultStatus.Error, Messages.ProjectCategory.NotFound(isPlural: true), new ProjectCategoryListDto {
                    ProjectCategories = null,
                    ResultStatus=ResultStatus.Error,
                    Message= Messages.ProjectCategory.NotFound(isPlural: true)
                });
            }
        }

        public async Task<IDataResult<ProjectCategoryListDto>> GetAllByNonDeleteAndActive()
        {
            var ProjectCategorys = await _unitOfWork.ProjectCategories.GetAllAsync(c => c.IsActive && !c.IsDeleted);
            if (ProjectCategorys.Count > -1)
            {
                return new DataResult<ProjectCategoryListDto>(ResultStatus.Succes, new ProjectCategoryListDto
                {
                    ProjectCategories = ProjectCategorys,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<ProjectCategoryListDto>(ResultStatus.Error, Messages.ProjectCategory.NotFound(isPlural: true), null);
        }

        public async Task<IDataResult<ProjectCategoryUpdateDto>> GetUpdateDto(int ProjectCategoryId)
        {
            var result = await _unitOfWork.ProjectCategories.AnyAsync(c => c.Id == ProjectCategoryId);
            if (result)
            {
                var ProjectCategory = await _unitOfWork.ProjectCategories.GetAsync(c => c.Id == ProjectCategoryId);
                var ProjectCategoryUpdateDto = _mapper.Map<ProjectCategoryUpdateDto>(ProjectCategory);
                return new DataResult<ProjectCategoryUpdateDto>(ResultStatus.Succes, ProjectCategoryUpdateDto);
            }
            else
            {
                return new DataResult<ProjectCategoryUpdateDto>(ResultStatus.Error, Messages.ProjectCategory.NotFound(isPlural: false), null);
            }
        }

        public async Task<IResult> HardDelete(int ProjectCategoryId)
        {
            var ProjectCategory = await _unitOfWork.ProjectCategories.GetAsync(c => c.Id == ProjectCategoryId);
            if (ProjectCategory != null)
            {
                await _unitOfWork.ProjectCategories.DeleteAsync(ProjectCategory);
                await _unitOfWork.SaveAsync();

                return new Result(ResultStatus.Succes, Messages.ProjectCategory.HardDelete(ProjectCategory.Name));
            }
            else
            {
                return new Result(ResultStatus.Error, message:
                   $"{ProjectCategory.Name} adlı kateqoriya silinə bilmədi, təkrar yoxlayın");
            }
        }

        public async Task<IDataResult<ProjectCategoryDto>> UndoDelete(int ProjectCategoryId, string modifiedByName)
        {
            var ProjectCategory = await _unitOfWork.ProjectCategories.GetAsync(c => c.Id == ProjectCategoryId);
            if (ProjectCategory != null)
            {
                ProjectCategory.IsDeleted = false;
                ProjectCategory.IsActive = true;
                ProjectCategory.ModifiedByName = modifiedByName;
                ProjectCategory.ModifiedDate = DateTime.Now;

                var deletedProjectCategory = await _unitOfWork.ProjectCategories.UpdateAsync(ProjectCategory);
                await _unitOfWork.SaveAsync();
                return new DataResult<ProjectCategoryDto>(ResultStatus.Succes, new ProjectCategoryDto
                {
                    ProjectCategory = ProjectCategory,
                    Message = Messages.ProjectCategory.UndoDelete(ProjectCategory.Name),
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<ProjectCategoryDto>(ResultStatus.Error, new ProjectCategoryDto
            {
                ProjectCategory = null,
                Message = Messages.ProjectCategory.NotFound(false),
                ResultStatus = ResultStatus.Error
            });
        }

        public async Task<IDataResult<ProjectCategoryDto>> Update(ProjectCategoryUpdateDto ProjectCategoryUpdateDto, string modifiedByName)
        {
            var oldProjectCategory = await _unitOfWork.ProjectCategories.GetAsync(c => c.Id == ProjectCategoryUpdateDto.Id);
            var ProjectCategory =  _mapper.Map<ProjectCategoryUpdateDto,ProjectCategory>(ProjectCategoryUpdateDto,oldProjectCategory);
            ProjectCategory.ModifiedByName = modifiedByName;
            if (ProjectCategory != null)
            {
                var updatedProjectCategory=await _unitOfWork.ProjectCategories.UpdateAsync(ProjectCategory);
                await _unitOfWork.SaveAsync();
                return new DataResult<ProjectCategoryDto>(ResultStatus.Succes, Messages.ProjectCategory.Add(updatedProjectCategory.Name), new ProjectCategoryDto { 
                    ProjectCategory=updatedProjectCategory,
                    Message= Messages.ProjectCategory.Add(updatedProjectCategory.Name),
                    ResultStatus=ResultStatus.Succes
                    });
            }
                return new DataResult<ProjectCategoryDto>(ResultStatus.Error, message: "Xəta baş verdi", new ProjectCategoryDto
                {
                    ProjectCategory = null,
                    Message = "Xəta baş verdi",
                    ResultStatus = ResultStatus.Error
                });
        }
    }
}
