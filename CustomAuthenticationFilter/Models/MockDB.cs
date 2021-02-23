using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace CustomAuthenticationFilter.Models
{
    public class MockDB
    {
        public MockDB(ILogger<MockDB> logger)
        {
        }

        public static Dictionary<int, UserModel> UserModels = new Dictionary<int, UserModel>
        {
            {1,new UserModel(1,"joe","test@emai.com")}
        };

        public static Dictionary<int, Role> RoleModels = new Dictionary<int, Role>
        {
            {1, new Role(1,"Admin", "biggest boss") }
        };

        public static Dictionary<string, Permission> PermissionModels = new Dictionary<string, Permission>
        {
            {"Read", new Permission(1, "Permission.Auth.Read") },
            {"Write", new Permission(2, "Permission.Auth.Write") },
            {"Delete", new Permission(3, "Permission.Auth.Delete") },
            {"Update", new Permission(4, "Permission.Auth.Update") },
        };

        public static string AddNewPermission(string module, string ability)
        {
            PermissionModels.Add(ability,
                new Permission(PermissionModels.Count + 1, $"Permissions.{module}.{ability}"));
            return PermissionModels[ability].Name;
        }
    }

    public record UserModel(int Id, string Name, string Email);
    public record Role(int Id, string RoleName, string RoleDescription);
    public record Permission(int Id, string Name);

    public class PermissionRequest
    {
        public string Module { get; set; }
        public string Ability { get; set; }
    }
}