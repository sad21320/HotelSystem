using HotelSystem.Controllers;
using HotelSystem.Data;
using HotelSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace HotelSystem.Tests
{
    public class GuestsControllerTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);
            context.Guests.AddRange(
                new Guest
                {
                    GuestID = 1,
                    FirstName = "Иван",
                    LastName = "Иванов",
                    PassportSeries = "1234",
                    PassportNumber = "567890",
                    PhoneNumber = "+79991234567",
                    Email = "ivan@example.com"
                },
                new Guest
                {
                    GuestID = 2,
                    FirstName = "Мария",
                    LastName = "Петрова",
                    PhoneNumber = "+79997654321",
                    Email = "maria@example.com",
                    PassportSeries = "4321",
                    PassportNumber = "098765"
                }
            );
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task Index_ReturnsViewWithGuests()
        {
            var context = GetInMemoryDbContext();
            var controller = new GuestsController(context);

            var result = await controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Guest>>(viewResult.Model);
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public async Task Create_PostValidGuest_RedirectsToIndex()
        {
            var context = GetInMemoryDbContext();
            var controller = new GuestsController(context);

            var guest = new Guest
            {
                FirstName = "Алексей",
                LastName = "Смирнов",
                PassportSeries = "1111",
                PassportNumber = "222333",
                PhoneNumber = "+79001234567890",
                Email = "alex@test.ru"
            };

            var result = await controller.Create(guest);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
            Assert.Equal(3, await context.Guests.CountAsync());
        }

        [Fact]
        public async Task DeleteConfirmed_ExistingId_RemovesGuest()
        {
            var context = GetInMemoryDbContext();
            var controller = new GuestsController(context);

            await controller.DeleteConfirmed(1);

            Assert.Single(context.Guests); // осталось 1 из 2
        }

        [Fact]
        public async Task Details_ValidId_ReturnsGuest()
        {
            var context = GetInMemoryDbContext();
            var controller = new GuestsController(context);

            var result = await controller.Details(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Guest>(viewResult.Model);
            Assert.Equal("Иванов", model.LastName);
        }
    }
}