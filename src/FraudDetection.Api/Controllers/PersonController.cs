﻿using FraudDetection.Api.Dto;
using FraudDetection.Contracts.Usecases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FraudDetection.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonUsecase _personUsecase;

        public PersonController(ILogger<PersonController> logger, IPersonUsecase personUsecase)
        {
            _logger = logger;
            _personUsecase = personUsecase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]PersonCreateDto dto)
        {
            var person = dto.ToModel();
            var createResult = await _personUsecase.CreateAsync(person);

            if (createResult.IsSuccess)
                return Ok();
            else
            {
                _logger.LogError(createResult.Error, "Error creating new person");
                return StatusCode(500, "Error on creating requested Person");
            }
        }
    }
}