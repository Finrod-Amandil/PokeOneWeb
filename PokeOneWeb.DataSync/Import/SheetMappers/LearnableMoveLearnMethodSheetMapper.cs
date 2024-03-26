using System;
using System.Collections.Generic;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.DataSync.Import.Interfaces;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.Import.SheetMappers
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
            { "RequiredItem", (e, v) => e.RequiredItemName = v.ParseAsString() },
            { "TutorName", (e, v) => e.MoveTutorName = v.ParseAsString() },
            { "Comments", (e, v) => e.Comments = v.ParseAsString() },
        };
    }
}