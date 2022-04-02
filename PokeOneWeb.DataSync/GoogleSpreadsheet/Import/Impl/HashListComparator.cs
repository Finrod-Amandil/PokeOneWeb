using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl
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

            var sheetHashesOrdered = sheetHashes.OrderBy(h => h.IdHash).ToList();
            var dbHashesOrdered = dbHashes.OrderBy(h => h.IdHash).ToList();

            CheckForHashCollisions(sheetHashesOrdered);

            var sheetHashIndex = 0;
            var sheetHashCount = sheetHashesOrdered.Count;
            var dbHashIndex = 0;
            var dbHashCount = dbHashesOrdered.Count;

            while (sheetHashIndex < sheetHashCount || dbHashIndex < dbHashCount)
            {
                var sheetIdHash = sheetHashIndex < sheetHashCount ? sheetHashesOrdered[sheetHashIndex].IdHash : null;
                var sheetContentHash = sheetHashIndex < sheetHashCount ? sheetHashesOrdered[sheetHashIndex].ContentHash : null;
                var dbIdHash = dbHashIndex < dbHashCount ? dbHashesOrdered[dbHashIndex].IdHash : null;
                var dbContentHash = dbHashIndex < dbHashCount ? dbHashesOrdered[dbHashIndex].ContentHash : null;

                var contentCmp =
                    sheetIdHash == null ? -1 :
                    dbIdHash == null ? 1 :
                    string.Compare(sheetIdHash, dbIdHash, StringComparison.Ordinal);

                if (contentCmp == 0)
                {
                    if (!string.Equals(sheetContentHash, dbContentHash, StringComparison.Ordinal))
                    {
                        result.RowsToUpdate.Add(sheetHashesOrdered[sheetHashIndex]);
                    }

                    sheetHashIndex += 1;
                    dbHashIndex += 1;
                }
                else if (contentCmp > 0) // Sheet Hash > DB Hash
                {
                    result.RowsToInsert.Add(sheetHashesOrdered[sheetHashIndex]);
                    sheetHashIndex += 1;
                }
                else // Sheet Hash < DB Hash
                {
                    result.RowsToDelete.Add(dbHashesOrdered[dbHashIndex]);
                    dbHashIndex += 1;
                }
            }

            result.RowsToDelete = SortHashesByOriginalOrder(result.RowsToDelete, sheetHashes);
            result.RowsToInsert = SortHashesByOriginalOrder(result.RowsToInsert, sheetHashes);
            result.RowsToUpdate = SortHashesByOriginalOrder(result.RowsToUpdate, sheetHashes);

            return result;
        }

        private void CheckForHashCollisions(IList<RowHash> hashes)
        {
            var previous = string.Empty;
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

        private List<RowHash> SortHashesByOriginalOrder(List<RowHash> hashesToSort, List<RowHash> sortedHashes)
        {
            return hashesToSort.OrderBy(sortedHashes.IndexOf).ToList();
        }
    }
}