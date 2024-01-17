using UnityEngine;
using Zenject;

namespace Core.Factory
{
    public class BootstrapperFactory
    {
        private readonly DiContainer _container;

        public BootstrapperFactory(DiContainer container)
        {
            _container = container;
        }

        public void CreateBootstrapper()
        {
            Bootstrapper bootstrapper = 
                new GameObject("Bootstrapper")
                .AddComponent<Bootstrapper>();
            _container.Inject(bootstrapper);
        }
    }
}