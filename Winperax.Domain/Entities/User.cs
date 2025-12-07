using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Winperax.Domain.Entities;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; private set; } = string.Empty;

    public string Username { get; private set; }
    public string PasswordHash { get; private set; }
    public string Email { get; private set; }

    public IReadOnlyCollection<string> Roles => _roles.AsReadOnly();
    private readonly List<string> _roles = new();

    public DateTime CreatedAt { get; private set; }

    private User() { }

    public User(string username, string email, string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username cannot be empty.");

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.");

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash cannot be empty.");

        Id = ObjectId.GenerateNewId().ToString();
        Username = username.Trim();
        Email = email.Trim();
        PasswordHash = passwordHash;
        CreatedAt = DateTime.UtcNow;
    }

    public void ChangePassword(string newHash)
    {
        if (string.IsNullOrWhiteSpace(newHash))
            throw new ArgumentException("Password hash cannot be empty.");

        PasswordHash = newHash;
    }

    public void AssignRole(string roleId)
    {
        if (string.IsNullOrWhiteSpace(roleId))
            throw new ArgumentException("Role ID cannot be empty.");

        if (_roles.Contains(roleId))
            return;

        _roles.Add(roleId);
    }

    public void RemoveRole(string roleId)
    {
        if (string.IsNullOrWhiteSpace(roleId))
            throw new ArgumentException("Role ID cannot be empty.");

        _roles.Remove(roleId);
    }
}
