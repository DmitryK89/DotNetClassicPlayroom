using System.Web.Http;
using BookService.DbContext;
using Unity;
using Unity.Lifetime;

namespace BookService.DependencyInjection.DependencyResolver
{
    public static class ContainerFactory
    {
        public static void Build(HttpConfiguration config)
        {
            var container = new UnityContainer();
            AddServices(container);
            config.DependencyResolver = new UnityResolver(container);
        }

        private static void AddServices(UnityContainer container)
        {
            container.RegisterType<BookContext>(new HierarchicalLifetimeManager());
        }
    }
}