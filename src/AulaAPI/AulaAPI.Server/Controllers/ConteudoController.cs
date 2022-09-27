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
    public class ConteudoController
    {
        private readonly ILogger<ConteudoController> _logger;
        private readonly DatabaseContext _dbContext;

        public ConteudoController(ILogger<ConteudoController> logger, DatabaseContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public ConteudoDTO Get([FromRoute] string id)
        {
            return string.IsNullOrWhiteSpace(id) ? null : new ConteudoService(_dbContext).Get(long.Parse(id));
        }

        [HttpGet]
        public IEnumerable<ConteudoDTO> Get([FromQuery] int pagesize = 50, [FromQuery] int currentpage = 0)
        {
            return new ConteudoService(_dbContext).GetAll(pagesize, currentpage);
        }

        [HttpPost]
        public void Post(ConteudoDTO item)
        {
            new ConteudoService(_dbContext).Add(item);
        }

        [HttpPut]
        public void Put(ConteudoDTO item)
        {
            new ConteudoService(_dbContext).Edit(item);
        }

        [HttpDelete("{id}")]
        public void Delete([FromRoute] string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return;
            }

            new ConteudoService(_dbContext).Delete(long.Parse(id));
        }
    }
}
