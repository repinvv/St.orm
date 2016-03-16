using System.Linq;
using StormTest.Entities;
using StormTest.Schema;

namespace StormTest.Services
{
    public class ConversionService
    {
        public company CompanyToDal(Company company)
        {
            return new company
                   {
                       company_id = company.Id ?? 0,
                       name = company.Name,
                       fkdepartment1 = company.Departments?.Select(DepartmentToDal).ToList()
                   };
        }

        private department DepartmentToDal(Department department)
        {
            return new department
                   {
                       department_id = department.Id ?? 0,
                       name = department.Name,
                       fkemployee1 = department.Employees == null ? null : department.Employees.Select(EmployeeToDal).ToList()
                   };
        }

        private employee EmployeeToDal(Employee employee)
        {
            return new employee
                   {
                       employee_id = employee.Id ?? 0,
                       name = employee.Name,
                       fkpayment1 = employee.Payments==null?null:employee.Payments.Select(PaymentToDal).ToList()
                   };
        }

        private payment PaymentToDal(Payment payment)
        {
            return new payment
                   {
                       payment_id = payment.Id ?? 0,
                       amount = payment.Amount,
                       effective_date = payment.EffectiveDate
                   };
        }

        public Company DalToCompany(company company)
        {
            return new Company
                   {
                       Id = company.company_id,
                       Name = company.name,
                       Departments = company.fkdepartment1?.Select(DalToDepartment).ToList()
                   };
        }

        private Department DalToDepartment(department department)
        {
            return new Department
                   {
                       Id= department.department_id,
                       CompanyId = department.company_id,
                       Name = department.name,
                       Employees = department.fkemployee1?.Select(DalToEmployee).ToList()
                   };
        }

        private Employee DalToEmployee(employee employee)
        {
            return new Employee
                   {
                       Id = employee.employee_id,
                       DepartmentId = employee.department_id,
                       Name = employee.name,
                       Payments = employee.fkpayment1?.Select(DalToPayment).ToList()
                   };
        }

        private Payment DalToPayment(payment payment)
        {
            return new Payment
                   {
                       Id = payment.payment_id,
                       EmployeeId = payment.employee_id,
                       Amount = payment.amount,
                       EffectiveDate = payment.effective_date
                   };
        }
    }
}
