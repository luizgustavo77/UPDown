using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using PessoaAPI.Data;
using PessoaAPI.Service;
using System.Collections.Generic;
using UPDown.Common.PessoaAPI;

namespace PessoaAPI.Server.Controllers
{
    // Todo:
    // Melhorar o microserviço para ser mais genérico
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProfessorController
    {
        private readonly ILogger<ProfessorController> _logger;
        private readonly DatabaseContext _dbContext;

        public ProfessorController(ILogger<ProfessorController> logger, DatabaseContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public ProfessorDTO Get([FromRoute] string id)
        {
            return string.IsNullOrWhiteSpace(id) ? null : new ProfessorService(_dbContext).Get(long.Parse(id));
        }

        [HttpGet]
        public IEnumerable<ProfessorDTO> Get([FromQuery] int pagesize = 50, [FromQuery] int currentpage = 0)
        {
            return new ProfessorService(_dbContext).GetAll(pagesize, currentpage);
        }

        [HttpPost]
        public void Post(ProfessorDTO item)
        {
            new ProfessorService(_dbContext).Add(item);
        }

        [HttpPut]
        public void Put(ProfessorDTO item)
        {
            new ProfessorService(_dbContext).Edit(item);
        }

        [HttpDelete("{id}")]
        public void Delete([FromRoute] string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return;
            }

            new ProfessorService(_dbContext).Delete(long.Parse(id));
        }
    }
}
