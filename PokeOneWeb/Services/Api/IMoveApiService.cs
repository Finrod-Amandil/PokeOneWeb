using System.Collections.Generic;
using PokeOneWeb.WebApi.Dtos;

namespace PokeOneWeb.WebApi.Services.Api
{
    public interface IMoveApiService
    {
        IEnumerable<MoveDto> GetAllMoves();

        IEnumerable<MoveNameDto> GetAllMoveNames();
    }
}
