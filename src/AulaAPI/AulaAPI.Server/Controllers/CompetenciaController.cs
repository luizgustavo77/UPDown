using AulaAPI.Data;
using AulaAPI.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using UPDown.Common.AulaAPI;

namespace AulaAPI.Server.Controllers
{
    // Todo:
    // Melhorar o microserviço para ser mais genérico

    [ApiController]
    [Route("[controller]")]
    public class CompetenciaController
    {
        private readonly ILogger<CompetenciaController> _logger;
        private readonly DatabaseContext _dbContext;

        public CompetenciaController(ILogger<CompetenciaController> logger, DatabaseContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public CompetenciaDTO Get([FromRoute] string id)
        {
            return string.IsNullOrWhiteSpace(id) ? null : new CompetenciaService(_dbContext).Get(long.Parse(id));
        }

        [HttpGet]
        public IEnumerable<CompetenciaDTO> Get([FromQuery] int pagesize = 50, [FromQuery] int currentpage = 0)
        {
            return new CompetenciaService(_dbContext).GetAll(pagesize, currentpage);
        }

        [HttpPost]
        public void Post(CompetenciaDTO item)
        {
            new CompetenciaService(_dbContext).Add(item);
        }

        [HttpPut]
        public void Put(CompetenciaDTO item)
        {
            new CompetenciaService(_dbContext).Edit(item);
        }

        [HttpDelete("{id}")]
        public void Delete([FromRoute] string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return;
            }

            new CompetenciaService(_dbContext).Delete(long.Parse(id));
        }
    }
}
