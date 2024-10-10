using Feijuca.MultiTenancy.Services.Models;

namespace Feijuca.MultiTenancy.Api.Models
{
    public class Settings
    {
        public required AuthSettings AuthSettings { get; set; }
    }
}
