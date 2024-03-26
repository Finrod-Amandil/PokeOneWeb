using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PokeOneWeb.Data.Entities
{
    /// <summary>
    /// PokeOne specific categorization of items.
    /// </summary>
    [Table("ImportLog")]
    public class ImportLog
    {
        public static void ConfigureForDatabase(ModelBuilder builder)
        {
            builder.Entity<ImportLog>().HasIndex(log => log.ImportTime);
        }

        [Key]
        public int Id { get; set; }

        // INDEXED
        public DateTime ImportTime { get; set; }

        public string UpdatedSheets { get; set; }

        public override string ToString()
        {
            return $"{ImportTime}: {UpdatedSheets}";
        }
    }
}