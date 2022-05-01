﻿using System;
using System.Collections.Generic;
using System.Linq;
using PokeOneWeb.Data.Entities;
using PokeOneWeb.Shared.Extensions;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.SheetMappers
{
    public class BuildSheetMapper : SheetMapper<Build>
    {
        private const string OptionDivider = "/";

        private const string StatAtk = "atk";
        private const string StatSpa = "spa";
        private const string StatDef = "def";
        private const string StatSpd = "spd";
        private const string StatSpe = "spe";
        private const string StatHp = "hp";

        private const int MaxEvsAvailable = 510;

        public BuildSheetMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Dictionary<string, Action<Build, object>> ValueToEntityMappings => new()
        {
            { "PokemonVarietyName", (e, v) => e.PokemonVarietyName = v.ParseAsNonEmptyString() },
            { "Name", (e, v) => e.Name = v.ParseAsNonEmptyString() },
            { "Description", (e, v) => e.Description = v.ParseAsString() },
            { "Move1Options", (e, v) => e.MoveOptions.AddRange(MapMoves(v.ParseAsString(), 1)) },
            { "Move2Options", (e, v) => e.MoveOptions.AddRange(MapMoves(v.ParseAsString(), 2)) },
            { "Move3Options", (e, v) => e.MoveOptions.AddRange(MapMoves(v.ParseAsString(), 3)) },
            { "Move4Options", (e, v) => e.MoveOptions.AddRange(MapMoves(v.ParseAsString(), 4)) },
            { "ItemOptions", (e, v) => e.ItemOptions.AddRange(MapItems(v.ParseAsString())) },
            { "NatureOptions", (e, v) => e.NatureOptions.AddRange(MapNatures(v.ParseAsString())) },
            { "Ability", (e, v) => e.AbilityName = v.ParseAsNonEmptyString() },
            { "EvDistribution", (e, v) => AddEvDistribution(e, v.ParseAsString()) },
        };

        private IEnumerable<MoveOption> MapMoves(string moveString, int slot)
        {
            return moveString.Split(OptionDivider)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => new MoveOption
                {
                    MoveName = x.Trim(),
                    Slot = slot
                });
        }

        private IEnumerable<ItemOption> MapItems(string itemString)
        {
            return itemString.Split(OptionDivider)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => new ItemOption
                {
                    ItemName = x.Trim()
                });
        }

        private IEnumerable<NatureOption> MapNatures(string natureString)
        {
            return natureString.Split(OptionDivider)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => new NatureOption
                {
                    NatureName = x.Trim()
                });
        }

        private void AddEvDistribution(Build build, string evDistributionString)
        {
            build.AttackEv = GetEvValue(StatAtk, evDistributionString, build.IdHash);
            build.SpecialAttackEv = GetEvValue(StatSpa, evDistributionString, build.IdHash);
            build.DefenseEv = GetEvValue(StatDef, evDistributionString, build.IdHash);
            build.SpecialDefenseEv = GetEvValue(StatSpd, evDistributionString, build.IdHash);
            build.SpeedEv = GetEvValue(StatSpe, evDistributionString, build.IdHash);
            build.HitPointsEv = GetEvValue(StatHp, evDistributionString, build.IdHash);

            if (build.AttackEv + build.SpecialAttackEv + build.DefenseEv +
                build.SpecialDefenseEv + build.SpeedEv + build.HitPointsEv > MaxEvsAvailable)
            {
                Reporter.ReportError(nameof(Build), build.IdHash,
                    $"The EV values given amounted to a total value higher than the allowed " +
                    $"maximum of {MaxEvsAvailable}: {evDistributionString}");
            }
        }

        private int GetEvValue(string statName, string evDistributionString, string idHash)
        {
            var evParts = evDistributionString
                .Split(OptionDivider)
                .Where(part => part.Contains(statName))
                .ToList();

            if (!evParts.Any())
            {
                return 0;
            }

            if (evParts.Count > 1)
            {
                Reporter.ReportError(nameof(Build), idHash,
                    $"For stat {statName} more than one value was given: {evDistributionString}");
                return 0;
            }

            var evPart = evParts[0];
            var valueString = evPart.Replace(statName, string.Empty).Trim();

            if (!int.TryParse(valueString, out var value))
            {
                Reporter.ReportError(nameof(Build), idHash,
                    $"EV-Distribution value for {statName} could not be parsed: {evPart}");
                return 0;
            }

            return value;
        }
    }
}