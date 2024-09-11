namespace AutofacDependecyInjectionFramework.Services
{
    public class TransientService : ITransientService
    {
        public Guid GetOperationID() => Guid.NewGuid(); // Sempre cria um novo ID
    }
}
