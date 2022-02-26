using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.MoveLearnMethodLocations
{
    public class MoveLearnMethodLocationMapper 
        : SpreadsheetEntityMapper<MoveLearnMethodLocationSheetDto, MoveLearnMethodLocation>
    {
        private readonly Dictionary<string, Location> _locations = new();
        private readonly Dictionary<string, MoveLearnMethod> _moveLearnMethods = new();
        private readonly Dictionary<string, Currency> _currencies = new();

        public MoveLearnMethodLocationMapper(ISpreadsheetImportReporter reporter) : base(reporter) { }

        protected override Entity Entity => Entity.MoveLearnMethodLocation;

        protected override bool IsValid(MoveLearnMethodLocationSheetDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.MoveLearnMethodName) &&
                !string.IsNullOrWhiteSpace(dto.LocationName);
        }

        protected override string GetUniqueName(MoveLearnMethodLocationSheetDto dto)
        {
            return null;
        }

        protected override MoveLearnMethodLocation MapEntity(
            MoveLearnMethodLocationSheetDto dto,
            RowHash rowHash,
            MoveLearnMethodLocation moveLearnMethodLocation = null)
        {
            moveLearnMethodLocation ??= new MoveLearnMethodLocation();

            MoveLearnMethod moveLearnMethod;
            if (_moveLearnMethods.ContainsKey(dto.MoveLearnMethodName))
            {
                moveLearnMethod = _moveLearnMethods[dto.MoveLearnMethodName];
            }
            else
            {
                moveLearnMethod = new MoveLearnMethod { Name = dto.MoveLearnMethodName };
                _moveLearnMethods.Add(dto.MoveLearnMethodName, moveLearnMethod);
            }

            Location location;
            if (_locations.ContainsKey(dto.LocationName))
            {
                location = _locations[dto.LocationName];
            }
            else
            {
                location = new Location { Name = dto.LocationName };
                _locations.Add(dto.LocationName, location);
            }

            moveLearnMethodLocation.IdHash = rowHash.IdHash;
            moveLearnMethodLocation.Hash = rowHash.ContentHash;
            moveLearnMethodLocation.ImportSheetId = rowHash.ImportSheetId;
            moveLearnMethodLocation.TutorType = dto.TutorType;
            moveLearnMethodLocation.NpcName = dto.NpcName;
            moveLearnMethodLocation.PlacementDescription = dto.PlacementDescription;
            moveLearnMethodLocation.MoveLearnMethod = moveLearnMethod;
            moveLearnMethodLocation.Location = location;

            moveLearnMethodLocation.Price = new List<MoveLearnMethodLocationPrice>();

            if (dto.PokeDollarPrice > 0)
            {
                moveLearnMethodLocation.Price.Add(GetPrice("Poké Dollar", dto.PokeDollarPrice, moveLearnMethodLocation));
            }

            if (dto.PokeGoldPrice > 0)
            {
                moveLearnMethodLocation.Price.Add(GetPrice("Poké Gold", dto.PokeGoldPrice, moveLearnMethodLocation));
            }

            if (dto.BigMushroomPrice > 0)
            {
                moveLearnMethodLocation.Price.Add(GetPrice("Big Mushroom", dto.BigMushroomPrice, moveLearnMethodLocation));
            }

            if (dto.HeartScalePrice > 0)
            {
                moveLearnMethodLocation.Price.Add(GetPrice("Heart Scale", dto.HeartScalePrice, moveLearnMethodLocation));
            }

            return moveLearnMethodLocation;
        }

        private MoveLearnMethodLocationPrice GetPrice(
            string currencyName, 
            int amount, 
            MoveLearnMethodLocation moveLearnMethodLocation)
        {
            Currency currency;
            if (_currencies.ContainsKey(currencyName))
            {
                currency = _currencies[currencyName];
            }
            else
            {
                currency = new Currency { Item = new Item { Name = currencyName } };
                _currencies.Add(currencyName, currency);
            }

            var price = new MoveLearnMethodLocationPrice
            {
                CurrencyAmount = new CurrencyAmount
                {
                    Currency = currency,
                    Amount = amount
                },
                MoveLearnMethodLocation = moveLearnMethodLocation
            };

            return price;
        }
    }
}
