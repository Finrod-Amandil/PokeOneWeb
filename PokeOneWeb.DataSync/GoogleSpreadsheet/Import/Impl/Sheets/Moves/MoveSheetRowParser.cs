using System;
using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Moves
{
    public class MoveSheetRowParser : SheetRowParser<MoveSheetDto>
    {
        protected override int RequiredValueCount => 9;

        protected override List<Action<MoveSheetDto, object>> MappingDelegates => new()
        {
            (dto, value) => dto.Name = ParseAsNonEmptyString(value),
            (dto, value) => dto.DoInclude = ParseAsBoolean(value),
            (dto, value) => dto.ResourceName = ParseAsNonEmptyString(value),
            (dto, value) => dto.DamageClassName = ParseAsNonEmptyString(value),
            (dto, value) => dto.TypeName = ParseAsNonEmptyString(value),
            (dto, value) => dto.AttackPower = ParseAsInt(value),
            (dto, value) => dto.Accuracy = ParseAsInt(value),
            (dto, value) => dto.PowerPoints = ParseAsInt(value),
            (dto, value) => dto.Priority = ParseAsInt(value),
            (dto, value) => dto.Effect = ParseAsString(value),
        };
    }
}
