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
    public class CategoryManager : ICategoryService
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IDataResult<CategoryDto>> Add(CategoryAddDto categoryAddDto, string createdByName)
        {
            var category = _mapper.Map<Category>(categoryAddDto);
            category.CreatedByName = createdByName;
            category.ModifiedByName = createdByName;
            var addedcategory=await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveAsync();
            return new DataResult<CategoryDto>(ResultStatus.Succes, Messages.Category.Add(addedcategory.Name), new CategoryDto
                {
                    Category = addedcategory,
                    ResultStatus=ResultStatus.Succes,
                    Message= Messages.Category.Add(addedcategory.Name)
                });
        }

        public async Task<IDataResult<int>> Count()
        {
            var categoriesCount = await _unitOfWork.Categories.CountAsync(c=>c.IsActive);
            if (categoriesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Succes, categoriesCount);
            }
            return new DataResult<int>(ResultStatus.Error, "Xəta baş verdi", -1);
        }

        public async Task<IDataResult<int>> CountByNonDeleted()
        {
            var categoriesCount = await _unitOfWork.Categories.CountAsync(c => !c.IsDeleted);
            if (categoriesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Succes, categoriesCount);
            }
            return new DataResult<int>(ResultStatus.Error, "Xəta baş verdi", -1);
        }

        public async Task<IDataResult<CategoryDto>> Delete(int categoryId, string modifiedByName)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                category.IsActive = false;
                category.IsDeleted = true;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;

                var deletedcategory = await _unitOfWork.Categories.UpdateAsync(category);
                await _unitOfWork.SaveAsync();
                return new DataResult<CategoryDto>(ResultStatus.Succes, 
                    Messages.Category.Delete(deletedcategory.Name), new CategoryDto
                    {
                        Category = deletedcategory,
                        Message = Messages.Category.Delete(deletedcategory.Name),
                        ResultStatus = ResultStatus.Succes
                    });
            }
            else
            {
                return new DataResult<CategoryDto>(ResultStatus.Succes, Messages.Category.NotFound(isPlural: false), new CategoryDto
                    {
                        Category = null,
                        Message = Messages.Category.NotFound(isPlural: false),
                        ResultStatus = ResultStatus.Error
                    });
            }
        }

        public async Task<IDataResult<CategoryDto>> Get(int categoryId)
        {
            var category =await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId,c=>c.Articles);
            if (category != null)
            {
                return new DataResult<CategoryDto>(ResultStatus.Succes,new CategoryDto 
                {
                    Category=category,
                    ResultStatus=ResultStatus.Succes
                });
            }
            else
            {
                return new DataResult<CategoryDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural:false), new CategoryDto { 
                Category=null,
                ResultStatus=ResultStatus.Error,
                Message= Messages.Category.NotFound(isPlural: false)
                });
            }
        }

        public async Task<IDataResult<CategoryListDto>> GetAll()
        {
            var categories =await _unitOfWork.Categories.GetAllAsync(null);
            if (categories.Count>-1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Succes,new CategoryListDto 
                {
                Categories=categories,
                ResultStatus=ResultStatus.Succes
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural: true),
                new CategoryListDto {
                    Message = Messages.Category.NotFound(isPlural: true),
                    Categories=null,
                    ResultStatus=ResultStatus.Error
                }) ;
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByDelete()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => c.IsDeleted);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Succes, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus=ResultStatus.Succes
                });
            }
            else
            {
                return new DataResult<CategoryListDto>(ResultStatus.Error, new CategoryListDto
                {
                    Categories = null,
                    ResultStatus = ResultStatus.Error,
                    Message=Messages.Category.NotFound(true)
                });
            }
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDelete()
        {                                                                   //==false
            var categories = await _unitOfWork.Categories.GetAllAsync(c => !c.IsDeleted);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Succes, new CategoryListDto 
                { 
                Categories=categories,
                ResultStatus=ResultStatus.Succes
                });
            }
            else
            {
                return new DataResult<CategoryListDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural: true), new CategoryListDto { 
                    Categories=null,
                    ResultStatus=ResultStatus.Error,
                    Message= Messages.Category.NotFound(isPlural: true)
                });
            }
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeleteAndActive()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => c.IsActive && !c.IsDeleted);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Succes, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural: true), null);
        }

        public async Task<IDataResult<CategoryUpdateDto>> GetUpdateDto(int categoryId)
        {
            var result = await _unitOfWork.Categories.AnyAsync(c => c.Id == categoryId);
            if (result)
            {
                var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
                var categoryUpdateDto = _mapper.Map<CategoryUpdateDto>(category);
                return new DataResult<CategoryUpdateDto>(ResultStatus.Succes, categoryUpdateDto);
            }
            else
            {
                return new DataResult<CategoryUpdateDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural: false), null);
            }
        }

        public async Task<IResult> HardDelete(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                await _unitOfWork.Categories.DeleteAsync(category);
                await _unitOfWork.SaveAsync();

                return new Result(ResultStatus.Succes, Messages.Category.HardDelete(category.Name));
            }
            else
            {
                return new Result(ResultStatus.Error, message:
                   $"{category.Name} adlı kateqoriya silinə bilmədi, təkrar yoxlayın");
            }
        }

        public async Task<IDataResult<CategoryDto>> UndoDelete(int categoryId, string modifiedByName)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                category.IsDeleted = false;
                category.IsActive = true;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;

                var deletedCategory = await _unitOfWork.Categories.UpdateAsync(category);
                await _unitOfWork.SaveAsync();
                return new DataResult<CategoryDto>(ResultStatus.Succes, new CategoryDto
                {
                    Category = category,
                    Message = Messages.Category.UndoDelete(category.Name),
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, new CategoryDto
            {
                Category = null,
                Message = Messages.Category.NotFound(false),
                ResultStatus = ResultStatus.Error
            });
        }

        public async Task<IDataResult<CategoryDto>> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            var oldCategory = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryUpdateDto.Id);
            var category =  _mapper.Map<CategoryUpdateDto,Category>(categoryUpdateDto,oldCategory);
            category.ModifiedByName = modifiedByName;
            if (category != null)
            {
                var updatedcategory=await _unitOfWork.Categories.UpdateAsync(category);
                await _unitOfWork.SaveAsync();
                return new DataResult<CategoryDto>(ResultStatus.Succes, Messages.Category.Add(updatedcategory.Name), new CategoryDto { 
                    Category=updatedcategory,
                    Message= Messages.Category.Add(updatedcategory.Name),
                    ResultStatus=ResultStatus.Succes
                    });
            }
                return new DataResult<CategoryDto>(ResultStatus.Error, message: "Xəta baş verdi", new CategoryDto
                {
                    Category = null,
                    Message = "Xəta baş verdi",
                    ResultStatus = ResultStatus.Error
                });
        }
    }
}
