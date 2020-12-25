using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PokeOneWeb.Data;
using PokeOneWeb.Models;
using PokeOneWeb.Services.GoogleSpreadsheet;
using PokeOneWeb.Services.PokeApi;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Services.GoogleSpreadsheet.Import;

namespace PokeOneWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPokeApiService _pokeApiService;
        private readonly IGoogleSpreadsheetImportService _googleSpreadsheetImportService;
        private readonly ApplicationDbContext _dbContext;

        public HomeController(
            ILogger<HomeController> logger,
            IPokeApiService pokeApiService,
            IGoogleSpreadsheetImportService googleSpreadsheetImportService,
            ApplicationDbContext dbContext)
        {
            _logger = logger;
            _pokeApiService = pokeApiService;
            _googleSpreadsheetImportService = googleSpreadsheetImportService;
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
            await _googleSpreadsheetImportService.ImportSpreadsheetData();

            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                e.ToString();
            }
            
            return Ok();
        }
    }
}
