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

        public MutantController(IMutantService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
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

        [HttpGet]
        [Route("Stats")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Stats()
        {
            try
            {
                var dna = await _service.Stats();
                return Ok(dna);
            }
            catch
            {
                return BadRequest("Error db connection");
            }
        }
    }
}
