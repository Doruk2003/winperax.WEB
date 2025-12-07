using System;

namespace Winperax.Domain.Entities;

public class Payroll : BaseEntity
{
    public string EmployeeId { get; set; }
    public DateTime Period { get; set; }
    public decimal Gross { get; set; }
    public decimal Net { get; set; }
}
