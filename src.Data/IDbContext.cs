using Microsoft.EntityFrameworkCore;
using src.Core;
using src.Core.Domains;
using System;
using System.Collections.Generic;

namespace src.Data
{
    public interface IDbContext
    {
        DbSet<Domain> Domains { get; set; }
        DbSet<EmailTemplate> EmailTemplates { get; set; }
        DbSet<Log> Logs { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Setting> Settings { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<UserRole> UserRoles { get; set; }
        DbSet<Student> Students { get; set; }
        DbSet<EmailConfig> EmailConfigs { get; set; }
        DbSet<ParentStudent> ParentStudents { get; set; }
        DbSet<Campus> Campus { get; set; }
        DbSet<UserCampus> UserCampus { get; set; }
        DbSet<Mandator> Mandators { get; set; }
        DbSet<AuthorizedPerson> AuthorizedPersons { get; set; }
        DbSet<PowerOfAttorney> PowerOfAttorneys { get; set; }
        DbSet<Parent> Parents { get; set; }

        DbSet<grade> grade { get; set; }

        DbSet<Meal> Meals { get; set; }

        DbSet<ChangeRequest> ChangeRequest { get; set; }
        DbSet<Applications> Applications { get; set; }
        DbSet<ChangeRequestAttachment> ChangeRequestAttachment { get; set; }
        DbSet<ChangeRequestFlow> ChangeRequestFlow { get; set; }
        DbSet<ChangeRequestState> ChangeRequestsState { get; set; }
        DbSet<ChangeRequestType> ChangeRequestType { get; set; }
        DbSet<Priority> Priority { get; set; }
        DbSet<Comments> Comments { get; set; }
        DbSet<ChangeRequestUserUpdate> ChangeRequestUserUpdate { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        int SaveChanges();
    
    }
}
