using ProjectTimelyIn.Core.Services;

namespace ProjectTimelyIn.Core.Entities
{
    public class FakeContext : IDataContext
    {
        public IList<Employee> Employee { get; set; } = new List<Employee>();

        public FakeContext()
        {
            Employee = new List<Employee>()
            {
                new Employee()
                {
                    FullName = "esti choen",
                    Id = 12,
                    Email = "estiC7756@gmail.com",
                    Password = "123",
                    Role = "Employee"
                }
            };
        }
    }
}
