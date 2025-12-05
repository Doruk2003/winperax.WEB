using System.Collections.Generic;

namespace Winperax.BlazorApp.Models
{
    public class Role
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public List<string> Permissions { get; set; } = new();
    }
}
