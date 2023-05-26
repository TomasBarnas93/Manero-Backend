using Manero_Backend.Models.Auth;

namespace Manero_Backend.Models.Entities
{
    public class UserCodeEntity : BaseEntity
    {
        public string UserId { get; set; } = null!;
        public AppUser AppUser { get; set; }

        public string Code { get; set; } = null!;
        public long ValidToUnix { get; set; }
        public bool Verified { get; set; }
    }
}
