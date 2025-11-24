using HotelSystem.Controllers;
using HotelSystem.Data;
using HotelSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HotelSystem.Tests
{
    public class RoomsControllerTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);

            // Seed data
            context.RoomTypes.Add(new RoomType
            {
                RoomTypeID = 1,
                TypeName = "Стандарт",
                Capacity = 2,
                BasePrice = 5000
            });

            context.Rooms.AddRange(
                new Room { RoomID = 1, RoomNumber = "101", Floor = 1, RoomTypeID = 1, Status = "Available" },
                new Room { RoomID = 2, RoomNumber = "202", Floor = 2, RoomTypeID = 1, Status = "Booked" }
            );

            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task Index_ReturnsViewWithRooms()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new RoomsController(context);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Room>>(viewResult.Model);
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public async Task Details_ExistingId_ReturnsViewWithRoom()
        {
            var context = GetInMemoryDbContext();
            var controller = new RoomsController(context);

            var result = await controller.Details(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Room>(viewResult.Model);
            Assert.Equal("101", model.RoomNumber);
        }

        [Fact]
        public async Task Details_NonExistingId_ReturnsNotFound()
        {
            var context = GetInMemoryDbContext();
            var controller = new RoomsController(context);

            var result = await controller.Details(999);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_PostValidRoom_RedirectsToIndex()
        {
            var context = GetInMemoryDbContext();
            var controller = new RoomsController(context);

            var newRoom = new Room
            {
                RoomNumber = "303",
                Floor = 3,
                RoomTypeID = 1,
                Status = "Available"
            };

            var result = await controller.Create(newRoom);

            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);

            // Проверяем, что комната реально добавилась
            Assert.Equal(3, await context.Rooms.CountAsync());
        }
    }
}