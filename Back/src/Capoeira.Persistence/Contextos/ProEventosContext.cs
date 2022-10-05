using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Capoeira.Domain;
using Capoeira.Domain.Identity;

namespace Capoeira.Persistence.Contextos
{
    public class CapoeiraContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, 
                                                        UserRole, IdentityUserLogin<int>, 
                                                        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public CapoeiraContext(DbContextOptions<CapoeiraContext> options) : base(options) { }
        
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Mestre> Mestres {get; set; }
        public DbSet<Filiado> Filiados {get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(userRole => {
                userRole.HasKey(ur => new {ur.UserId, ur.RoleId});

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<Evento>();
            modelBuilder.Entity<Mestre>();
            modelBuilder.Entity<Filiado>();
        }
    }
}