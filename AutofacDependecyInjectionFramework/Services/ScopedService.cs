namespace AutofacDependecyInjectionFramework.Services
{
    public class ScopedService : IScopedService
    {
        private readonly Guid _operationId;

        public ScopedService()
        {
            _operationId = Guid.NewGuid(); // Mesma instância no mesmo escopo/requisição
        }

        public Guid GetOperationID() => _operationId;
    }
}
