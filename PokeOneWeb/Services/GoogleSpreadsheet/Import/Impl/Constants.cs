using System.Collections.Generic;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl
{
    public class Constants
    {
        public static readonly string MAIN_SPREADSHEET_ID = "1MDHewtqu5ABMW1oi7SEIXwxsZ_gtooDsA4f6l85R3w8";
        public static readonly string MASTER_SPREADSHEET_ID = "12xw_oAguMofict4WdnPpnZzsP2MJDZ8hw0C7yQNId60";

        public static readonly IEnumerable<string> LEARNABLE_MOVES_SPREADSHEET_IDS = new[]
        {
            "1V9MnS7U8YQwEja4QI59_-DV5zG0JJXrl6If7zR6PMYE",
            "1lrK8DVf8LNV0wsG-SQLzURgpW8a1B4CTFa_8gtS9tys",
        };

        //Master data
        public static readonly string SHEET_PREFIX_ITEMS = "items";
        public static readonly string SHEET_PREFIX_ABILITIES = "abilities";
        public static readonly string SHEET_PREFIX_ELEMENTAL_TYPES = "elemental_types";
        public static readonly string SHEET_PREFIX_ELEMENTAL_TYPE_RELATIONS = "elemental_type_relations";
        public static readonly string SHEET_PREFIX_MOVES = "moves";
        public static readonly string SHEET_PREFIX_NATURES = "natures";
        public static readonly string SHEET_PREFIX_TIMES = "times";
        public static readonly string SHEET_PREFIX_CURRENCIES = "currencies";
        public static readonly string SHEET_PREFIX_AVAILABILITIES = "availabilities";
        public static readonly string SHEET_PREFIX_PVP_TIERS = "pvp_tiers";
        public static readonly string SHEET_PREFIX_POKEMON = "pokemon";
        public static readonly string SHEET_PREFIX_EVOLUTIONS = "evolutions";
        public static readonly string SHEET_PREFIX_SPAWN_TYPES = "spawn_types";

        //Main data
        public static readonly string SHEET_PREFIX_REGIONS = "regions";
        public static readonly string SHEET_PREFIX_LOCATIONS = "locations";
        public static readonly string SHEET_PREFIX_SPAWNS = "spawns";
        public static readonly string SHEET_PREFIX_PLACEDITEMS = "placeditems";
        public static readonly string SHEET_PREFIX_TUTOR_MOVES = "tutormoves";
        public static readonly string SHEET_PREFIX_LEARNABLE_MOVES = "learnablemoves";
        public static readonly string SHEET_PREFIX_BUILDS = "builds";
        public static readonly string SHEET_PREFIX_HUNTINGCONFIGURATIONS = "huntingconfigurations";
    }
}
