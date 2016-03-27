namespace Tests.Unit
{
    using System.Collections.Generic;
    using System.Linq;
    using TechTalk.SpecFlow;

    internal static class TableToStringList
    {
        public static List<string> ToStringList(this Table table)
        {
            return table.Rows.Select(row => row.Values.First())
                              .ToList();
        }
    }
}
