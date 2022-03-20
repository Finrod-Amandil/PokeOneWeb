namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.MoveTutorMoves
{
    public class MoveTutorMoveSheetRowParser : SheetRowParser<MoveTutorMoveSheetDto>
    {
        protected override int RequiredValueCount => 2;

        protected override List<Action<MoveTutorMoveSheetDto, object>> MappingDelegates => new()
        {
            (dto, value) => dto.MoveTutorName = ParseAsNonEmptyString(value),
            (dto, value) => dto.MoveName = ParseAsNonEmptyString(value),
            (dto, value) => dto.RedShardPrice = ParseAsInt(value),
            (dto, value) => dto.BlueShardPrice = ParseAsInt(value),
            (dto, value) => dto.GreenShardPrice = ParseAsInt(value),
            (dto, value) => dto.YellowShardPrice = ParseAsInt(value),
            (dto, value) => dto.PWTBPPrice = ParseAsInt(value),
            (dto, value) => dto.BFBPPrice = ParseAsInt(value),
            (dto, value) => dto.PokeDollarPrice = ParseAsInt(value),
            (dto, value) => dto.PokeGoldPrice = ParseAsInt(value)
        };
    }
}
