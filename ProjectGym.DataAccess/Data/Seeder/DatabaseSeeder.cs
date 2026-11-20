using ProjectGym.DataAccess.Data.Contexts;

namespace ProjectGym.DataAccess.Data.Seeder;

using global::ProjectGym.DataAccess.Entities;
using global::ProjectGym.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using ProjectGym.DataAccess.Entities.ValueObjects;

public static class DatabaseSeeder
{
    public static async Task SeedAllAsync(GymDbContext dbContext)
    {
        await SeedCategoriesAsync(dbContext);
        await SeedPlansAsync(dbContext);
        await SeedMembersAsync(dbContext);
        await SeedTrainersAsync(dbContext);
        await SeedHealthRecordsAsync(dbContext);
        await SeedMemberShipsAsync(dbContext);
        await SeedSessionsAsync(dbContext);
        await SeedBookingsAsync(dbContext);
    }

    private static async Task SeedCategoriesAsync(GymDbContext dbContext)
    {
        if (await dbContext.Categories.IgnoreQueryFilters().AnyAsync())
            return;

        var categories = new List<Category>
        {
            new() { Name = "Yoga", CreatedAt = new DateOnly(2026, 11, 3), UpdatedAt = null, Deleted = null, IsDeleted = false },
            new() { Name = "Cardio", CreatedAt = new DateOnly(2026, 11, 3), UpdatedAt = null, Deleted = null, IsDeleted = false },
            new() { Name = "Flexibility", CreatedAt = new DateOnly(2026, 11, 3), UpdatedAt = null, Deleted = null, IsDeleted = false },
            new() { Name = "Balance", CreatedAt = new DateOnly(2026, 11, 3), UpdatedAt = null, Deleted = null, IsDeleted = false },
            new() { Name = "Endurance", CreatedAt = new DateOnly(2026, 11, 3), UpdatedAt = null, Deleted = null, IsDeleted = false }
        };

        await dbContext.Categories.AddRangeAsync(categories);
        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedPlansAsync(GymDbContext dbContext)
    {
        if (await dbContext.Plans.IgnoreQueryFilters().AnyAsync())
            return;

        var plans = new List<Plan>
        {
            new()
            {
                Name = "Basic Plan",
                Description = "Access to gym equipment during staffed hours",
                Price = 300.00m,
                IsActive = true,
                DurationDays = 30,
                CreatedAt = new DateOnly(2026, 11, 3),
                UpdatedAt = null,
                Deleted = null,
                IsDeleted = false
            },
            new()
            {
                Name = "Standard Plan",
                Description = "Includes gym equipment and 2 group classes per week",
                Price = 500.00m,
                IsActive = false,
                DurationDays = 60,
                CreatedAt = new DateOnly(2026, 11, 3),
                UpdatedAt = new DateOnly(2026, 11, 14),
                Deleted = null,
                IsDeleted = false
            },
            new()
            {
                Name = "Premium Plan",
                Description = "Unlimited access to equipment, classes, and sauna",
                Price = 900.00m,
                IsActive = false,
                DurationDays = 90,
                CreatedAt = new DateOnly(2026, 11, 3),
                UpdatedAt = null,
                Deleted = null,
                IsDeleted = false
            },
            new()
            {
                Name = "Annual Plan",
                Description = "Full year access with personal trainer sessions",
                Price = 3000.00m,
                IsActive = true,
                DurationDays = 365,
                CreatedAt = new DateOnly(2026, 11, 3),
                UpdatedAt = new DateOnly(2026, 11, 14),
                Deleted = null,
                IsDeleted = false
            }
        };

        await dbContext.Plans.AddRangeAsync(plans);
        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedMembersAsync(GymDbContext dbContext)
    {
        if (await dbContext.Users.IgnoreQueryFilters().OfType<Member>().AnyAsync())
            return;

        var members = new List<Member>
        {
            new()
            {
                Name = "ahmed youssef",
                Email = "ay3220187@gmail.com",
                DateOfBirth = new DateOnly(2026, 11, 2),
                Phone = "01122363539",
                Gender = (Gender)1,
                Address = new Address { Street = "ds", City = "sdfsdf", BuildingNumber = 2 },
                ImageUrl = null,
                JoinDate = new DateOnly(2026, 11, 4),
                CreatedAt = new DateOnly(2026, 11, 4),
                UpdatedAt = new DateOnly(2026, 11, 18),
                Deleted = null,
                IsDeleted = true
            },
            new()
            {
                Name = "rick willms",
                Email = "ahmed333@gmail.com",
                DateOfBirth = new DateOnly(2026, 7, 4),
                Phone = "01122545000",
                Gender = (Gender)2,
                Address = new Address { Street = "Russel Ferriii", City = "Oremii", BuildingNumber = 30 },
                ImageUrl = null,
                JoinDate = new DateOnly(2026, 11, 4),
                CreatedAt = new DateOnly(2026, 11, 4),
                UpdatedAt = new DateOnly(2026, 11, 11),
                Deleted = null,
                IsDeleted = true
            },
            new()
            {
                Name = "marin purdy",
                Email = "fakedata83693@gmail.com",
                DateOfBirth = new DateOnly(2027, 4, 26),
                Phone = "01025226622",
                Gender = (Gender)2,
                Address = new Address { Street = "S Union Street", City = "London", BuildingNumber = 500 },
                ImageUrl = null,
                JoinDate = new DateOnly(2026, 11, 15),
                CreatedAt = new DateOnly(2026, 11, 15),
                UpdatedAt = new DateOnly(2026, 11, 18),
                Deleted = null,
                IsDeleted = true
            },
            new()
            {
                Name = "melvina hermiston",
                Email = "fakedata56786@gmail.com",
                DateOfBirth = new DateOnly(2026, 2, 10),
                Phone = "01122363532",
                Gender = (Gender)1,
                Address = new Address { Street = "Schuster Course", City = "Madera", BuildingNumber = 450 },
                ImageUrl = null,
                JoinDate = new DateOnly(2026, 11, 17),
                CreatedAt = new DateOnly(2026, 11, 17),
                UpdatedAt = null,
                Deleted = null,
                IsDeleted = false
            },
            new()
            {
                Name = "Sherry Abbott",
                Email = "fakedata54027@gmail.com",
                DateOfBirth = new DateOnly(2026, 1, 24),
                Phone = "01533363570",
                Gender = (Gender)2,
                Address = new Address { Street = "Alex Route", City = "Giza", BuildingNumber = 70 },
                ImageUrl = null,
                JoinDate = new DateOnly(1, 1, 1),
                CreatedAt = new DateOnly(2026, 11, 18),
                UpdatedAt = new DateOnly(2026, 11, 18),
                Deleted = null,
                IsDeleted = true
            },
            new()
            {
                Name = "Casper Ortiz",
                Email = "fakedata45029@gmail.com",
                DateOfBirth = new DateOnly(2000, 1, 5),
                Phone = "01022632556",
                Gender = (Gender)2,
                Address = new Address { Street = "Wehner Springs", City = "Perth Amboy", BuildingNumber = 83 },
                ImageUrl = null,
                JoinDate = new DateOnly(1, 1, 1),
                CreatedAt = new DateOnly(2026, 11, 18),
                UpdatedAt = new DateOnly(2026, 11, 18),
                Deleted = null,
                IsDeleted = true
            },
            new()
            {
                Name = "Francesco Wintheiser",
                Email = "fakedata31604@gmail.com",
                DateOfBirth = new DateOnly(2000, 5, 8),
                Phone = "01143568792",
                Gender = (Gender)1,
                Address = new Address { Street = "Whitney Junctions", City = "Minneapolis", BuildingNumber = 50 },
                ImageUrl = null,
                JoinDate = new DateOnly(1, 1, 1),
                CreatedAt = new DateOnly(2026, 11, 19),
                UpdatedAt = new DateOnly(2026, 11, 19),
                Deleted = null,
                IsDeleted = false
            },
            new()
            {
                Name = "Bonnie Schaefer",
                Email = "fakedata75681@gmail.com",
                DateOfBirth = new DateOnly(1998, 6, 7),
                Phone = "01153687972",
                Gender = (Gender)2,
                Address = new Address { Street = "Kutch Mount", City = "Trujillo Alto", BuildingNumber = 222 },
                ImageUrl = null,
                JoinDate = new DateOnly(1, 1, 1),
                CreatedAt = new DateOnly(2026, 11, 19),
                UpdatedAt = null,
                Deleted = null,
                IsDeleted = false
            },
            new()
            {
                Name = "Trevor Dickens",
                Email = "fakedata567860@gmail.com",
                DateOfBirth = new DateOnly(2005, 4, 19),
                Phone = "01252526276",
                Gender = (Gender)1,
                Address = new Address { Street = "Pennsylvania Avenue", City = "Boynton Beach", BuildingNumber = 535 },
                ImageUrl = null,
                JoinDate = new DateOnly(1, 1, 1),
                CreatedAt = new DateOnly(2026, 11, 19),
                UpdatedAt = null,
                Deleted = null,
                IsDeleted = true
            }
        };

        await dbContext.Users.AddRangeAsync(members);
        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedTrainersAsync(GymDbContext dbContext)
    {
        if (await dbContext.Users.IgnoreQueryFilters().OfType<Trainer>().AnyAsync())
            return;

        var trainers = new List<Trainer>
        {
            new()
            {
                Name = "cody",
                Email = "fakedata93380@gmail.com",
                DateOfBirth = new DateOnly(2027, 7, 3),
                Phone = "01125656300",
                Gender = (Gender)1,
                Address = new Address { Street = "Brian Tunnel", City = "Charlotte", BuildingNumber = 555 },
                Speciality = Speciality.Flexibility,
                HireDate = new DateOnly(2026, 11, 12),
                CreatedAt = new DateOnly(2026, 11, 12),
                UpdatedAt = new DateOnly(2026, 11, 12),
                Deleted = null,
                IsDeleted = false
            },
            new()
            {
                Name = "raul hilpert",
                Email = "fakedata93381@gmail.com",
                DateOfBirth = new DateOnly(2026, 12, 28),
                Phone = "01112566652",
                Gender = (Gender)1,
                Address = new Address { Street = "Washington Street", City = "Norfolk", BuildingNumber = 1 },
                Speciality = Speciality.Balance,
                HireDate = new DateOnly(2026, 11, 12),
                CreatedAt = new DateOnly(2026, 11, 12),
                UpdatedAt = new DateOnly(2026, 11, 12),
                Deleted = null,
                IsDeleted = false
            },
            new()
            {
                Name = "saige bradtke",
                Email = "fakedata94990@gmail.com",
                DateOfBirth = new DateOnly(2026, 3, 31),
                Phone = "01123565952",
                Gender = (Gender)2,
                Address = new Address { Street = "Joaquin Clubs", City = "Altadena", BuildingNumber = 50 },
                Speciality = Speciality.Cardio,
                HireDate = new DateOnly(2026, 11, 12),
                CreatedAt = new DateOnly(2026, 11, 14),
                UpdatedAt = new DateOnly(2026, 11, 14),
                Deleted = null,
                IsDeleted = true
            },
            new()
            {
                Name = "felicia robel",
                Email = "fakedata52397@gmail.com",
                DateOfBirth = new DateOnly(2026, 7, 2),
                Phone = "01154262263",
                Gender = (Gender)2,
                Address = new Address { Street = "Jackie Views", City = "Hamilton", BuildingNumber = 263 },
                Speciality = Speciality.Strength,
                HireDate = new DateOnly(2026, 11, 12),
                CreatedAt = new DateOnly(2026, 11, 14),
                UpdatedAt = new DateOnly(2026, 11, 14),
                Deleted = null,
                IsDeleted = true
            }
        };

        await dbContext.Users.AddRangeAsync(trainers);
        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedHealthRecordsAsync(GymDbContext dbContext)
    {
        if (await dbContext.HealthRecords.IgnoreQueryFilters().AnyAsync())
            return;

        var membersByEmail = await dbContext.Users
            .IgnoreQueryFilters()
            .OfType<Member>()
            .ToDictionaryAsync(m => m.Email, m => m.Id);

        var records = new List<HealthRecord>();

        void AddIfMemberExists(string email, decimal height, decimal weight, string bloodType, string? notes, DateOnly createdAt, DateOnly? updatedAt, bool isDeleted)
        {
            if (membersByEmail.TryGetValue(email, out var memberId))
            {
                records.Add(new HealthRecord
                {
                    Height = height,
                    Weight = weight,
                    BloodType = BloodType.ABNegative,
                    Notes = notes,
                    MemberId = memberId,
                    CreatedAt = createdAt,
                    UpdatedAt = updatedAt,
                    Deleted = null,
                    IsDeleted = isDeleted
                });
            }
        }

        AddIfMemberExists("ay3220187@gmail.com", 180.00m, 80.00m, "APositive", null, new DateOnly(2026, 11, 4), new DateOnly(2026, 11, 18), true);
        AddIfMemberExists("ahmed333@gmail.com", 180.00m, 96.00m, "APositive", null, new DateOnly(2026, 11, 4), null, false);
        AddIfMemberExists("fakedata83693@gmail.com", 160.00m, 55.00m, "ABPositive", "374", new DateOnly(2026, 11, 15), new DateOnly(2026, 11, 18), true);
        AddIfMemberExists("fakedata56786@gmail.com", 175.00m, 65.00m, "APositive", null, new DateOnly(2026, 11, 17), null, false);
        AddIfMemberExists("fakedata54027@gmail.com", 157.00m, 60.00m, "ABNegative", "93", new DateOnly(2026, 11, 18), new DateOnly(2026, 11, 18), true);
        AddIfMemberExists("fakedata45029@gmail.com", 180.00m, 50.00m, "ABNegative", "8", new DateOnly(2026, 11, 18), new DateOnly(2026, 11, 18), true);
        AddIfMemberExists("fakedata31604@gmail.com", 167.00m, 80.00m, "ABNegative", "239", new DateOnly(2026, 11, 19), null, false);
        AddIfMemberExists("fakedata75681@gmail.com", 180.00m, 74.00m, "BPositive", "637", new DateOnly(2026, 11, 19), null, false);
        AddIfMemberExists("fakedata567860@gmail.com", 166.00m, 80.00m, "ABNegative", "509", new DateOnly(2026, 11, 19), new DateOnly(2026, 11, 19), true);

        await dbContext.HealthRecords.AddRangeAsync(records);
        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedMemberShipsAsync(GymDbContext dbContext)
    {
        if (await dbContext.MemberShip.IgnoreQueryFilters().AnyAsync())
            return;

        var member = await dbContext.Users.IgnoreQueryFilters().OfType<Member>().FirstOrDefaultAsync(m => m.Email == "ay3220187@gmail.com");
        var plan = await dbContext.Plans.IgnoreQueryFilters().FirstOrDefaultAsync(p => p.Name == "Standard Plan");

        if (member is null || plan is null)
            return;

        await dbContext.MemberShip.AddAsync(new MemberShip
        {
            MemberId = member.Id,
            PlanId = plan.Id,
            StartDate = new DateOnly(2026, 11, 2),
            EndDate = new DateOnly(2027, 1, 2),
            CreatedAt = new DateOnly(2026, 11, 2),
            UpdatedAt = null,
            Deleted = null,
            IsDeleted = false
        });

        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedSessionsAsync(GymDbContext dbContext)
    {
        if (await dbContext.Sessions.IgnoreQueryFilters().AnyAsync())
            return;

        var trainerCody = await dbContext.Users.IgnoreQueryFilters().OfType<Trainer>().FirstOrDefaultAsync(t => t.Email == "fakedata93380@gmail.com");
        var trainerSaige = await dbContext.Users.IgnoreQueryFilters().OfType<Trainer>().FirstOrDefaultAsync(t => t.Email == "fakedata94990@gmail.com");

        var categoryFlexibility = await dbContext.Categories.IgnoreQueryFilters().FirstOrDefaultAsync(c => c.Name == "Flexibility");
        var categoryCardio = await dbContext.Categories.IgnoreQueryFilters().FirstOrDefaultAsync(c => c.Name == "Cardio");

        var sessions = new List<Session>();

        if (trainerCody is not null && categoryFlexibility is not null)
        {
            sessions.Add(new Session
            {
                Description = "GeneralFitness",
                Capacity = 10,
                StartTime = new DateTime(2026, 11, 30, 9, 0, 0),
                EndTime = new DateTime(2026, 12, 10, 10, 30, 0),
                TrainerId = trainerCody.Id,
                CategoryId = categoryFlexibility.Id,
                CreatedAt = new DateOnly(2026, 11, 30),
                UpdatedAt = null,
                Deleted = null,
                IsDeleted = false
            });
        }

        if (trainerSaige is not null && categoryCardio is not null)
        {
            sessions.Add(new Session
            {
                Description = "jj",
                Capacity = 10,
                StartTime = new DateTime(2026, 11, 11, 14, 0, 0),
                EndTime = new DateTime(2026, 11, 12, 15, 30, 0),
                TrainerId = trainerSaige.Id,
                CategoryId = categoryCardio.Id,
                CreatedAt = new DateOnly(2026, 11, 11),
                UpdatedAt = null,
                Deleted = null,
                IsDeleted = false
            });
        }

        await dbContext.Sessions.AddRangeAsync(sessions);
        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedBookingsAsync(GymDbContext dbContext)
    {
        if (await dbContext.Bookings.IgnoreQueryFilters().AnyAsync())
            return;

        var session = await dbContext.Sessions.IgnoreQueryFilters().FirstOrDefaultAsync(s => s.Description == "GeneralFitness");
        var member = await dbContext.Users.IgnoreQueryFilters().OfType<Member>().FirstOrDefaultAsync(m => m.Email == "fakedata56786@gmail.com");

        if (session is null || member is null)
            return;

        await dbContext.Bookings.AddAsync(new Booking
        {
            Date = new DateOnly(2026, 11, 19),
            IsAttended = true,
            SessionId = session.Id,
            MemberId = member.Id,
            CreatedAt = new DateOnly(2026, 11, 19),
            UpdatedAt = null,
            Deleted = null,
            IsDeleted = false
        });

        await dbContext.SaveChangesAsync();
    }
}
