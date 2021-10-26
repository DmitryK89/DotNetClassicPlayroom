using BookService.DbContext;
using Unity;
using Unity.Lifetime;

namespace BookService.DependencyInjection
{
    public static class ContainerFactory
    {
        public static IUnityContainer Build()
        {
            var container = new UnityContainer();
            AddServices(container);
            return container;
        }

        private static void AddServices(IUnityContainer container)
        {
            container.RegisterType<BookContext>(new HierarchicalLifetimeManager());
        }
    }
}