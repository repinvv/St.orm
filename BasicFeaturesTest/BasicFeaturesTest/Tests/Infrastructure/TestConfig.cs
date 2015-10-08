namespace BasicFeaturesTest.Tests.Infrastructure
{
    using System.Linq;

    internal static class TestConfig
    {
        private static readonly IoC Container = Register(new IoC());

        private static IoC Register(IoC container)
        {
            foreach (var type in typeof(TestConfig).Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => !x.IsValueType)
                .Where(x => !x.Name.StartsWith("<")))
            {
                container.Register(type);
                var face = type.GetInterfaces().FirstOrDefault();
                if (face != null)
                {
                    container.Register(face, type);
                }
            }

            return container;
        }

        public static T Get<T>()
        {
            return Container.Resolve<T>();
        }
    }
}