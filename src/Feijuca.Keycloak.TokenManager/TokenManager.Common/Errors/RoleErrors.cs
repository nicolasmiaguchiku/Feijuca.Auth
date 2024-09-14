using TokenManager.Common.Models;

namespace TokenManager.Common.Errors
{
    public static class RoleErrors
    {
        public static string TechnicalMessage { get; private set; } = "";

        public static Error GetRoleErrors => new(
            "Clients.GetRoleErrors",
            $"An error occurred while trying get roles {TechnicalMessage}"
        );
        
        public static void SetTechnicalMessage(string technicalMessage)
        {
            TechnicalMessage = technicalMessage;
        }
    }
}
