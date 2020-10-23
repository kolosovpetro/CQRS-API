﻿using System.Threading.Tasks;
using CqrsApi.Abstractions;
using CqrsApi.Queries.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CqrsApi.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MovieController : Controller, IMovieController
    {
        private readonly IMediator _mediator;

        public MovieController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var request = new GetAllQuery();
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{id}", Name = "GetMovieById")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetByIdQuery(id);
            var response = await _mediator.Send(query);
            return response != null ? (IActionResult) Ok(response) : NotFound();
        }
    }
}