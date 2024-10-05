namespace UserService.EFCore.Entities;

public class Permission : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}