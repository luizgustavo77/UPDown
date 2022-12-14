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
    public class AlunoController
    {
        private readonly ILogger<AlunoController> _logger;
        private readonly DatabaseContext _dbContext;

        public AlunoController(ILogger<AlunoController> logger, DatabaseContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public AlunoDTO Get([FromRoute] string id)
        {
            return string.IsNullOrWhiteSpace(id) ? null : new AlunoService(_dbContext).Get(long.Parse(id));
        }

        [HttpGet]
        public IEnumerable<AlunoDTO> Get([FromQuery] int pagesize = 50, [FromQuery] int currentpage = 0)
        {
            return new AlunoService(_dbContext).GetAll(pagesize, currentpage);
        }

        [HttpPost]
        public void Post(AlunoDTO item)
        {
            new AlunoService(_dbContext).Add(item);
        }

        [HttpPut]
        public void Put(AlunoDTO item)
        {
            new AlunoService(_dbContext).Edit(item);
        }

        [HttpDelete("{id}")]
        public void Delete([FromRoute] string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return;
            }

            new AlunoService(_dbContext).Delete(long.Parse(id));
        }
    }
}
