using System;

namespace Winperax.Domain.Entities;

public class Order : BaseEntity
{
    public DateTime Date { get; set; }
    public decimal TotalAmount { get; set; }
}
