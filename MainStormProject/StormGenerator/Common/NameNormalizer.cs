namespace StormGenerator.Common
{
    using System;
    using System.Collections.Generic;

    internal class NameNormalizer
    {
        public List<string> NormalizeNames(List<string> names)
        {
            var all = new HashSet<string>(names);
            var placed = new HashSet<string>();
            var output = new List<string>();
            foreach (var name in names)
            {
                if (!placed.Contains(name))
                {
                    output.Add(name);
                    placed.Add(name);
                    continue;
                }

                bool found = false;
                int i = 2;
                while (!found)
                {
                    var newName = name + i;
                    if (!all.Contains(newName))
                    {
                        all.Add(newName);
                        placed.Add(newName);
                        output.Add(newName);
                        found = true;
                    }

                    i++;
                    if (i == 100)
                    {
                        throw new Exception("Too many entities/properties with the same name, consider revising.");
                    }
                }
            }

            return output;
        }
    }
}
