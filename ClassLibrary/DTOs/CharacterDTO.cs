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

        public string Name { get; set; } = string.Empty;

        public int health { get; set; }

        public int Level { get; set; }

        public int strength { get; set; }

        public int charisma { get; set; }
        public int intelegence { get; set; }
        public int wisdome { get; set; }
        public int constitution { get; set; }
        public int dextarity { get; set; }

    }
}
