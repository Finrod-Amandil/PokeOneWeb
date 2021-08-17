using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Extensions;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MoveLearnMethodLocations
{
    public class MoveLearnMethodLocationMapper : ISpreadsheetEntityMapper<MoveLearnMethodLocationSheetDto, MoveLearnMethodLocation>
    {
        private readonly ILogger<MoveLearnMethodLocationMapper> _logger;

        private IDictionary<string, Location> _locations;
        private IDictionary<string, MoveLearnMethod> _moveLearnMethods;
        private IDictionary<string, Currency> _currencies;

        public MoveLearnMethodLocationMapper(ILogger<MoveLearnMethodLocationMapper> logger)
        {
            _logger = logger;
        }

        public IEnumerable<MoveLearnMethodLocation> Map(IDictionary<RowHash, MoveLearnMethodLocationSheetDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            _locations = new Dictionary<string, Location>();
            _moveLearnMethods = new Dictionary<string, MoveLearnMethod>();
            _currencies = new Dictionary<string, Currency>();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid MoveLearnMethodLocation DTO. Skipping.");
                    continue;
                }

                yield return MapMoveLearnMethodLocation(dto, rowHash);
            }
        }

        public IEnumerable<MoveLearnMethodLocation> MapOnto(IList<MoveLearnMethodLocation> entities, IDictionary<RowHash, MoveLearnMethodLocationSheetDto> dtosWithHashes)
        {
            if (dtosWithHashes is null)
            {
                throw new ArgumentNullException(nameof(dtosWithHashes));
            }

            _locations = new Dictionary<string, Location>();
            _moveLearnMethods = new Dictionary<string, MoveLearnMethod>();
            _currencies = new Dictionary<string, Currency>();

            foreach (var (rowHash, dto) in dtosWithHashes)
            {
                if (!IsValid(dto))
                {
                    _logger.LogWarning($"Found invalid MoveLearnMethodLocation DTO. Skipping.");
                    continue;
                }

                var matchingEntity = entities.SingleOrDefault(x => x.IdHash.EqualsExact(rowHash.IdHash));

                if (matchingEntity is null)
                {
                    _logger.LogWarning($"Found no single matching MoveLearnMethodLocation entity for ID hash {rowHash.IdHash}. Skipping.");
                    continue;
                }

                MapMoveLearnMethodLocation(dto, rowHash, matchingEntity);
            }

            return entities;
        }

        private static bool IsValid(MoveLearnMethodLocationSheetDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.MoveLearnMethodName) &&
                !string.IsNullOrWhiteSpace(dto.LocationName);
        }

        private MoveLearnMethodLocation MapMoveLearnMethodLocation(
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
