using TokenManager.Common.Models;

namespace TokenManager.Common.Errors
{
    public static class GroupErrors
    {
        public static string TechnicalMessage { get; private set; } = "";

        public static Error CreationGroupError => new(
            "Group.CreationGroupError",
            $"An error occurred while trying creating the group: {TechnicalMessage}"
        );
        
        public static Error DeletionGroupError => new(
            "Group.DeletionGroupError",
            $"An error occurred while trying delete the group: {TechnicalMessage}"
        );

        public static Error GetGroupsError => new(
            "Group.GetGroupsError",
            $"An error occurred while trying get the groups: {TechnicalMessage}"
        );

        public static void SetTechnicalMessage(string technicalMessage)
        {
            TechnicalMessage = technicalMessage;
        }
    }
}
