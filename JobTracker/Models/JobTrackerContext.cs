using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobTracker.Models
{
    public class JobTrackerContext : DbContext
    {
        public JobTrackerContext(DbContextOptions<JobTrackerContext> options) : base(options) { }

        public DbSet<Contractor> Contractors { get; set;}
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobRegulation> JobRegulations { get; set; }
        public DbSet<Regulation> Regulations { get; set; }
        public DbSet<RegulationRequirement> RegulationRequirements { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskAssignment> TaskAssignments { get; set; }
        public DbSet<WorkHour> WorkHours { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contractor>().HasData(
                new Contractor
                {
                    ContractorId = 1,
                    FName = "John",
                    LName = "Doe",
                    PhoneNumber = "123-456-7890",
                    Email = "John.D@Email.com"
                }, 
                new Contractor
                {
                    ContractorId = -1,
                    FName = "",
                    LName = "",
                    PhoneNumber = "",
                    Email = ""
                });

            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    CustomerId = 1,
                    FName = "Jane",
                    LName = "Doe",
                    PhoneNumber = "098-765-4321",
                    Email = "Jane.D@Email.com"
                });

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                { 
                    EmployeeId = 1,
                    FName = "Bob",
                    LName = "Saget",
                    Email = "B.S@Email.com",
                    PhoneNumber = "456-789-0123",
                    Role = "admin",
                    Username = "bob.s",
                    Password = "pass"
                },
                new Employee
                {
                    EmployeeId = 2,
                    FName = "Kevin",
                    LName = "Hart",
                    Email = "k.h@Email.com",
                    PhoneNumber = "568-875-5789",
                    Role = "employee",
                    Username = "kevin.h",
                    Password = "pass"
                },
                new Employee
                {
                    EmployeeId = -1,
                    FName = "",
                    LName = "",
                    Email = "",
                    PhoneNumber = "",
                    Role = "",
                    Username = "",
                    Password = ""
                });

            modelBuilder.Entity<Expense>().HasData(
                new Expense
                {
                    ExpenseId = 1,
                    JobId = 1,
                    Amount = 190.9m
                });

            modelBuilder.Entity<Job>().HasData(
                new Job
                {
                    JobId = 1,
                    CustomerId = 1,
                    Name = "The Creamery",
                    Address = "123 This Pl Sioux Falls, SD 57104",
                    StartDate = DateTime.Parse("2021-05-20 00:00:00.000"),
                    Status = "In Progress"
                });

            modelBuilder.Entity<JobRegulation>().HasData(
                new JobRegulation
                {
                    JobRegulationId = 1,
                    JobId = 1,
                    RegulationId = 1
                });

            modelBuilder.Entity<Regulation>().HasData(
                new Regulation
                {
                    RegulationId = 1,
                    Name = "HB201",
                    Description = "Insulation"
                });

            modelBuilder.Entity<RegulationRequirement>().HasData(
                new RegulationRequirement
                {
                    RegulationRequirementId = 1,
                    RegulationId = 1,
                    Requirement = "Insulation shall be extended the prescribed distance by any combination of vertical insulation."
                });

            modelBuilder.Entity<Task>().HasData(
                new Task
                {
                    TaskId = 1,
                    JobId = 1,
                    Name = "Add siding",
                    Description = "Use 2 inch foam.",
                    Status = "In Progress",
                    EstCompletionDate = DateTime.Parse("2021-05-10 00:00:00.000")
                },
                new Task
                {
                    TaskId = 2,
                    JobId = 1,
                    Name = "Install Wiring",
                    Description = "Use the casing and caping wiring.",
                    Status = "In Progress",
                    EstCompletionDate = DateTime.Parse("2021-05-10 00:00:00.000")
                });

            modelBuilder.Entity<TaskAssignment>().HasData(
                new TaskAssignment
                {
                    TaskAssignmentId = 1,
                    TaskId = 1,
                    EmployeeId = 1,
                    ContractorId = -1
                },
                new TaskAssignment
                {
                    TaskAssignmentId = 2,
                    TaskId = 2,
                    EmployeeId = -1,
                    ContractorId = 1
                });

            modelBuilder.Entity<WorkHour>().HasData(
                new WorkHour
                {
                    WorkHourId = 1,
                    EmployeeId = 1,
                    Date = DateTime.Parse("2021-04-10 00:00:00.000"),
                    Hours = 8
                });

        }
    }
}
