namespace StormGenerator.Generation.CommonGeneration
{
    internal class ObjectStringService
    {
        public string CreateObjectString(string[] keys, string accessor)
        {
            if (keys.Length == 1)
            {
                return accessor + "." + keys[0];
            }

            var output = "new { ";
            for (int n = 0; n < keys.Length; n++)
            {
                output += "f" + (n + 1) + " = " + accessor + "." + keys[n];
                if (n < keys.Length - 1)
                {
                    output += ", ";
                }
            }

            return output + " }";
        }
    }
}
