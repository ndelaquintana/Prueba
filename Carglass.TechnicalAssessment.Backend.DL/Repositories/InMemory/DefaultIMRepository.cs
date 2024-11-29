using Carglass.TechnicalAssessment.Backend.Entities;

namespace Carglass.TechnicalAssessment.Backend.DL.Repositories;

public abstract class DefaultIMRepository<TEntity> : ICrudRepository<TEntity>
    where TEntity : class, IEntity
{
    protected readonly Dictionary<Key,TEntity> _items = new();
    protected abstract void Initialize();

    public DefaultIMRepository() =>
        Initialize();

    public IEnumerable<TEntity> GetAll(Func<TEntity, bool> predicate) =>
        _items.Values.Where(predicate);
    public IEnumerable<TEntity> GetAll() =>
        _items.Values;

    public TEntity? GetById(params object[] values) =>
        _items.GetValueOrDefault(values);

    public void Create(TEntity item)
    {
        if (_items.ContainsKey(item.Key))
            throw new Exception();

       _items.Add(item.Key, item);
    }

    public void Update(TEntity item) =>
        _items[item.Key] = item;

    public void Delete(TEntity item) =>
        _items.Remove(item.Key);
}