using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RBACV2.Domain.Entities;
using RBACV2.Domain.Entities.PermissionsEntity;
using RBACV2.Domain.Entities.UserEntity;

namespace RBACV2.Infrastructure.Persistence.Context
{

    public interface IApplicationDbContext : IDbContext { }
    public class ApplicationDbContext : BaseDbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options
            ) : base(options)
        {

        }

        //UNCOMMENT WHEN AUTH IS READY
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
        //  IHttpContextAccessor context) : base(options, context)
        //{

        //}


        public DbSet<Applications> Applications { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<PermissionsToRoles> PermissionToRoles { get; set; }
        public DbSet<Roles> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PermissionsToRoles>()
                .HasOne(x => x.Role)
                .WithMany(x => x.PermissionsToRoles)
                .HasForeignKey(x => x.RoleId);

            modelBuilder.Entity<PermissionsToRoles>()
                            .HasOne(x => x.Permission)
                            .WithMany(x => x.PermissionsToRoles)
                            .HasForeignKey(x => x.PermissionId);


            base.OnModelCreating(modelBuilder);
        }
    }


}
