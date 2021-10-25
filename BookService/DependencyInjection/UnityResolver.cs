using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Unity;

namespace BookService.DependencyInjection
{
    public class UnityResolver : IDependencyResolver
    {
        private readonly IUnityContainer _unityContainer;

        public UnityResolver(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer ?? throw new ArgumentNullException(nameof(unityContainer));
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _unityContainer.Resolve(serviceType);
            }
            catch (ResolutionFailedException exception)
            {
                throw new InvalidOperationException($"Unable to resolve service for type {serviceType}.", exception);
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _unityContainer.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException exception)
            {
                throw new InvalidOperationException($"Unable to resolve service for type {serviceType}.", exception);
            }
        }
        
        public IDependencyScope BeginScope()
        {
            var child = _unityContainer.CreateChildContainer();
            return new UnityResolver(child);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            _unityContainer.Dispose();
        }
    }
}