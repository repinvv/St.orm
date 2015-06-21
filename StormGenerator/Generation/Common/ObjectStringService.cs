namespace StormGenerator.Generation.Common
{
    using System.Collections.Generic;

    internal class ObjectStringService
    {
        public string CreateObjectString(IList<string> keys, string accessor = null, bool replaceFieldNames = true)
        {
            if (accessor != null)
            {
                accessor += ".";
            }

            if (keys.Count == 1)
            {
                return accessor + keys[0];
            }

            var output = "new { ";
            for (int n = 0; n < keys.Count; n++)
            {
                var fieldName = replaceFieldNames ? "f" + (n + 1) + " = " : string.Empty;
                output += fieldName + accessor + keys[n];
                if (n < keys.Count - 1)
                {
                    output += ", ";
                }
            }

            return output + " }";
        }
    }
}
