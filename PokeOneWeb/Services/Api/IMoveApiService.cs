using PokeOneWeb.WebApi.Dtos;
using System.Collections.Generic;

namespace PokeOneWeb.WebApi.Services.Api
{
    public interface IMoveApiService
    {
        IEnumerable<MoveDto> GetAllMoves();

        IEnumerable<MoveNameDto> GetAllMoveNames();
    }
}
