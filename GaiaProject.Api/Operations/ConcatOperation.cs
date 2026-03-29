using GaiaProject.Api.Core;

namespace GaiaProject.Api.Operations
{
    public class ConcatOperation : IOperation
    {
        public string Name => "Concat";

        public string Execute(string fieldA, string fieldB)
        {
            return $"{fieldA}{fieldB}";
        }
    }
}