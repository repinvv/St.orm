﻿namespace StormGenerator.AutomaticPopulation
{
    using System.Collections.Generic;
    using System.Linq;
    using StormGenerator.Common;
    using StormGenerator.Models;
    using StormGenerator.Models.Configs;

    internal class ConfigListNameNormalizer
    {
        private readonly NameNormalizer normalizer;

        public ConfigListNameNormalizer(NameNormalizer normalizer)
        {
            this.normalizer = normalizer;
        }

        public void NormalizeConfigNames<T>(IEnumerable<T> items)
            where T : ItemConfig
        {
            var ordered = items.OrderBy(x => x.IsGenerated ? 1 : 0)
                               .ToList();
            var names = ordered.Select(x => x.Name)
                               .ToList();
            var normalized = normalizer.NormalizeNames(names);
            for (int i = 0; i < ordered.Count; i++)
            {
                if (ordered[i].IsGenerated)
                {
                    ordered[i].Name = normalized[i];
                }
            }
        }

        public void NormalizeNames<T>(IEnumerable<T> items)
            where T : Named
        {
            var list = items as IList<T> ?? items.ToList();
            var names = list.Select(x => x.Name)
                            .ToList();
            var normalized = normalizer.NormalizeNames(names);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Name = normalized[i];
            }
        }
    }
}
