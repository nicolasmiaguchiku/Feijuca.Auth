using TokenManager.Common.Models;

namespace TokenManager.Common.Errors
{
    public static class RoleGroupErrors
    {
        public static string TechnicalMessage { get; private set; } = "";

        public static Error ErrorAddRoleToGroup => new(
            "User.ErrorAddRoleToGroup",
            $"An error occurred while trying adding a new role to the group: {TechnicalMessage}"
        );
    }
}
