using FraudDetection.Api.Dto;
using FraudDetection.Contracts.Usecases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace FraudDetection.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IPersonUsecase _personUsecase;
        private readonly IDuplicatePersonUsecase _duplicatePersonUsecase;

        public PersonController(ILogger logger, IPersonUsecase personUsecase, IDuplicatePersonUsecase duplicatePersonUsecase)
        {
            _logger = logger;
            _personUsecase = personUsecase;
            _duplicatePersonUsecase = duplicatePersonUsecase;
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] PersonInputDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var person = dto.ToModel();
            var createResult = await _personUsecase.CreateAsync(person);

            if (createResult.IsSuccess)
                return Ok();
            else
            {
                _logger.Error(createResult.Error, "Error creating new person");
                return StatusCode(500, "Error on creating requested Person");
            }
        }

        [HttpPost]
        [Route("similar")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CheckSimilarity([FromBody] PersonSimilarityInputDto similarityCheckInput)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sourcePerson = similarityCheckInput.SourcePerson.ToModel();
            var targetPerson = similarityCheckInput.TargetPerson.ToModel();

            var result = await _duplicatePersonUsecase.FindSimilarityRankAsync(sourcePerson, targetPerson);

            if (result.IsSuccess)
                return Ok(result.Value);
            else
            {
                _logger.Error(result.Error, "Error checking similarity of two given person {sourcePerson}, {targetPerson}", sourcePerson, targetPerson);
                return StatusCode(500, "Error checking similarity of two given person");
            }
        }






    }
}
