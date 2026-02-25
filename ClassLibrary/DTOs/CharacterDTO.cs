using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.DTOs
{
    //
    // DTO ROLE
    // ---------
    // DTOs define the SHAPE of data sent to or received from clients.
    //
    // DTOs:
    // - Do NOT reference EF Core
    // - Do NOT match the database exactly
    // - Protect internal fields from being exposed(easier to hack if they are known)
    //
    // Think of DTOs as your API contract.
    //

    public class CharacterDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int health { get; set; }

        [Required]
        public int Level { get; set; }

        [Range(5, 20)]
        public int strength { get; set; }
        [Range(5, 20)]
        public int charisma { get; set; }
        [Range(5, 20)]
        public int intelegence { get; set; }
        [Range(5, 20)]
        public int wisdome { get; set; }
        [Range(5, 20)]
        public int constitution { get; set; }
        [Range(5, 20)]
        public int dextarity { get; set; }

    }
}
