using Project.Core.Identity.Users.Dtos;

namespace Project.Core.Identity.Users.Features.AssignUserRole;
public class AssignUserRoleCommand
{
    public List<UserRoleDetail> UserRoles { get; set; } = new();
}
