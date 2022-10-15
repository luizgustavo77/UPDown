using AulaAPI.Data;
using AulaAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using UPDown.Common.AulaAPI;

namespace AulaAPI.Server.Controllers
{
    // Todo:
    // Melhorar o microserviço para ser mais genérico
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MateriaController
    {
        private readonly ILogger<MateriaController> _logger;
        private readonly DatabaseContext _dbContext;

        public MateriaController(ILogger<MateriaController> logger, DatabaseContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public MateriaDTO Get([FromRoute] string id)
        {
            return string.IsNullOrWhiteSpace(id) ? null : new MateriaService(_dbContext).Get(long.Parse(id));
        }

        [HttpGet]
        public IEnumerable<MateriaDTO> Get([FromQuery] int pagesize = 50, [FromQuery] int currentpage = 0)
        {
            return new MateriaService(_dbContext).GetAll(pagesize, currentpage);
        }

        [HttpPost]
        public void Post(MateriaDTO item)
        {
            new MateriaService(_dbContext).Add(item);
        }

        [HttpPut]
        public void Put(MateriaDTO item)
        {
            new MateriaService(_dbContext).Edit(item);
        }

        [HttpDelete("{id}")]
        public void Delete([FromRoute] string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return;
            }

            new MateriaService(_dbContext).Delete(long.Parse(id));
        }
    }
}
