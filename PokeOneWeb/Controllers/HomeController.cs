using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Models;
using PokeOneWeb.Services.GoogleSpreadsheet;
using PokeOneWeb.Services.PokeApi;

namespace PokeOneWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPokeApiService _pokeApiService;
        private readonly IGoogleSpreadsheetService _googleSpreadsheetService;
        private readonly ApplicationDbContext _dbContext;

        public HomeController(
            ILogger<HomeController> logger,
            IPokeApiService pokeApiService,
            IGoogleSpreadsheetService googleSpreadsheetService,
            ApplicationDbContext dbContext)
        {
            _logger = logger;
            _pokeApiService = pokeApiService;
            _googleSpreadsheetService = googleSpreadsheetService;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> GetData()
        {
            await _googleSpreadsheetService.SynchronizeSpreadsheetData();

            
            /*var data = await _pokeApiService.DownloadData();

            _dbContext.LearnableMoveApis.RemoveRange(_dbContext.LearnableMoveApis);
            _dbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('PokeOneWeb.dbo.LearnableMoveApis',RESEED, 0)");
            _dbContext.LearnableMoveApis.AddRange(data.LearnableMoveApis);

            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                e.ToString();
            }*/
            
            return Ok();
        }
    }
}
