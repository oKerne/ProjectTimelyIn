using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjectTimelyIn.Core.Entities;

namespace ProjectTimelyIn.Data
{
    //public class DataContext : DbContext
    //{
    //    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    //    private readonly IConfiguration _configuration;

    //    public DbSet<Employee> Employees { get; set; }
    //    public DbSet<Vacation> Vacations { get; set; }
    //    public DbSet<WorkHours> WorkHours { get; set; }
    //    public DbSet<User> Users { get; set; }
    //        public IList<Employee> EmployeeList
    //        {
    //            get => Employees.Local.ToList();
    //            set
    //            {
    //                foreach (var employee in value)
    //                {
    //                    Employees.Add(employee);
    //                }
    //            }
    //        }
    //        public DataContext(IConfiguration configuration)
    //        {
    //            _configuration = configuration;
    //        }
    //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //        {
    //            optionsBuilder.UseSqlServer(_configuration["DbConnectionString"]);
    //        }
    //        protected override void OnModelCreating(ModelBuilder modelBuilder)
    //        {
    //            base.OnModelCreating(modelBuilder);

    //            // קשרים
    //            //modelBuilder.Entity<Employee>()
    //            //    .HasOne(u => u.)
    //            //    .WithMany(r => r.)
    //            //    .HasForeignKey(u => u.EmployeeId);

    //            modelBuilder.Entity<WorkHours>()
    //                .HasOne(wh => wh.Employee)
    //                .WithMany(u => u.WorkHours)
    //                .HasForeignKey(wh => wh.EmployeeId);



    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<WorkHours> WorkHours { get; set; }
        public DbSet<User> Users { get; set; }


        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration["DbConnectionString"]);
            }
        }
        //קשרים
        //    protected override void OnModelCreating(ModelBuilder modelBuilder)
        //    {
        //        base.OnModelCreating(modelBuilder);

        //        modelBuilder.Entity<Employee>()
        //            .HasOne(u => u.User)
        //            .WithMany(r => r.Employees)
        //            .HasForeignKey(u => u.UserId);

        //        modelBuilder.Entity<Vacation>()
        //            .HasOne(v => v.Employee)
        //            .WithMany(e => e.Vacations)
        //            .HasForeignKey(v => v.EmployeeId)
        //            .OnDelete(DeleteBehavior.Cascade);

        //        modelBuilder.Entity<WorkHours>()
        //            .HasOne(wh => wh.Employee)
        //            .WithMany(e => e.WorkHours)
        //            .HasForeignKey(wh => wh.EmployeeId)
        //            .OnDelete(DeleteBehavior.Cascade);
        //    }
        //}
    }
}