﻿namespace PokeOneWeb.Data.Entities.Interfaces
{
    public interface IHashedEntity
    {
        public string Hash { get; set; }

        public string IdHash { get; set; }

        public ImportSheet ImportSheet { get; set; }

        public int ImportSheetId { get; set; }
    }
}