﻿using System.Collections.Generic;
using PokeOneWeb.Dtos;

namespace PokeOneWeb.Services.Api
{
    public interface IItemApiService
    {
        IEnumerable<ItemStatBoostPokemonDto> GetItemStatBoostsForPokemon(string pokemonVarietyResourceName);
    }
}
