using System;

namespace Winperax.Api.Models
{
    public class AuditLog
    {
        public string Id { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string Action { get; set; } = null!;
        public string EntityName { get; set; } = null!;
        public string EntityId { get; set; } = null!;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string? AdditionalData { get; set; }
    }
}
