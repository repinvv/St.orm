using System.Linq;
using StormTest.Entities;

namespace StormTest.Services
{
    public class ComparisionService
    {
        public bool CompaniesAreEqual(Company a, Company b)
        {
            if (a.Name != b.Name)
            {
                return false;
            }

            var equal = a.Departments.All(x => b.Departments.Count(y => DepartmentsAreEqual(x, y)) == 1);
            return equal;
        }

        private bool DepartmentsAreEqual(Department a, Department b)
        {
            if (a.Name != b.Name)
            {
                return false;
            }

            var equal = a.Employees.All(x => b.Employees.Count(y => EmployeesAreEqual(x, y)) == 1);
            return equal;
        }

        private bool EmployeesAreEqual(Employee a, Employee b)
        {
            if (a.Name != b.Name)
            {
                return false;
            }

            var equal = a.Payments.All(x => b.Payments.Count(y => PaymentsAreEqual(x, y)) == 1);
            return equal;
        }

        private bool PaymentsAreEqual(Payment a, Payment b)
        {
            if (a.Amount != b.Amount)
            {
                return false;
            }

            if (a.EffectiveDate != b.EffectiveDate)
            {
                return false;
            }

            return true;
        }
    }
}
