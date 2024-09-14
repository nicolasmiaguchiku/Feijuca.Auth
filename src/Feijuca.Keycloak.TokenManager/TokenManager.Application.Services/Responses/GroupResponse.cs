using System.Text.RegularExpressions;

namespace TokenManager.Application.Responses
{
    public class GroupResponse
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Path { get; set; }
        public List<Group> SubGroups { get; set; } = [];
    }
}
