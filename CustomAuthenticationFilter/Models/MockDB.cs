using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace CustomAuthenticationFilter.Models
{
    public class MockDB
    {
        public MockDB(ILogger<MockDB> logger)
        {
        }

        public static Dictionary<int, UserModel> UserModels = new()
        {
            { 1, new UserModel(1, "joe", "test@emai.com") },
            { 2, new UserModel(1, "sam", "test2@emai.com") },
            { 3, new UserModel(1, "gayle", "test3@emai.com") },
        };

        public static Dictionary<int, Role> RoleModels = new()
        {
            { 1, new Role(1, "Admin", "biggest boss") },
            { 2, new Role(1, "Manager", "small boss") },
            { 3, new Role(1, "User", "user") },
        };

        public static Dictionary<string, Permission> PermissionModels = new()
        {
            { "Read", new Permission(1, "Permission.Authy.Read") },
            { "Write", new Permission(2, "Permission.Authy.Write") },
            { "Delete", new Permission(3, "Permission.Authy.Delete") },
            { "Update", new Permission(4, "Permission.Authy.Update") },
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