using Restauracja.Common.Model.Dto;
using Restauracja.Services.ProductApi.Model;

namespace Restauracja.Common.Model.Repositories;

public interface IRepository<T, TDto>
    where T : BaseEntity
    where TDto : BaseDtoEntity
{
    Task<IEnumerable<TDto>> GetAll();

    Task<TDto> GetById(long id);

    Task<TDto> Add(TDto entity);

    Task<bool> Update(TDto entity);

    Task<bool> Delete(long id);

    Task<bool> Exists(int id);
}
