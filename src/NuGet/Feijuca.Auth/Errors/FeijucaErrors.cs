using Coderaw.Settings.Models;

namespace Feijuca.Auth.Errors
{
    public static class FeijucaErrors
    {
        public static readonly Error GetUserErrors = new("ErrorGetUsers", "An error occured while tried get users.");
        public static readonly Error GenerateTokenError = new("GenerateTokenError", "An error occured while generate JWT Token.");
    }
}
