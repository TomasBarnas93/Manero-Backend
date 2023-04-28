using System.ComponentModel.DataAnnotations;

namespace Manero_Backend.Models.Entities
{
    public class TagEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
