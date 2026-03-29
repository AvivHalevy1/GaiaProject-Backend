namespace GaiaProject.Api.Models
{
    public class CalculationResult
    {
        public string Result { get; set; } = string.Empty;
        public List<OperationHistory> LastThreeOperations { get; set; } = new List<OperationHistory>();
        public int CurrentMonthCount { get; set; }

        public CalculationResult(string result, List<OperationHistory> lastThreeOperations, int currentMonthCount)
        {
            Result = result;
            LastThreeOperations = lastThreeOperations;
            CurrentMonthCount = currentMonthCount;
        }
    }
}
