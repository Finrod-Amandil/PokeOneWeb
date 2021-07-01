using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PokeOneWeb.Controllers.Dtos;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;

namespace PokeOneWeb.Controllers
{
    [ApiController]
    public class MoveController : ControllerBase
    {
        private readonly ReadModelDbContext _dbContext;

        public MoveController(ReadModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("api/move/getall")]
        [HttpGet]
        public ActionResult<List<MoveReadModel>> GetAll()
        {
            return _dbContext.MoveReadModels.ToList();
        }

        [Route("api/move/getallnames")]
        [HttpGet]
        public ActionResult<List<MoveNameDto>> GetAllNames()
        {
            return _dbContext.MoveReadModels
                .ToList()
                .Select(ToMoveName)
                .ToList();
        }

        private static MoveNameDto ToMoveName(MoveReadModel readModel)
        {
            return new MoveNameDto
            {
                Name = readModel.Name
            };
        }
    }
}
