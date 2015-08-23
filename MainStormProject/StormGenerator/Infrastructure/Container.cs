namespace StormGenerator.Infrastructure
{
    using System.Linq;

    internal class Container
    {
        private readonly IoC container;

        public Container()
        {
            container = new IoC();
            Register(this);
            container.RegisterSingleton<OptionsService>();
        }

        public T Get<T>()
        {
            return container.Resolve<T>();
        }

        private void Register(object source)
        {
            foreach (var type in source.GetType().Assembly.GetTypes().Where(x => !x.IsAbstract).Where(x => !x.IsValueType).Where(x => !x.Name.StartsWith("<")))
            {
                container.Register(type);
                var face = type.GetInterfaces().FirstOrDefault();
                if (face != null)
                {
                    container.Register(face, type);
                }
            }
        }
    }
}
