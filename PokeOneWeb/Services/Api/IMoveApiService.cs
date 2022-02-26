using System.Collections.Generic;
using PokeOneWeb.Dtos;
using PokeOneWeb.WebApi.Dtos;

namespace PokeOneWeb.Services.Api
{
    public interface IMoveApiService
    {
        IEnumerable<MoveDto> GetAllMoves();

        IEnumerable<MoveNameDto> GetAllMoveNames();
    }
}
