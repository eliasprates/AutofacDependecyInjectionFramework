namespace AutofacDependecyInjectionFramework.Services
{
    public class SingletonService : ISingletonService
    {
        private readonly Guid _operationId;

        public SingletonService()
        {
            _operationId = Guid.NewGuid(); // A mesma instância terá o mesmo ID sempre
        }

        public Guid GetOperationID() => _operationId;

    }
}