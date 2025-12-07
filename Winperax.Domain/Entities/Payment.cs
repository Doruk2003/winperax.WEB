using System;

namespace Winperax.Domain.Entities;

public class Payment : BaseEntity
{
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}
