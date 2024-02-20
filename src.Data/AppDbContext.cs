using Microsoft.EntityFrameworkCore;
using src.Core;
using src.Core.Domains;
using System.Diagnostics;
using System;
using Newtonsoft.Json;

namespace src.Data
{
    public sealed class AppDbContext : DbContext, IDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.SetCommandTimeout(TimeSpan.FromMinutes(30));
           
        }
        public DbSet<Domain> Domains { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Campus> Campus { get; set; }
        public DbSet<grade> grade { get; set; }
        public DbSet<EmailConfig> EmailConfigs { get; set; }
        public DbSet<ParentStudent> ParentStudents { get; set; }
        public DbSet<UserCampus> UserCampus { get; set; }
        public DbSet<Mandator> Mandators { get; set; }
        public DbSet<AuthorizedPerson> AuthorizedPersons { get; set; }
        public DbSet<PowerOfAttorney> PowerOfAttorneys { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<ChangeRequest> ChangeRequest { get; set; }
        public DbSet<Applications> Applications { get; set; }
        public DbSet<ChangeRequestAttachment> ChangeRequestAttachment { get; set; }
        public DbSet<ChangeRequestFlow> ChangeRequestFlow { get; set; }
        public DbSet<ChangeRequestState> ChangeRequestsState { get; set; }
        public DbSet<ChangeRequestType> ChangeRequestType { get; set; }
        public DbSet<Priority> Priority { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<ChangeRequestUserUpdate> ChangeRequestUserUpdate { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships here using Fluent API

            modelBuilder.Entity<ChangeRequest>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("uniqueidentifier");
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
                entity.Property(e => e.PriorityExpectedDate).HasColumnType("datetime");
                entity.HasOne(cr => cr.ChangeRequestFlow).WithOne(cf => cf.ChangeRequest).HasForeignKey<ChangeRequestFlow>(cf => cf.Id);

                entity.HasOne(c => c.Applications)
                .WithMany(a => a.ChangeRequests)
                .HasForeignKey(c => c.ApplicationId);

                entity.HasOne(c => c.ChangeRequestTypes)
                .WithMany(a => a.ChangeRequests)
                .HasForeignKey(c => c.ChangeRequestTypeId);

                entity.HasOne(c => c.Prioritys)
                .WithMany(a => a.ChangeRequests)
                .HasForeignKey(c => c.PriorityId);

                entity.HasOne(c => c.Users)
                 .WithMany(a => a.ChangeRequest)
                 .HasForeignKey(c => c.CreatedUserId);

                entity.HasMany(c => c.ChangeRequestAttachment)
                 .WithOne(a => a.ChangeRequest)
                 .HasPrincipalKey(a => a.Id);
                 
            });



            modelBuilder.Entity<ChangeRequestFlow>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(cf => cf.ChangeRequestState)
                    .WithMany(a => a.ChangeRequestFlow)
                    .HasForeignKey(cf => cf.ChangeRequestId);
               
                entity.HasOne(cf => cf.User)
                    .WithMany(a => a.ChangeRequestFlow)
                    .HasForeignKey(cf => cf.UpdatedUserId);

                entity.HasMany(c => c.Comments)
                    .WithOne(a => a.ChangeRequestFlow)
                    .HasPrincipalKey(a => a.Id);

                entity.HasMany(c => c.ChangeRequestUserUpdate)
                    .WithOne(c => c.ChangeRequestFlow)
                    .HasPrincipalKey(c => c.Id);

            });

            modelBuilder.Entity<ChangeRequestUserUpdate>(entity =>
            {                
                entity.HasOne(a => a.ChangeRequestFlow).WithMany(a => a.ChangeRequestUserUpdate) .HasForeignKey(a => a.RequestId);
                entity.HasOne(a => a.User).WithMany(c => c.ChangeRequestUserUpdate).HasForeignKey(a => a.UserId);
            });

            modelBuilder.Entity<Comments>(entity => {
                entity.HasOne(a => a.ChangeRequestFlow)
                .WithMany(a => a.Comments)
                .HasForeignKey(a => a.ForeignId);

                entity.HasOne(a => a.User).WithMany(c => c.Comments).HasForeignKey(a => a.UserId);
                
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");              
            });

            modelBuilder.Entity<ChangeRequestState>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name);
            });

            modelBuilder.Entity<Applications>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name);
            });

            modelBuilder.Entity<ChangeRequestState>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name);
            });

            modelBuilder.Entity<ChangeRequestType>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name);
            });

            modelBuilder.Entity<Priority>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

            });

            modelBuilder.Entity<ChangeRequestAttachment>( entity =>
            {
               entity.HasOne(cra => cra.ChangeRequest)
                .WithMany(cr => cr.ChangeRequestAttachment)
                .HasForeignKey(cra => cra.ChangeRequestId)
                .OnDelete(DeleteBehavior.Cascade); 

            });

            modelBuilder.Entity<Domain>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<EmailConfig>(entity =>
            {
                entity.HasKey(e => e.configId);
                entity.HasOne(e => e.validate).WithMany().HasForeignKey(e => e.for_validate);
                entity.HasOne(e => e.sms_validate).WithMany().HasForeignKey(e => e.for_sms_validate);
                entity.HasOne(e => e.confirmation).WithMany().HasForeignKey(e => e.for_parent_confirmation);
                entity.HasOne(e => e.confirmation_success).WithMany().HasForeignKey(e => e.for_confirmation_success);
                entity.HasOne(e => e.email_not_yet_confirmed).WithMany().HasForeignKey(e => e.for_email_not_yet_confirmation);
                entity.HasOne(e => e.sms_not_yet_confirmed).WithMany().HasForeignKey(e => e.for_sms_not_yet_confirmation);
                entity.HasOne(e => e.confirmation_meal).WithMany().HasForeignKey(e => e.for_parent_confirmation_meal);
                entity.HasOne(e => e.confirmation_request_user).WithMany().HasForeignKey(e => e.for_confirmation_request_user);
                entity.HasOne(e => e.confirmation_user_in_charge).WithMany().HasForeignKey(e => e.for_confirmation_user_in_charge);
            });

            modelBuilder.Entity<EmailTemplate>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("uniqueidentifier");
                entity.Property(e => e.Body).IsRequired();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(200);

                
                
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.Property(e => e.Action).HasMaxLength(50);

                entity.Property(e => e.Application)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Controller).HasMaxLength(50);

                entity.Property(e => e.Identity).HasMaxLength(50);

                entity.Property(e => e.Level)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Logged).HasColumnType("datetime");

                entity.Property(e => e.Logger).HasMaxLength(250);

                entity.Property(e => e.Message).IsRequired();

                entity.Property(e => e.Referrer).HasMaxLength(250);

                entity.Property(e => e.Url).HasMaxLength(500);

                entity.Property(e => e.UserAgent).HasMaxLength(250);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<BaseCategory>(entity =>
            {
                entity.Property(b => b.Code);
                entity.Property(b => b.CategoryName);
            });
            modelBuilder.Entity<Setting>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Value).IsRequired();
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PK_UserRoles");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_UserRoles_Roles");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserRoles_Users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastLoginDate).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ParentStudent>().HasKey(pc => new { pc.ParentId, pc.StudentCode });
            modelBuilder.Entity<PowerOfAttorney>().HasKey(po => new { po.Id, po.MandatorId, po.AuthorizedPersonId });
            modelBuilder.Entity<UserCampus>().HasKey(u => new { u.Campuscode, u.UserId });

            modelBuilder.Entity<Student>()
             .HasMany(s => s.Meals)
             .WithOne()
             .HasForeignKey(meal => meal.StudentCode);

            modelBuilder.Entity<Meal>(entity =>
            {
                entity.HasKey(e => new { e.Id });
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");
                entity.Property(e => e.FromDate).HasColumnType("datetime");
                entity.Property(e => e.ToDate).HasColumnType("datetime");
            }
            );



        }



    }
}
