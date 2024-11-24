using Carglass.TechnicalAssessment.Backend.Entities;
using System;

namespace Carglass.TechnicalAssessment.Backend.DL.Repositories;

public class ProductIMRepository : ICrudRepository<Product>
{
    private readonly ICollection<Product> _products;
    private readonly HashSet<int> _productIds;
    public ProductIMRepository()
    {
        _products = new HashSet<Product>()
        {
            new Product()
            {
                Id = 1111111,
                ProductName = "Cristal ventanilla delantera",
                ProductType = 25,
                NumTerminal = 933933933,
                SoldAt = DateTime.Parse("2019-01-09 14:26:17")
            }
        };
        _productIds = new HashSet<int>(
            _products.Select(z => z.Id)
            );
    }

    public IEnumerable<Product> GetAll(Func<Product, bool>? predicate = null)
    {
        if (predicate != null)
            return _products.Where(x => predicate(x));

        return _products;
    }

    public Product? GetById(params object[] keyValues)
    {
        return _products.SingleOrDefault(x => x.Id.Equals(keyValues[0]));
    }


    public void Create(Product item)
    {
        if (_productIds.Contains(item.Id))
            throw new Exception();

        _products.Add(item);
        _productIds.Add(item.Id);
    }

    public void Update(Product item)
    {
        if (_productIds.Contains(item.Id))
        {
            var toUpdateItem = _products.Single(x => x.Id.Equals(item.Id));
            Delete(toUpdateItem);
            Create(item);
        }
    }

    public void Delete(Product item)
    {
        var toDeleteItem = _products.Single(x => x.Id.Equals(item.Id));
        _products.Remove(toDeleteItem);
        _productIds.Remove(toDeleteItem.Id);
    }
}
