using GaiaProject.Api.Data;
using GaiaProject.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GaiaProject.Api.Services
{
    public class CalculatorService : ICalculatorService
    {
        private readonly IOperationFactory _operationFactory;
        private readonly IHistoryService _historyService;

        public CalculatorService(IOperationFactory operationFactory, IHistoryService historyService)
        {
            _operationFactory = operationFactory;
            _historyService = historyService;
        }


        /// <summary>
        /// Executes the requested operation, saves it to history, and retrieves operation statistics.
        /// </summary>
        /// <param name="operationName">The name of the operation (e.g., "Add").</param>
        /// <param name="fieldA">The first input value.</param>
        /// <param name="fieldB">The second input value.</param>
        /// <returns>A CalculationResult containing the result, last 3 operations, and current month count.</returns>
        public async Task<CalculationResult> Calculate(string operationName, string fieldA, string fieldB)
        {
            var operation = _operationFactory.GetOperation(operationName);
            var result = operation.Execute(fieldA, fieldB);

            // Saving history
            await _historyService.SaveOperationAsync(operationName, fieldA, fieldB, result);

            // Retrieve last 3 records for the current operation
            var lastThree = await _historyService.GetLastThreeOperationsAsync(operationName);

            // Retrieve the amount of actions using the current opertaion for the current month
            var monthCount = await _historyService.GetMonthlyOperationCountAsync(operationName);

            return new CalculationResult(result, lastThree, monthCount);
        }
    }

    public interface ICalculatorService
    {
        Task<CalculationResult> Calculate(string operationName, string fieldA, string fieldB);
    }
}
