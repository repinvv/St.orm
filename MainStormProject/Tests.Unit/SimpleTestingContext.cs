namespace Tests.Unit
{
    using System.Collections.Generic;

    public class SimpleTestingContext
    {
        private readonly Dictionary<string, object> data = new Dictionary<string, object>();

        public void Set(string key, object value)
        {
            data[key] = value;
        }

        public T Get<T>(string key)
        {
            return (T)data[key];
        }
    }
}
