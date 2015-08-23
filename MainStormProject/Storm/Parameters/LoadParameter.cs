namespace St.Orm.Parameters
{
    public class LoadParameter
    {
        public LoadParameter(object key, object val)
        {
            Key = key;
            Value = val;
        }

        public LoadParameter(object key)
        {
            Key = key;
            Value = true;
        }

        public object Key { get; set; }

        public object Value { get; set; }
    }
}
