using System.Collections.Generic;

namespace StormTest.Entities
{
    public class Employee
    {
        public int? Id { get; set; }

        public int DepartmentId { get; set; }

        public string Name { get; set; }

        public List<Payment> Payments { get; set; }
    }
}