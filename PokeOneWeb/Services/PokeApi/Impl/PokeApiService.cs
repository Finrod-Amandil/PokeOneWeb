using Microsoft.Extensions.Options;
using PokeOneWeb.Configuration;
using System.Threading.Tasks;

namespace PokeOneWeb.Services.PokeApi.Impl
{
    public class PokeApiService : IPokeApiService
    {
        private readonly PokeApiLoader _loader;
        private readonly PokeApiMapper _mapper;

        public PokeApiService(IOptions<PokeApiSettings> pokeApiSettings)
        {
            _loader = new PokeApiLoader(pokeApiSettings);
            _mapper = new PokeApiMapper(pokeApiSettings);
        }

        public async Task<MappedPokeApiData> DownloadData()
        {
            var data = await _loader.LoadPokeApiData();

            var mappedData = _mapper.MapPokeApiData(data);

            return mappedData;
        }
    }
}
