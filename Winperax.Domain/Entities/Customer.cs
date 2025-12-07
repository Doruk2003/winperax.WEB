namespace Winperax.Domain.Entities;

public class Customer : BaseEntity
{
    public string Name { get; set; }
    public decimal Balance { get; set; }
}
