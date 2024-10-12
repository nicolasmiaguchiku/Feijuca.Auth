using Feijuca.Auth.Common.Models;

namespace Feijuca.Auth.Common.Errors
{
    public static class UserGroupErrors
    {
        public static string TechnicalMessage { get; private set; } = "";

        public static Error ErrorAddUserToGroup => new(
            "User.ErrorAddUserToGroup",
            $"An error occurred while trying adding a new user to the group: {TechnicalMessage}"
        );
    }
}
