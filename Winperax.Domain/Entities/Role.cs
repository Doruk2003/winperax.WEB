using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Winperax.Domain.Entities;

public class Role
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; private set; } = string.Empty;

    public string Name { get; private set; }

    private Role() { }

    public Role(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Role name cannot be empty.");

        Id = ObjectId.GenerateNewId().ToString();
        Name = name.Trim();
    }

    public void Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Role name cannot be empty.");

        Name = newName.Trim();
    }
}
