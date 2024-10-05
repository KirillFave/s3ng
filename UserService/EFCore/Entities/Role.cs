namespace UserService.EFCore.Entities;

public class Role : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Permission> Permissions { get; set; }
}