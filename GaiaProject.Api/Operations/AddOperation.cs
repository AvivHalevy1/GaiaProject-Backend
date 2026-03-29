using GaiaProject.Api.Core;

namespace GaiaProject.Api.Operations
{
    public class AddOperation : IOperation
    {
        public string Name => "Add";

        public string Execute(string fieldA, string fieldB)
        {
            if (double.TryParse(fieldA, out double a) && double.TryParse(fieldB, out double b))
            {
                return (a + b).ToString();
            }
            throw new ArgumentException("Invalid input for Addition. Both fields must be numbers.");
        }
    }
}