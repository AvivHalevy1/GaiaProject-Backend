using System.Reflection;
using GaiaProject.Api.Core;

namespace GaiaProject.Api.Services
{
    public class OperationFactory : IOperationFactory
    {
        
        private readonly Dictionary<string, IOperation> _operations;

        public OperationFactory()
        {
            _operations = new Dictionary<string, IOperation>(StringComparer.OrdinalIgnoreCase);
            LoadOperations();
        }

        /// <summary>
        /// Scans the current assembly and loads all classes implementing IOperation.
        /// </summary>
        private void LoadOperations()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var operationTypes = assembly.GetTypes()
                .Where(t => typeof(IOperation).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var type in operationTypes)
            {
                var operation = (IOperation)Activator.CreateInstance(type);
                if (operation != null)
                {
                    _operations[operation.Name] = operation;
                }
            }
        }

        /// <summary>
        /// Retrieves all currently loaded operations.
        /// </summary>
        /// <returns>A collection of all available operations.</returns>
        public IEnumerable<IOperation> GetAllOperations() => _operations.Values;
     
        /// <summary>
        /// Retrieves a specific operation instance by its name.
        /// </summary>
        /// <param name="name">The name of the requested operation (e.g., "Add").</param>
        /// <returns>An instance of the requested operation.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the requested operation name is not found.</exception>
        public IOperation GetOperation(string name)
        {
            if (_operations.TryGetValue(name, out var operation))
            {
                return operation;
            }
            throw new KeyNotFoundException($"Operation '{name}' is not supported.");
        }
    }
    public interface IOperationFactory
    {
        IEnumerable<IOperation> GetAllOperations();
        IOperation GetOperation(string name);
    }
}