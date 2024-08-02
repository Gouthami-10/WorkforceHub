using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using WorkforceHub.DataAccess.Entities;

namespace WorkforceHubAPI.DataAccess
{
    public class WorkforceHubContext : DbContext
    {
        public WorkforceHubContext(DbContextOptions<WorkforceHubContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }
        public DbSet<Manager> Managers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Person)
                .WithMany()
                .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<Supervisor>()
                .HasOne(s => s.Person)
                .WithMany()
                .HasForeignKey(s => s.SupervisorID);

            modelBuilder.Entity<Manager>()
                .HasOne(m => m.Person)
                .WithMany()
                .HasForeignKey(m => m.ManagerID);
        }
    }
}
