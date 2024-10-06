namespace UserService.EFCore.Entities;

public class Permission : IEntity<Guid>
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}