using Microsoft.EntityFrameworkCore;
using Routine.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.Data
{
    public class RoutineDbContext : DbContext
    {
        public RoutineDbContext(DbContextOptions<RoutineDbContext> options) : base(options)
        {

        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().Property(x => x.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Company>().Property(x => x.Introduction).IsRequired().HasMaxLength(500);
            modelBuilder.Entity<Employee>().Property(x => x.employeeNo).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<Employee>().Property(x => x.firstName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Employee>().Property(x => x.lastName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Employee>()
                .HasOne(x => x.Company)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.companyId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = Guid.Parse("da7ee895-bb98-4b80-b0f1-d9d86fef8c95"),
                    Name = "Microsoft",
                    Introduction = "Great Company"
                },
                new Company
                {
                    Id = Guid.Parse("1544a292-1c80-4595-bc4a-2a4952fc5c1f"),
                    Name = "Google",
                    Introduction = "Don't be evil"
                },
                new Company
                {
                    Id = Guid.Parse("d030761a-7ab4-4235-9232-def88be14d4a"),
                    Name = "Alipapa",
                    Introduction = "Fubao Company"
                });

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = Guid.Parse("9665c8ef-03d4-4af1-87bc-d84cdaded5f5"),
                    companyId = Guid.Parse("da7ee895-bb98-4b80-b0f1-d9d86fef8c95"),
                    employeeNo = "1wckk9NbrGGUSZU4",
                    firstName = "Bruce",
                    lastName = "Forster",
                    Gender = Gender.男,
                    dateOfBirth = new DateTime(1990, 01, 01)
                },
                new Employee
                {
                    Id = Guid.Parse("12bcfa41-5b5c-45e9-aa34-5a7a52183584"),
                    companyId = Guid.Parse("da7ee895-bb98-4b80-b0f1-d9d86fef8c95"),
                    employeeNo = "VU7yqFzVUXqnVJyU",
                    firstName = "Donald",
                    lastName = "Washington",
                    Gender = Gender.女,
                    dateOfBirth = new DateTime(1988, 01, 01)
                },
                new Employee
                {
                    Id = Guid.Parse("d51d5aa1-2329-4e40-8573-4a8ddb520c18"),
                    companyId = Guid.Parse("da7ee895-bb98-4b80-b0f1-d9d86fef8c95"),
                    employeeNo = "1RewsGnQdvozB0cb",
                    firstName = "Vito",
                    lastName = "Evans",
                    Gender = Gender.男,
                    dateOfBirth = new DateTime(1958, 01, 01)
                },
                new Employee
                {
                    Id = Guid.Parse("bc7d1bbd-bd48-413d-95e7-c8f800e32629"),
                    companyId = Guid.Parse("1544a292-1c80-4595-bc4a-2a4952fc5c1f"),
                    employeeNo = "huHM1OcqoLLpyL0X",
                    firstName = "Polly",
                    lastName = "Salome",
                    Gender = Gender.女,
                    dateOfBirth = new DateTime(1994, 01, 01)
                },
                new Employee
                {
                    Id = Guid.Parse("e9d3f23d-c71d-4c12-bcc2-a9554ba50396"),
                    companyId = Guid.Parse("d030761a-7ab4-4235-9232-def88be14d4a"),
                    employeeNo = "g7BSz9HJZoJui5wr",
                    firstName = "Len",
                    lastName = "Partridge",
                    Gender = Gender.男,
                    dateOfBirth = new DateTime(1976, 01, 01)
                },
                new Employee
                {
                    Id = Guid.Parse("053988cc-05b4-4bf5-8c24-ea826d5ee90d"),
                    companyId = Guid.Parse("1544a292-1c80-4595-bc4a-2a4952fc5c1f"),
                    employeeNo = "Zpsy9IdWEc6kdrhQ",
                    firstName = "Adair",
                    lastName = "Kent",
                    Gender = Gender.女,
                    dateOfBirth = new DateTime(2000, 01, 01)
                },
                new Employee
                {
                    Id = Guid.Parse("3c3fe8ec-c441-4855-83cd-ea9e3cf60fc9"),
                    companyId = Guid.Parse("d030761a-7ab4-4235-9232-def88be14d4a"),
                    employeeNo = "I4nNzk1ezsuSEaVC",
                    firstName = "Bernice",
                    lastName = "Harold",
                    Gender = Gender.男,
                    dateOfBirth = new DateTime(1983, 01, 01)
                },
                new Employee
                {
                    Id = Guid.Parse("a035c368-1171-4633-baee-fcfa31bf9b3b"),
                    companyId = Guid.Parse("1544a292-1c80-4595-bc4a-2a4952fc5c1f"),
                    employeeNo = "NO9drmGvuBM9hBom",
                    firstName = "Edison",
                    lastName = "Thackeray",
                    Gender = Gender.女,
                    dateOfBirth = new DateTime(1975, 01, 01)
                }

                ); ;

            base.OnModelCreating(modelBuilder);
        }
    }
}
