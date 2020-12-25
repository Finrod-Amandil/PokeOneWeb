using System.Collections.Generic;
using System.Linq;
using PokeOneWeb.Data.Entities;

namespace PokeOneWeb.Services.GoogleSpreadsheet.Import.Impl.MainData.TutorMoves
{
    public class TutorMoveMapper : ISpreadsheetEntityMapper<TutorMoveDto, TutorMove>
    {
        public IEnumerable<TutorMove> Map(IEnumerable<TutorMoveDto> dtos)
        {
            return dtos.Select(MapTutorMove);
        }

        private TutorMove MapTutorMove(TutorMoveDto dto)
        {
            return new TutorMove
            {
                TutorType = dto.TutorType,
                TutorName = dto.TutorName,
                LocationName = dto.LocationName,
                PlacementDescription = dto.PlacementDescription,
                MoveName = dto.MoveName,
                RedShardPrice = dto.RedShardPrice,
                BlueShardPrice = dto.BlueShardPrice,
                GreenShardPrice = dto.GreenShardPrice,
                YellowShardPrice = dto.YellowShardPrice,
                PWTBPPrice = dto.PWTBPPrice,
                BFBPPrice = dto.BFBPPrice,
                PokeDollarPrice = dto.PokeDollarPrice,
                PokeGoldPrice = dto.PokeGoldPrice,
                BigMushrooms = dto.BigMushrooms,
                HeartScales = dto.HeartScales,
            };
        }
    }
}
