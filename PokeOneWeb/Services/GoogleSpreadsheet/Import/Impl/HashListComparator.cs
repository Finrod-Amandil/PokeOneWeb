using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl
{
    public class HashListComparator : IHashListComparator
    {
        private readonly ILogger<HashListComparator> _logger;

        public HashListComparator(ILogger<HashListComparator> logger)
        {
            _logger = logger;
        }

        public HashListComparisonResult CompareHashLists(List<RowHash> sheetHashes, List<RowHash> dbHashes)
        {
            var result = new HashListComparisonResult();

            sheetHashes = sheetHashes.OrderBy(h => h.IdHash).ToList();
            dbHashes = dbHashes.OrderBy(h => h.IdHash).ToList();

            CheckForHashCollisions(sheetHashes);

            var sheetHashIndex = 0;
            var sheetHashCount = sheetHashes.Count;
            var dbHashIndex = 0;
            var dbHashCount = dbHashes.Count;

            while (sheetHashIndex < sheetHashCount || dbHashIndex < dbHashCount)
            {
                var sheetIdHash = sheetHashIndex < sheetHashCount ? sheetHashes[sheetHashIndex].IdHash : null;
                var sheetContentHash = sheetHashIndex < sheetHashCount ? sheetHashes[sheetHashIndex].ContentHash : null;
                var dbIdHash = dbHashIndex < dbHashCount ? dbHashes[dbHashIndex].IdHash : null;
                var dbContentHash = dbHashIndex < dbHashCount ? dbHashes[dbHashIndex].ContentHash : null;

                var contentCmp = 
                    sheetIdHash == null ? -1 : 
                    dbIdHash == null ? 1 : 
                    string.Compare(sheetIdHash, dbIdHash, StringComparison.Ordinal);

                if (contentCmp == 0)
                {
                    if (!string.Equals(sheetContentHash, dbContentHash, StringComparison.Ordinal))
                    {
                        result.RowsToUpdate.Add(sheetHashes[sheetHashIndex]);
                    }
                    sheetHashIndex += 1;
                    dbHashIndex += 1;
                }
                else if (contentCmp > 0) // Sheet Hash > DB Hash
                {
                    result.RowsToInsert.Add(sheetHashes[sheetHashIndex]);
                    sheetHashIndex += 1;
                }
                else // Sheet Hash < DB Hash
                {
                    result.RowsToDelete.Add(dbHashes[dbHashIndex]);
                    dbHashIndex += 1;
                }
            }

            return result;
        }

        private void CheckForHashCollisions(IList<RowHash> hashes)
        {
            var previous = "";
            for (var i = 0; i < hashes.Count; i++)
            {
                var current = hashes[i].IdHash;
                if (current.EqualsExact(previous))
                {
                    _logger.LogWarning("Found duplicate ID Hash: " + current);
                    hashes.RemoveAt(i);
                    i -= 1;
                }
                previous = current;
            }
        }
    }
}
