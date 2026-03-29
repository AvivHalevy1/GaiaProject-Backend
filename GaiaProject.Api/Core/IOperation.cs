namespace GaiaProject.Api.Core
{
    public interface IOperation
    {
        string Name { get; }
        string Execute(string fieldA, string fieldB);
    }
}