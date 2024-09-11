namespace AutofacDependecyInjectionFramework.Services
{
    public class MatchingScopeService : IMatchingScopeService
    {
        private readonly Guid _operationId;

        public MatchingScopeService()
        {
            _operationId = Guid.NewGuid(); // Compartilhado em um escopo específico
        }

        public Guid GetOperationID() => _operationId;
    }
}
