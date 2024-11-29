namespace Carglass.TechnicalAssessment.Backend.Entities;

public class Product : IEntity
{
    public Key Key => new Key(Id);
    public int Id { get; set; }
    public string ProductName { get; set; }
    public int ProductType { get; set; }
    public int NumTerminal { get; set; }
    public DateTime SoldAt { get; set; }
}