using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjectTimelyIn.Core.Entities;
namespace ProjectTimelyIn.Data
{
    public class DataContext : DbContext
    {  
        //public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        private readonly IConfiguration _configuration;

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<WorkHours> WorkHours { get; set; }
        public IList<Employee> EmployeeList
        {
            get => Employees.Local.ToList();
            set
            {
                foreach (var employee in value)
                {
                    Employees.Add(employee);
                }
            }
        }
        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration["DbConnectionString"]);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // קשרים
            //modelBuilder.Entity<Employee>()
            //    .HasOne(u => u.)
            //    .WithMany(r => r.)
            //    .HasForeignKey(u => u.EmployeeId);

            modelBuilder.Entity<WorkHours>()
                .HasOne(wh => wh.Employee)
                .WithMany(u => u.WorkHours)
                .HasForeignKey(wh => wh.EmployeeId);

            modelBuilder.Entity<Vacation>()
                .HasOne(lr => lr.Employee)
                .WithMany(u => u.Vacations)
                .HasForeignKey(lr => lr.EmployeeId);
        }
    }
}

    