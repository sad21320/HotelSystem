using HotelSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // ТВОИ СУЩЕСТВУЮЩИЕ ТАБЛИЦЫ — НО МЫ ИХ ИГНОРИРУЕМ ДЛЯ МИГРАЦИЙ!
        public DbSet<Guest> Guests => Set<Guest>();
        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<RoomType> RoomTypes => Set<RoomType>();
        public DbSet<Booking> Bookings => Set<Booking>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ЭТО САМОЕ ГЛАВНОЕ — ГОВОРИМ EF: "НЕ СОЗДАВАЙ ЭТИ ТАБЛИЦЫ, ОНИ УЖЕ ЕСТЬ!"
            modelBuilder.Entity<Guest>().ToTable("Guests", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<Room>().ToTable("Rooms", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<RoomType>().ToTable("RoomTypes", t => t.ExcludeFromMigrations());
            modelBuilder.Entity<Booking>().ToTable("Bookings", t => t.ExcludeFromMigrations());

            // А таблицы Identity — создаём как обычно
            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
            {
                b.ToTable("AspNetUsers");
            });
            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
            {
                b.ToTable("AspNetRoles");
            });
            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
            {
                b.ToTable("AspNetUserRoles");
            });
            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
            {
                b.ToTable("AspNetUserClaims");
            });
            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
            {
                b.ToTable("AspNetUserLogins");
            });
            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
            {
                b.ToTable("AspNetUserTokens");
            });
            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
            {
                b.ToTable("AspNetRoleClaims");
            });
        }
    }
}