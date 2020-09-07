using PokeOneWeb.Services.PokeApi.Impl;
using System.Threading.Tasks;

namespace PokeOneWeb.Services.PokeApi
{
    public interface IPokeApiService
    {
        Task<MappedPokeApiData> DownloadData();
    }
}
