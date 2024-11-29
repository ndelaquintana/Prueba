using Carglass.TechnicalAssessment.Backend.Entities;

namespace Carglass.TechnicalAssessment.Backend.DL.Repositories.InMemory.Products;

public class ProductIMRepository : DefaultIMRepository<Product>
{
    protected override void Initialize()
    {
        Create(new Product()
        {
            Id = 1111111,
            ProductName = "Cristal ventanilla delantera",
            ProductType = 25,
            NumTerminal = 933933933,
            SoldAt = DateTime.Parse("2019-01-09 14:26:17")
        });
    }
}