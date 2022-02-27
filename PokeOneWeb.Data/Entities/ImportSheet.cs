using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// Represents an imported Google Spreadsheet. The Sheet Hash is used to quickly check
    /// whether a sheet contains any changes.
    /// </summary>
    [Table("ImportSheet")]
    public class ImportSheet
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<ImportSheet>().HasIndex(s => s.SheetName);
            builder.Entity<ImportSheet>().HasIndex(s => s.SheetName);
        }

        /// <summary>
        /// The database ID of this sheet.
        /// </summary>
        [Key] 
        public int Id { get; set; }

        /// <summary>
        /// A constant hash value which builds the URL to this sheet.
        /// </summary>
        [Required]
        public string SpreadsheetId { get; set; }

        /// <summary>
        /// The user-defined, unique name for this sheet.
        /// </summary>
        [Required]
        public string SheetName { get; set; }

        /// <summary>
        /// A hash value calculated over all the contents of the sheet. Can be
        /// used to determine whether there are changes anywhere in the sheet.
        /// </summary>
        public string SheetHash { get; set; }
    }
}
