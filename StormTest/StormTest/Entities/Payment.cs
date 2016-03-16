using System;

namespace StormTest.Entities
{
    public class Payment
    {
        public int? Id { get; set; }

        public int EmployeeId { get; set; }

        public DateTime EffectiveDate { get; set; }

        public decimal Amount { get; set; }
    }
}