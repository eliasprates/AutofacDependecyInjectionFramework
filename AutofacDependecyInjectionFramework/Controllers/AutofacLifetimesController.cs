using Autofac;
using AutofacDependecyInjectionFramework.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutofacDependecyInjectionFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutofacLifetimes : ControllerBase
    {
        private readonly ISingletonService _singletonService;
        private readonly ITransientService _transientService;
        private readonly IScopedService _scopedService;
        private readonly IExternallyOwnedService _externallyOwnedService;
        private readonly ILifetimeScope _lifetimeScope;

        public AutofacLifetimes(
            ISingletonService singletonService,
            ITransientService transientService,
            IScopedService scopedService,
            IExternallyOwnedService externallyOwnedService,
            ILifetimeScope lifetimeScope)
        {
            _singletonService = singletonService;
            _transientService = transientService;
            _scopedService = scopedService;
            _externallyOwnedService = externallyOwnedService;
            _lifetimeScope = lifetimeScope;
        }

        [HttpGet]
        public IActionResult Get()
        {
            // Singleton - Deve ser o mesmo em todas as requisições
            var singletonFirstCall = _singletonService.GetOperationID();
            var singletonSecondCall = _singletonService.GetOperationID();

            // Transient - Deve criar uma nova instância a cada vez
            var transientFirstCall = _transientService.GetOperationID();
            var transientSecondCall = _transientService.GetOperationID();

            // Scoped - Deve ser o mesmo dentro da requisição, mas diferente entre requisições
            var scopedFirstCall = _scopedService.GetOperationID();
            var scopedSecondCall = _scopedService.GetOperationID();

            // ExternallyOwned - Vamos criar a instância manualmente e forçar a mudança
            var externallyOwnedServiceNewInstance = new ExternallyOwnedService();
            var externallyOwnedFirstCall = externallyOwnedServiceNewInstance.GetOperationID();
            var externallyOwnedSecondCall = externallyOwnedServiceNewInstance.GetOperationID();

            // MatchingScope - Será o mesmo dentro do escopo "custom-scope"
            Guid matchingScopeFirstCall;
            Guid matchingScopeSecondCall;

            using (var customScope1 = _lifetimeScope.BeginLifetimeScope("custom-scope"))
            {
                var matchingScopeService = customScope1.Resolve<IMatchingScopeService>();
                matchingScopeFirstCall = matchingScopeService.GetOperationID();
            }

            using (var customScope2 = _lifetimeScope.BeginLifetimeScope("custom-scope"))
            {
                var matchingScopeService = customScope2.Resolve<IMatchingScopeService>();
                matchingScopeSecondCall = matchingScopeService.GetOperationID();
            }

            return Ok(new
            {
                Singleton = new { FirstCall = singletonFirstCall, SecondCall = singletonSecondCall },
                Transient = new { FirstCall = transientFirstCall, SecondCall = transientSecondCall },
                Scoped = new { FirstCall = scopedFirstCall, SecondCall = scopedSecondCall },
                ExternallyOwned = new { FirstCall = externallyOwnedFirstCall, SecondCall = externallyOwnedSecondCall },
                MatchingScope = new { FirstCall = matchingScopeFirstCall, SecondCall = matchingScopeSecondCall }
            });
        }
    }
}
