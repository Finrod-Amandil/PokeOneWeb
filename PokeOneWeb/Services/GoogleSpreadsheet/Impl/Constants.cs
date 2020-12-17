using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Impl
{
    public class Constants
    {
        public static readonly string SPREADSHEET_ID = "1MDHewtqu5ABMW1oi7SEIXwxsZ_gtooDsA4f6l85R3w8";

        public static readonly IEnumerable<string> LEARNABLE_MOVES_SPREADSHEET_IDS = new[]
        {
            "1V9MnS7U8YQwEja4QI59_-DV5zG0JJXrl6If7zR6PMYE",
            "1lrK8DVf8LNV0wsG-SQLzURgpW8a1B4CTFa_8gtS9tys",
        };
        public static readonly string SHEET_PREFIX_LOCATIONS = "locations";
        public static readonly string SHEET_PREFIX_SPAWNS = "spawns";
        public static readonly string SHEET_PREFIX_PLACEDITEMS = "placeditems";
        public static readonly string SHEET_PREFIX_TUTOR_MOVES = "tutormoves";
        public static readonly string SHEET_PREFIX_LEARNABLE_MOVES = "learnablemoves";
    }
}
