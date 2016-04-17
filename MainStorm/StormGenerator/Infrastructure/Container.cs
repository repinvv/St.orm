namespace StormGenerator.Infrastructure
{
    using System.Linq;
    using StormGenerator.Settings;

    internal interface IResolve
    {
        T Get<T>();
    }

    internal class Container : IResolve
    {
        private readonly IoC container;

        public Container(Options options)
        {
            container = new IoC();
            Register(this);
            container.RegisterInstance(options);
            container.RegisterInstance<IResolve, Container>(this);
        }

        public T Get<T>()
        {
            return container.Resolve<T>();
        }

        private void Register(object source)
        {
            foreach (var type in source.GetType()
                                       .Assembly.GetTypes()
                                       .Where(x => !x.IsAbstract)
                                       .Where(x => !x.IsValueType)
                                       .Where(x => !x.Name.StartsWith("<")))
            {
                container.Register(type);
                var face = type.GetInterfaces()
                               .FirstOrDefault();
                if (face != null)
                {
                    container.Register(face, type);
                }
            }
        }
    }
}
