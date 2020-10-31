using Microsoft.Extensions.Options;
using PokeOneWeb.Configuration;
using System.Threading.Tasks;
using PokeOneWeb.Data;

namespace PokeOneWeb.Services.PokeApi.Impl
{
    public class PokeApiService : IPokeApiService
    {
        private readonly PokeApiLoader _loader;
        private readonly PokeApiMapper _mapper;

        public PokeApiService(IOptions<PokeApiSettings> pokeApiSettings, ApplicationDbContext dbContext)
        {
            _loader = new PokeApiLoader(pokeApiSettings);
            _mapper = new PokeApiMapper(pokeApiSettings, dbContext);
        }

        public async Task<MappedPokeApiData> DownloadData()
        {
            var data = await _loader.LoadPokeApiData();

            var mappedData = _mapper.MapPokeApiData(data);

            return mappedData;
        }
    }
}
