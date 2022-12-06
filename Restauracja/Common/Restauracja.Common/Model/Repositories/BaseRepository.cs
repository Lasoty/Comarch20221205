using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Restauracja.Common.Model.Dto;
using Restauracja.Services.ProductApi.Model;

namespace Restauracja.Common.Model.Repositories;

public class BaseRepository<T, TDto> : IRepository<T, TDto> where T : BaseEntity where TDto : BaseDtoEntity
{
    private readonly DbContext _dbContext;
    private readonly IMapper _mapper;

    public BaseRepository(DbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;

        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), "An instance of DbContext is required to use this repository");
        DbSet = _dbContext.Set<T>();
    }

    protected DbSet<T> DbSet { get; set; }

    public async Task<TDto> Add(TDto data)
    {
        T e = _mapper.Map<T>(data);
        EntityEntry dbEntityEntry = _dbContext.Entry(e);
        if (dbEntityEntry.State != EntityState.Detached)
            dbEntityEntry.State = EntityState.Added;
        else
            DbSet.Add(e);

        await _dbContext.SaveChangesAsync();
        data.Id = e.Id;
        return data;
    }

    public async Task<bool> Delete(long id)
    {
        T entity = await DbSet!.FindAsync(id);
        if (entity == null)
            throw new InvalidOperationException($"Entity {typeof(T).Name} with id {id} not found.");

        DbSet.Remove(entity);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public Task<bool> Exists(int id) => DbSet.AnyAsync(x => x.Id == id);

    public async Task<IEnumerable<TDto>> GetAll()
    {
        return _mapper.Map<IEnumerable<TDto>>(await DbSet.ToListAsync());
    }

    public async Task<TDto> GetById(long id)
    {
        T entity = await DbSet.FindAsync(id);
        return _mapper.Map<TDto>(entity);
    }

    public async Task<bool> Update(TDto data)
    {
        T entity = _mapper.Map<T>(data);
        EntityEntry dbEntityEntry = _dbContext.Entry(entity);
        if (dbEntityEntry.State == EntityState.Detached) DbSet.Attach(entity);
        dbEntityEntry.State = EntityState.Modified;

        return await _dbContext.SaveChangesAsync() > 0;
    }
}
