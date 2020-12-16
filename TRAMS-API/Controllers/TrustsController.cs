﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Mapping;
using API.Models.D365;
using API.Models.Response;
using API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TRAMS_API.Controllers
{
    /// <summary>
    /// API controller for retrieving information about a trust from TRAMS
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public class TrustsController : ControllerBase
    {
        private readonly ILogger<TrustsController> _logger;
        private readonly TrustRepository _trustRepostiory;
        private readonly IMapper<GetTrustD365Model, GetTrustsModel> _mapper;

        public TrustsController(ILogger<TrustsController> logger, 
                                         IConfiguration config,
                                         TrustRepository trustRepostiory,
                                         IMapper<GetTrustD365Model, GetTrustsModel> mapper)
        {
            _logger = logger;
            _trustRepostiory = trustRepostiory;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves information about a trust given its TRAMS Guid
        /// </summary>
        /// <param name="id">The GUID</param>
        /// <returns><see cref="GetTrustsModel"/>A GetTrustsModel object</returns>
        [HttpGet]
        [Route("/trusts/{id}")]
        [ProducesResponseType(typeof(GetTrustsModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetTrustsModel>> GetById(Guid id)
        {
            var result = await _trustRepostiory.GetTrustById(id);

            if (result == null)
            {
                return NotFound($"The trust with the id: '{id}' was not found");
            }

            var formattedResult = _mapper.Map(result);
            
            return Ok(formattedResult);
        }

        /// <summary>
        /// Get all trusts or search via query parameters
        /// </summary>
        /// <param name="search">Will search for trusts that contain the search query. The searchable fields are: Name, Companies House Number, and Trust Reference Number</param>
        /// <returns><see cref="GetTrustsModel"/>An array of GetTrustsModel objects</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<GetTrustsModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GetTrustsModel>>> SearchTrusts(string search)
        {
            var results = await _trustRepostiory.SearchTrusts(search);

            var formattedOutput = results.Select(r => _mapper.Map(r)).ToList();

            return Ok(formattedOutput);
        }
    }
}