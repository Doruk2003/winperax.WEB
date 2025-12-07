using System;

namespace Winperax.Domain.Entities;

public class JournalEntry : BaseEntity
{
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
}
