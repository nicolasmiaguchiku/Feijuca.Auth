using TokenManager.Common.Models;

namespace TokenManager.Domain.Errors
{
    public static class GroupErrors
    {
        public static string TechnicalMessage { get; private set; } = "";

        public static Error CreationGroupError => new(
            "Group.CreationGroupError",
            $"An error occurred while trying creating the group: {TechnicalMessage}"
        );

        public static void SetTechnicalMessage(string technicalMessage)
        {
            TechnicalMessage = technicalMessage;
        }
    }
}
