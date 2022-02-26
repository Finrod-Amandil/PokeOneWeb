using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PokeOneWeb.Data.Entities
{
    [Table("ImportSheet")]
    public class ImportSheet
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<ImportSheet>().HasIndex(s => s.SheetName);
            builder.Entity<ImportSheet>().HasIndex(s => s.SheetName);
        }

        [Key] 
        public int Id { get; set; }

        [Required]
        public string SpreadsheetId { get; set; }

        [Required]
        public string SheetName { get; set; }

        public string SheetHash { get; set; }
    }
}
