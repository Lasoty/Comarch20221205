using Restauracja.Services.ProductApi.Model.Dto;
using Restauracja.Services.ProductApi.Model;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Restauracja.Services.ProductApi.Repositories;

public class BaseRepository<T, TDto> : IRepository<T, TDto>
    where T : BaseEntity
    where TDto : BaseDtoEntity
{
    private readonly DbContext _dbContext;
    private readonly IMapper _mapper;

    public BaseRepository(DbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;

        DbSet = _dbContext.Set<T>();
    }

    protected DbSet<T> DbSet { get; set; }

    public async Task<TDto> Add(TDto entity)
    {
        T e = _mapper.Map<T>(entity);
        EntityEntry dbEntry = _dbContext.Entry(e);
        if (dbEntry.State != EntityState.Detached) 
            dbEntry.State = EntityState.Added;
        else
            DbSet.Add(e);

        await _dbContext.SaveChangesAsync();
        entity.Id = e.Id;

        return entity;
    }

    public async Task<bool> Delete(long id)
    {
        T entity = await DbSet.FindAsync(id);
        if (entity == null)
            throw new InvalidOperationException($"Entity {typeof(T).Name} with id {id} not found.");

        DbSet.Remove(entity);
        return (await _dbContext.SaveChangesAsync()) > 0;
    }

    public Task<bool> Exists(long id) => DbSet.AnyAsync(x => x.Id == id);

    public async Task<IEnumerable<TDto>> GetAll()
    {
        return _mapper.Map<IEnumerable<TDto>>(await DbSet.ToListAsync());
    }

    public async Task<TDto> GetById(long id)
    {
        T entity = await DbSet.FirstAsync(x => x.Id == id);
        return _mapper.Map<TDto>(entity);
    }

    public async Task<TDto> Update(TDto entity)
    {
        T e = _mapper.Map<T>(entity);
        EntityEntry dbEntry = _dbContext.Entry(e);
        if (dbEntry.State != EntityState.Detached)
            dbEntry.State = EntityState.Modified;
        else
            DbSet.Attach(e);

        await _dbContext.SaveChangesAsync();
        return entity;
    }
}
