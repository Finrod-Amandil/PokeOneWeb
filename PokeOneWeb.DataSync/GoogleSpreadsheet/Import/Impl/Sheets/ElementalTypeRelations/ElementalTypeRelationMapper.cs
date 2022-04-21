using System.Collections.Generic;
using PokeOneWeb.Data;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.DataSync.GoogleSpreadsheet.Import.Impl.Sheets.ElementalTypeRelations
{
    public class ElementalTypeRelationMapper : XSpreadsheetEntityMapper<ElementalTypeRelationSheetDto, ElementalTypeRelation>
    {
        private readonly Dictionary<string, ElementalType> _types = new();

        public ElementalTypeRelationMapper(ISpreadsheetImportReporter reporter) : base(reporter)
        {
        }

        protected override Entity Entity => Entity.ElementalTypeRelation;

        protected override bool IsValid(ElementalTypeRelationSheetDto dto)
        {
            return
                !string.IsNullOrWhiteSpace(dto.AttackingTypeName) &&
                !string.IsNullOrWhiteSpace(dto.DefendingTypeName);
        }

        protected override string GetUniqueName(ElementalTypeRelationSheetDto dto)
        {
            return dto.AttackingTypeName + dto.DefendingTypeName;
        }

        protected override ElementalTypeRelation MapEntity(
            ElementalTypeRelationSheetDto dto,
            RowHash rowHash,
            ElementalTypeRelation elementalTypeRelation = null)
        {
            elementalTypeRelation ??= new ElementalTypeRelation();

            ElementalType attackingType;
            if (_types.ContainsKey(dto.AttackingTypeName))
            {
                attackingType = _types[dto.AttackingTypeName];
            }
            else
            {
                attackingType = new ElementalType { Name = dto.AttackingTypeName };
                _types.Add(dto.AttackingTypeName, attackingType);
            }

            ElementalType defendingType;
            if (_types.ContainsKey(dto.DefendingTypeName))
            {
                defendingType = _types[dto.DefendingTypeName];
            }
            else
            {
                defendingType = new ElementalType { Name = dto.DefendingTypeName };
                _types.Add(dto.DefendingTypeName, defendingType);
            }

            elementalTypeRelation.IdHash = rowHash.IdHash;
            elementalTypeRelation.Hash = rowHash.Hash;
            elementalTypeRelation.ImportSheetId = rowHash.ImportSheetId;
            elementalTypeRelation.AttackingType = attackingType;
            elementalTypeRelation.DefendingType = defendingType;
            elementalTypeRelation.AttackEffectivity = dto.Effectivity;

            return elementalTypeRelation;
        }
    }
}