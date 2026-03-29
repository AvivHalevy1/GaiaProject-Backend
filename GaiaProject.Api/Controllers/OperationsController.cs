using GaiaProject.Api.Core;
using GaiaProject.Api.Data;
using GaiaProject.Api.Models;
using GaiaProject.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GaiaProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OperationsController : ControllerBase
    {
        private readonly IOperationFactory _operationFactory;
        private readonly ICalculatorService _calculatorService;

        public OperationsController(IOperationFactory operationFactory, ICalculatorService calculatorService)
        {
            _operationFactory = operationFactory;
            _calculatorService = calculatorService;
        }

        /// <summary>
        /// Retrieves a list of all supported operation names.
        /// </summary>
        /// <returns>A list of operation names.</returns>
        [HttpGet("list")]
        public IActionResult GetOperations()
        {
            var operations = _operationFactory.GetAllOperations()
                .Select(o => o.Name);
            return Ok(operations);
        }

        /// <summary>
        /// Executes a specific calculation based on the provided request.
        /// </summary>
        /// <param name="request">The calculation request containing the operation name and fields.</param>
        /// <returns>The result of the calculation along with historical statistics.</returns>
        /// <response code="200">Returns the calculation result.</response>
        /// <response code="400">If the input parameters are invalid for the chosen operation.</response>
        /// <response code="404">If the specified operation name does not exist.</response>
        /// <response code="500">If an unexpected server error occurs.</response>
        [HttpPost("calculate")]
        public async Task<IActionResult> Calculate([FromBody] CalculationRequest request)
        {
            try
            {
                var result = await _calculatorService.Calculate(
                    request.OperationName, request.FieldA, request.FieldB);

                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (DivideByZeroException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {                
                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again later." });
            }
        }
    }
}
