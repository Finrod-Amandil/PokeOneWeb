using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data.Entities.Interfaces;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// Represents an imported Google Sheet. The Sheet Hash is used to quickly check
    /// whether a sheet contains any changes.
    /// </summary>
    [Table("ImportSheet")]
    public class ImportSheet : IEntity
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<ImportSheet>().HasIndex(s => s.SheetName);
            builder.Entity<ImportSheet>().HasIndex(s => s.SheetName);
        }

        /// <summary>
        /// Gets or sets the database ID of this sheet.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a constant hash value which builds the URL to this sheet.
        /// </summary>
        [Required]
        public string SpreadsheetId { get; set; }

        /// <summary>
        /// Gets or sets the user-defined, unique name for this sheet.
        /// </summary>
        [Required]
        public string SheetName { get; set; }
    }
}