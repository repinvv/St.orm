using System.Collections.Generic;
using System.Linq;
using StormTest.Entities;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using TestingContextCore.PublicMembers;

namespace StormTest.Definitions
{
    [Binding]
    public class EntitySetup
    {
        private readonly Storage storage;

        public EntitySetup(Storage storage)
        {
            this.storage = storage;
        }

        [Given(@"I have companies")]
        public void GivenIHaveCompanies(Table table)
        {
            var companies = table.CreateSet<Company>().ToList();
            storage.Set(companies);
        }

        [Given(@"I have departments in companies")]
        public void GivenIHaveDepartmentsInCompanies(Table table)
        {
            var departments = table.CreateSet<Department>().ToList();
            storage.Set(departments);
            var companies = storage.Get<List<Company>>().ToDictionary(x => x.Id);
            foreach (var depGroup in departments.GroupBy(x => x.CompanyId))
            {
                var company = companies[depGroup.Key];
                company.Departments = company.Departments ?? new List<Department>();
                company.Departments.AddRange(depGroup);
            }
        }

        [Given(@"I have empleyees in departments")]
        public void GivenIHaveEmpleyeesInDepartments(Table table)
        {
            var employees = table.CreateSet<Employee>().ToList();
            storage.Set(employees);
            var departments = storage.Get<List<Department>>().ToDictionary(x => x.Id);
            foreach (var empGroup in employees.GroupBy(x => x.DepartmentId))
            {
                var department = departments[empGroup.Key];
                department.Employees = department.Employees ?? new List<Employee>();
                department.Employees.AddRange(empGroup);
            }
        }

        [Given(@"I have payments for employees")]
        public void GivenIHavePaymentsForEmployees(Table table)
        {
            var payments = table.CreateSet<Payment>().ToList();
            storage.Set(payments);
            var employees = storage.Get<List<Employee>>().ToDictionary(x => x.Id);
            foreach (var payGroup in payments.GroupBy(x => x.EmployeeId))
            {
                var employee = employees[payGroup.Key];
                employee.Payments = employee.Payments ?? new List<Payment>();
                employee.Payments.AddRange(payGroup);
            }
        }
    }
}
