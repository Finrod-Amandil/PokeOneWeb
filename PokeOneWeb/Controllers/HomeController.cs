using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Models;
using PokeOneWeb.Services.GoogleSpreadsheet.Import;
using PokeOneWeb.Services.PokeApi;
using PokeOneWeb.Services.ReadModelUpdate;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PokeOneWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPokeApiService _pokeApiService;
        private readonly IGoogleSpreadsheetImportService _googleSpreadsheetImportService;
        private readonly IReadModelUpdateService _readModelUpdateService;
        private readonly ApplicationDbContext _dbContext;

        public HomeController(
            ILogger<HomeController> logger,
            IPokeApiService pokeApiService,
            IGoogleSpreadsheetImportService googleSpreadsheetImportService,
            IReadModelUpdateService readModelUpdateService,
            ApplicationDbContext dbContext)
        {
            _logger = logger;
            _pokeApiService = pokeApiService;
            _googleSpreadsheetImportService = googleSpreadsheetImportService;
            _readModelUpdateService = readModelUpdateService;
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
            var importReport = await _googleSpreadsheetImportService.ImportSpreadsheetData();

            if (true /*|| totalChangedEntries > 0*/)
            {
                await Task.Run(() => _readModelUpdateService.UpdateReadModel(importReport));
            }

            return Ok();
        }
    }
}
