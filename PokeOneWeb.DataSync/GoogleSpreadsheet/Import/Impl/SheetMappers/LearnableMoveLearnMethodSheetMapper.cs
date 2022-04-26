using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.SheetMappers
{
    public class LearnableMoveLearnMethodSheetMapper : SheetMapper<LearnableMoveLearnMethod>
    {
        public LearnableMoveLearnMethodSheetMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Dictionary<string, Action<LearnableMoveLearnMethod, object>> ValueToEntityMappings => new()
        {
            { "PokemonVariety", (e, v) => e.LearnableMove.PokemonVarietyName = v.ParseAsNonEmptyString() },
            { "Move", (e, v) => e.LearnableMove.MoveName = v.ParseAsNonEmptyString() },
            { "LearnMethod", (e, v) => e.MoveLearnMethod.Name = v.ParseAsNonEmptyString() },
            { "IsAvailable", (e, v) => e.IsAvailable = v.ParseAsBoolean() },
            { "LevelLearnedAt", (e, v) => e.LevelLearnedAt = v.ParseAsOptionalInt() },
            { "RequiredItem", (e, v) => e.RequiredItemName = v.ParseAsOptionalString() },
            { "TutorName", (e, v) => e.MoveTutorName = v.ParseAsOptionalString() },
            { "Comments", (e, v) => e.Comments = v.ParseAsOptionalString() },
        };
    }
}