using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibrary.Entities
{
    //
    // ENTITY ROLE
    // ------------
    // Entities represent DATABASE TABLES.
    //
    // Entities:
    // - Match the database schema exactly
    // - Use attributes for column mapping
    // - Are NOT safe to return to the frontend
    //
    // Changing this class usually means changing the database.
    //

    [Table("character", Schema = "backend_RPG")] // Maps this class to the "character" table
    public class CharacterEntity
    {
        [Key] // Primary key
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("health")]
        public int health { get; set; }

        [Column("level")]
        public int Level { get; set; }

        [Column("strength")]
        public int strength { get; set; }

        [Column("charisma")]
        public int charisma { get; set; }
        [Column("intelegence")]
        public int intelegence { get; set; }
        [Column("wisdome")]
        public int wisdome { get; set; }
        [Column("constitution")]
        public int constitution { get; set; }
        [Column("dextarity")]
        public int dextarity { get; set; }

        // Exists in the database but NOT exposed to the client
        // [Column("intelegence")]
        // public DateTime CreatedAt { get; set; }
    }
}
