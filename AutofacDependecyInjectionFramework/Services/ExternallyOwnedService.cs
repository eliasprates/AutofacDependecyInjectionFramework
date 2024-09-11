namespace AutofacDependecyInjectionFramework.Services
{
    public class ExternallyOwnedService : IExternallyOwnedService
    {
        private readonly Guid _operationId;

        public ExternallyOwnedService()
        {
            _operationId = Guid.NewGuid(); // Controlado externamente
        }

        public Guid GetOperationID() => _operationId;
    }
}
