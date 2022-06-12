using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MutantRecruiter.Services.Contract;
using MutantRecruiter.Services.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MutantRecruiter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MutantController : ControllerBase
    {
        private readonly IMutantService _service;
        private readonly ICosmosDBService<Human> _cosmosDBService;

        public MutantController(IMutantService service,ICosmosDBService<Human> cosmosDBService)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _cosmosDBService = cosmosDBService ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpPost]
        [Route("IsMutant")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> IsMutant(Human human)
        {
            try
            {
                if (await _service.IsMutant(human))
                    return Ok("Is mutant");
                return StatusCode(403,"Is not a mutant");
            }
            catch
            {
                return BadRequest("he's not a human, he's a alien");
            }
        }

        [HttpPost]
        [Route("Stats")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Stats(Human human)
        {
            try
            {
                var dna = await _cosmosDBService.GetByQuery(JsonConvert.SerializeObject(human.DNA));
                return Ok();
            }
            catch
            {
                return BadRequest("Error db connection");
            }
        }
    }
}
