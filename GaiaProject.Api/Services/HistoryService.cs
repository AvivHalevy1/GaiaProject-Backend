using GaiaProject.Api.Data;
using GaiaProject.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GaiaProject.Api.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly GaiaDbContext _dbContext;

        public HistoryService(GaiaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Saves the executed operation details to the database.
        /// </summary>
        /// <param name="operationName">The name of the executed operation.</param>
        /// <param name="fieldA">The first input value used.</param>
        /// <param name="fieldB">The second input value used.</param>
        /// <param name="result">The calculated result.</param>
        /// <returns>A task that represents the asynchronous save operation.</returns>
        public async Task SaveOperationAsync(string operationName, string fieldA, string fieldB, string result)
        {
            var record = new OperationHistory
            {
                OperationName = operationName,
                FieldA = fieldA,
                FieldB = fieldB,
                Result = result,
                ExecutionDate = DateTime.UtcNow
            };

            _dbContext.OperationHistories.Add(record);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves the last 3 history records for a specific operation.
        /// </summary>
        /// <param name="operationName">The name of the operation to filter by.</param>
        /// <returns>A list of up to 3 most recent OperationHistory records.</returns>
        public async Task<List<OperationHistory>> GetLastThreeOperationsAsync(string operationName)
        {
            return await _dbContext.OperationHistories
                .Where(o => o.OperationName == operationName)
                .OrderByDescending(o => o.ExecutionDate)
                .Take(3)
                .ToListAsync();
        }

        /// <summary>
        /// Gets the total number of times a specific operation was executed in the current month.
        /// </summary>
        /// <param name="operationName">The name of the operation to count.</param>
        /// <returns>The count of executions for the current month.</returns>
        public async Task<int> GetMonthlyOperationCountAsync(string operationName)
        {
            var currentMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);

            return await _dbContext.OperationHistories
                .CountAsync(o => o.OperationName == operationName && o.ExecutionDate >= currentMonth);
        }
    }
    public interface IHistoryService
    {
        Task SaveOperationAsync(string operationName, string fieldA, string fieldB, string result);
        Task<List<OperationHistory>> GetLastThreeOperationsAsync(string operationName);
        Task<int> GetMonthlyOperationCountAsync(string operationName);
    }
}