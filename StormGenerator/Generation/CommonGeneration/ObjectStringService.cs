namespace StormGenerator.Generation.CommonGeneration
{
    using System;

    internal class ObjectStringService
    {
        public string CreateObjectString(string[] keys, string accessor, bool replaceFieldNames = true)
        {
            if (keys.Length == 1)
            {
                return accessor + "." + keys[0];
            }

            var output = "new { ";
            for (int n = 0; n < keys.Length; n++)
            {
                var fieldName = replaceFieldNames ? "f" + (n + 1) + " = " : string.Empty;
                output += fieldName + accessor + "." + keys[n];
                if (n < keys.Length - 1)
                {
                    output += ", ";
                }
            }

            return output + " }";
        }
    }
}
