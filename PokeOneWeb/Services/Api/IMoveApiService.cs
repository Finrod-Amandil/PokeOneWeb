using System.Collections.Generic;
using PokeOneWeb.Dtos;

namespace PokeOneWeb.Services.Api
{
    public interface IMoveApiService
    {
        IEnumerable<MoveDto> GetAllMoves();

        IEnumerable<MoveNameDto> GetAllMoveNames();
    }
}
