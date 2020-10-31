﻿using System;
using System.Collections.Generic;
using System.Linq;
using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Services.GoogleSpreadsheet.Impl.TutorMoves;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Impl.LearnableMoveLearnMethods
{
    public class LearnableMoveLearnMethodReader : SpreadsheetReader<LearnableMoveLearnMethodDto>
    {
        private readonly ISpreadsheetReader<TutorMoveDto> _tutorMoveReader;

        public LearnableMoveLearnMethodReader(ILogger<LearnableMoveLearnMethodReader> logger, ISpreadsheetReader<TutorMoveDto> tutorMoveReader) : base(logger)
        {
            _tutorMoveReader = tutorMoveReader;
        }

        public override IEnumerable<LearnableMoveLearnMethodDto> Read(Spreadsheet spreadsheet, string sheetPrefix)
        {
            var dtos = base.Read(spreadsheet, sheetPrefix).ToList();

            dtos = AddPricesToMoves(dtos, spreadsheet);

            return dtos;
        }

        protected override LearnableMoveLearnMethodDto ReadRow(RowData rowData)
        {
            if (rowData is null || rowData.Values.Count < 5)
            {
                throw new InvalidRowDataException("Row data does not contain sufficient values.");
            }

            var value = new LearnableMoveLearnMethodDto {
                PokemonVarietyName = rowData.Values[1]?.EffectiveValue?.StringValue,
                MoveName = rowData.Values[2]?.EffectiveValue?.StringValue,
                IsAvailable = rowData.Values[4]?.EffectiveValue?.BoolValue ?? false
            };

            if (string.IsNullOrEmpty(value.PokemonVarietyName))
            {
                throw new InvalidRowDataException(
                    $"Tried to read LearnableMoveLearnMethod, but required " +
                    $"field {nameof(value.PokemonVarietyName)} was empty.");
            }

            if (string.IsNullOrEmpty(value.MoveName))
            {
                throw new InvalidRowDataException(
                    $"Tried to read LearnableMoveLearnMethod, but required " +
                    $"field {nameof(value.MoveName)} was empty.");
            }

            switch (rowData.Values[3]?.EffectiveValue?.StringValue)
            {
                case LearnableMoveConstants.LearnMethodType.EGG:
                    value.LearnMethod = LearnMethod.EggMove;
                    break;

                case LearnableMoveConstants.LearnMethodType.LEVELUP:
                    value.LearnMethod = LearnMethod.LevelUp;

                    if (rowData.Values.Count > 6)
                    {
                        value.LevelLearnedAt = (int) (rowData.Values[6]?.EffectiveValue?.NumberValue ?? 0);
                    }

                    if (value.LevelLearnedAt is null || value.LevelLearnedAt == 0)
                    {
                        throw new InvalidRowDataException(
                            $"Tried to read LearnableMoveLearnMethod of type {LearnableMoveConstants.LearnMethodType.LEVELUP}, but required " +
                            $"field {nameof(value.LevelLearnedAt)} was empty.");
                    }

                    break;

                case LearnableMoveConstants.LearnMethodType.PREEVOLUTION:
                    value.LearnMethod = LearnMethod.PreEvolutionMove;
                    break;

                case LearnableMoveConstants.LearnMethodType.MACHINE:
                    value.LearnMethod = LearnMethod.Machine;

                    if (rowData.Values.Count > 7)
                    {
                        value.RequiredItemName = rowData.Values[7]?.EffectiveValue?.StringValue;
                    }

                    break;

                case LearnableMoveConstants.LearnMethodType.TUTOR:
                    value.LearnMethod = LearnMethod.Tutor;

                    if (rowData.Values.Count > 8)
                    {
                        value.TutorName = rowData.Values[8]?.EffectiveValue?.StringValue;
                    }

                    if (rowData.Values.Count > 9)
                    {
                        value.TutorLocation = rowData.Values[9]?.EffectiveValue?.StringValue;
                    }

                    break;

                default:
                    throw new InvalidRowDataException(
                        $"Tried to read LearnableMoveLearnMethod, but required " +
                        $"field {nameof(value.LearnMethod)} could not be parsed.");
            }

            if (rowData.Values.Count > 10)
            {
                value.Comments = rowData.Values[10]?.EffectiveValue?.StringValue;
            }

            return value;
        }

        private List<LearnableMoveLearnMethodDto> AddPricesToMoves(List<LearnableMoveLearnMethodDto> dtos, Spreadsheet spreadsheet)
        {
            var tutorMoves = _tutorMoveReader.Read(spreadsheet, Constants.SHEET_PREFIX_TUTOR_MOVES).ToList();

            foreach (var dto in dtos)
            {
                if (!dto.IsAvailable)
                {
                    continue;
                }

                var matchingTutorMoves = new List<TutorMoveDto>();

                switch (dto.LearnMethod)
                {
                    case LearnMethod.LevelUp:
                        matchingTutorMoves = tutorMoves.Where(tm => tm.TutorType.Equals(LearnableMoveConstants.TutorName.LEVELUP)).ToList();
                        break;

                    case LearnMethod.EggMove:
                        matchingTutorMoves = tutorMoves.Where(tm => tm.TutorType.Equals(LearnableMoveConstants.TutorName.EGG)).ToList();
                        break;

                    case LearnMethod.Tutor:
                        matchingTutorMoves = tutorMoves.Where(tm =>
                            tm.MoveName != null &&
                            tm.MoveName.Equals(dto.MoveName) &&
                            tm.TutorName.Equals(dto.TutorName) &&
                            tm.LocationName.Equals(dto.TutorLocation)).ToList();
                        break;

                    case LearnMethod.Machine:
                        continue; //Machine does not need a tutor / has no price

                    case LearnMethod.PreEvolutionMove:
                        matchingTutorMoves = tutorMoves.Where(tm => tm.TutorType.Equals(LearnableMoveConstants.TutorName.PREEVOLUTION)).ToList();
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                dto.TutorMoveDtos = matchingTutorMoves;
            }

            return dtos;
        }
    }
}
