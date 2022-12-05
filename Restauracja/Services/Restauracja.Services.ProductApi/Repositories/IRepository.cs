using Restauracja.Services.ProductApi.Model;
using Restauracja.Services.ProductApi.Model.Dto;

namespace Restauracja.Services.ProductApi.Repositories;

public interface IRepository<T, TDto>
    where T : BaseEntity
    where TDto : BaseDtoEntity
{
    Task<IEnumerable<TDto>> GetAll();
    Task<TDto> GetById(long id);
    Task<TDto> Add(TDto entity);
    Task<TDto> Update(TDto entity);
    Task<bool> Delete(long id);
    Task<bool> Exists(long id);
}
