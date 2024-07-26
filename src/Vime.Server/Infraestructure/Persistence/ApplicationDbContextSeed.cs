using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vime.Server.Domain.Constants;
using Vime.Server.Entities;

namespace Vime.Server.Infraestructure.Persistence;

public static class ApplicationDbContextSeed
{
    public static void Seed(this ModelBuilder builder)
    {
        SeedRoles(builder);
    }


    private static void SeedRoles(this ModelBuilder builder)
    {
        builder.Entity<IdentityRole>().HasData(
        new IdentityRole { Id = "1", Name = Roles.User, NormalizedName = Roles.User.ToUpper() },
        new IdentityRole { Id = "2", Name = Roles.Mod, NormalizedName = Roles.Mod.ToUpper() },
        new IdentityRole { Id = "3", Name = Roles.Admin, NormalizedName = Roles.Admin.ToUpper() });

        builder.Entity<Room>().HasData(
            new Room {Id = 1, Title = "Sala General", Provider = 0, Leader = "Admin", VideoUrl = "https://www.youtube.com/watch?v=Ata9cSC2WpM" } 
        );
    }


}