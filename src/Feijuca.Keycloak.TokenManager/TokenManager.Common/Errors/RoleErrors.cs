using TokenManager.Common.Models;

namespace TokenManager.Common.Errors
{
    public static class RoleErrors
    {
        public static string TechnicalMessage { get; private set; } = "";

        public static Error GetRoleErrors => new(
            "Role.GetRoleErrors",
            $"An error occurred while trying get roles {TechnicalMessage}"
        );
        
        public static Error AddRoleErrors => new(
            "Role.AddRoleErrors",
            $"An error occurred while trying add a new role {TechnicalMessage}"
        );
        
        public static void SetTechnicalMessage(string technicalMessage)
        {
            TechnicalMessage = technicalMessage;
        }
    }
}
