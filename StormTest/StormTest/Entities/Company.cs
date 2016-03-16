using System.Collections.Generic;

namespace StormTest.Entities
{
    public class Company
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public List<Department> Departments { get; set; }
    }
}
