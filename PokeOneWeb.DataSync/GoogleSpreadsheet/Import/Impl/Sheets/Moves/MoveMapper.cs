using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;
using System.Collections.Generic;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.Moves
{
    public class MoveMapper : SpreadsheetEntityMapper<MoveSheetDto, Move>
    {
        private readonly Dictionary<string, MoveDamageClass> _damageClasses = new();
        private readonly Dictionary<string, ElementalType> _types = new();

        public MoveMapper(ISpreadsheetImportReporter reporter) : base(reporter) { }

        protected override Entity Entity => Entity.Move;

        protected override bool IsValid(MoveSheetDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.Name) &&
                !string.IsNullOrWhiteSpace(dto.ResourceName) &&
                !string.IsNullOrWhiteSpace(dto.DamageClassName) &&
                !string.IsNullOrWhiteSpace(dto.TypeName);
        }

        protected override string GetUniqueName(MoveSheetDto dto)
        {
            return dto.Name;
        }

        protected override Move MapEntity(MoveSheetDto dto, RowHash rowHash, Move move = null)
        {
            move ??= new Move();

            MoveDamageClass damageClass;
            if (_damageClasses.ContainsKey(dto.DamageClassName))
            {
                damageClass = _damageClasses[dto.DamageClassName];
            }
            else
            {
                damageClass = new MoveDamageClass { Name = dto.DamageClassName };
                _damageClasses.Add(dto.DamageClassName, damageClass);
            }

            ElementalType type;
            if (_types.ContainsKey(dto.TypeName))
            {
                type = _types[dto.TypeName];
            }
            else
            {
                type = new ElementalType { Name = dto.TypeName };
                _types.Add(dto.TypeName, type);
            }

            move.IdHash = rowHash.IdHash;
            move.Hash = rowHash.ContentHash;
            move.ImportSheetId = rowHash.ImportSheetId;
            move.Name = dto.Name;
            move.DoInclude = dto.DoInclude;
            move.ResourceName = dto.ResourceName;
            move.DamageClass = damageClass;
            move.ElementalType = type;
            move.AttackPower = dto.AttackPower;
            move.Accuracy = dto.Accuracy;
            move.PowerPoints = dto.PowerPoints;
            move.Priority = dto.Priority;
            move.PokeApiName = dto.PokeApiName;
            move.Effect = dto.Effect;

            return move;
        }
    }
}
