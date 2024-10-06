namespace UserService.EFCore.Entities;

public class Role : IEntity<Guid>
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required List<Permission> Permissions { get; set; }
}