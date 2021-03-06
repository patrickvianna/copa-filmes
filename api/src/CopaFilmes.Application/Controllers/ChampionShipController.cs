﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CopaFilmes.Application.Business;
using CopaFilmes.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CopaFilmes.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChampionShipController : ControllerBase
    {
        // POST: api/ChampionShip/GenerateChampionship
        [Route("GenerateChampionship")]
        [HttpPost]
        public async Task<ActionResult<List<Movie>>> Post([FromBody] List<Movie> movies)
        {
            ChampionshipBus championshipBus = new ChampionshipBus();

            var rankedMovies = await championshipBus.RankedList(movies);

            return rankedMovies;

        }
    }
}
