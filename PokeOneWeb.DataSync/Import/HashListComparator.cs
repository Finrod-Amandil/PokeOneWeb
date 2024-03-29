﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.DataSync.Import.DataTypes;
using PokeOneWeb.DataSync.Import.Interfaces;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.Import
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

            var duplicateHashes = FindDuplicateHashes(sheetHashesOrdered);

            var sheetHashIndex = 0;
            var sheetHashCount = sheetHashesOrdered.Count;
            var dbHashIndex = 0;
            var dbHashCount = dbHashesOrdered.Count;

            while (sheetHashIndex < sheetHashCount || dbHashIndex < dbHashCount)
            {
                var sheetIdHash = sheetHashIndex < sheetHashCount ? sheetHashesOrdered[sheetHashIndex].IdHash : null;
                var sheetContentHash = sheetHashIndex < sheetHashCount ? sheetHashesOrdered[sheetHashIndex].Hash : null;
                var dbIdHash = dbHashIndex < dbHashCount ? dbHashesOrdered[dbHashIndex].IdHash : null;
                var dbContentHash = dbHashIndex < dbHashCount ? dbHashesOrdered[dbHashIndex].Hash : null;

                var contentCmp = CompareIdHashes(sheetIdHash, dbIdHash);

                if (contentCmp == 0)
                {
                    if (!string.Equals(sheetContentHash, dbContentHash, StringComparison.Ordinal))
                    {
                        result.RowsToUpdate.Add(sheetHashesOrdered[sheetHashIndex].IdHash);
                    }

                    sheetHashIndex += 1;
                    dbHashIndex += 1;
                }
                else if (contentCmp > 0) // Sheet Hash > DB Hash
                {
                    result.RowsToInsert.Add(sheetHashesOrdered[sheetHashIndex].IdHash);
                    sheetHashIndex += 1;
                }
                else // Sheet Hash < DB Hash
                {
                    result.RowsToDelete.Add(dbHashesOrdered[dbHashIndex].IdHash);
                    dbHashIndex += 1;
                }
            }

            var sheetIdHashes = sheetHashes.Select(x => x.IdHash).ToList();

            result.RowsToDelete = SortHashesByOriginalOrder(result.RowsToDelete, sheetIdHashes);
            result.RowsToInsert = SortHashesByOriginalOrder(result.RowsToInsert, sheetIdHashes);
            result.RowsToUpdate = SortHashesByOriginalOrder(result.RowsToUpdate, sheetIdHashes);
            result.DuplicateIdHashes = duplicateHashes;

            return result;
        }

        private static int CompareIdHashes(string sheetIdHash, string dbIdHash)
        {
            if (sheetIdHash == null)
            {
                return -1;
            }

            if (dbIdHash == null)
            {
                return 1;
            }

            return string.Compare(sheetIdHash, dbIdHash, StringComparison.Ordinal);
        }

        private static List<string> SortHashesByOriginalOrder(List<string> hashesToSort, List<string> sortedHashes)
        {
            return hashesToSort.OrderBy(sortedHashes.IndexOf).ToList();
        }

        private List<string> FindDuplicateHashes(IList<RowHash> hashes)
        {
            var duplicateHashes = new List<string>();

            var previous = string.Empty;
            for (var i = 0; i < hashes.Count; i++)
            {
                var current = hashes[i].IdHash;
                if (current.EqualsExact(previous))
                {
                    duplicateHashes.Add(current);
                }

                previous = current;
            }

            return duplicateHashes.Distinct().ToList();
        }
    }
}