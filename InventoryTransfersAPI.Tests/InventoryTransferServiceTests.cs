using InventoryTransfersAPI.Data;
using InventoryTransfersAPI.DTOs;
using InventoryTransfersAPI.Models;
using InventoryTransfersAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace InventoryTransfersAPI.Tests
{
  public class InventoryTransferServiceTests
  {
    private AppDbContext GetDbContext(string dbName) {
      var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseInMemoryDatabase(dbName)
        .Options;
      var context = new AppDbContext(options);
      if (!context.Products.Any()) {
        context.Products.Add(new Product { Name = "Test Product" });
        context.SaveChanges();
      }
      return context;
    }

    [Fact]
    public async Task CreateAsync_CreatesEntity() {
      var dbName = Guid.NewGuid().ToString();
      var context = GetDbContext(dbName);
      var service = new InventoryTransferService(context);
      var dto = new InventoryTransferCreateDto {
        FromLocation = "A",
        ToLocation = "B",
        ProductId = context.Products.First().Id,
        Quantity = 5,
        TransferDate = DateTime.UtcNow
      };
      var result = await service.CreateAsync(dto);
      Assert.NotNull(result);
      Assert.Equal(dto.FromLocation, result.FromLocation);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsEntities() {
      var dbName = Guid.NewGuid().ToString();
      var context = GetDbContext(dbName);
      context.InventoryTransfers.Add(new InventoryTransfer {
        FromLocation = "A",
        ToLocation = "B",
        ProductId = context.Products.First().Id,
        Quantity = 10,
        Status = "Pending",
        TransferDate = DateTime.UtcNow
      });
      context.SaveChanges();
      var service = new InventoryTransferService(context);
      var list = await service.GetAllAsync();
      Assert.NotEmpty(list);
    }

    [Fact]
    public async Task DeleteAsync_RemovesEntity() {
      var dbName = Guid.NewGuid().ToString();
      var context = GetDbContext(dbName);
      var service = new InventoryTransferService(context);
      var transfer = new InventoryTransfer {
        FromLocation = "A",
        ToLocation = "B",
        ProductId = context.Products.First().Id,
        Quantity = 5,
        Status = "Pending",
        TransferDate = DateTime.UtcNow
      };
      context.InventoryTransfers.Add(transfer);
      context.SaveChanges();
      var deleted = await service.DeleteAsync(transfer.Id);
      Assert.True(deleted);
      Assert.Null(await context.InventoryTransfers.FindAsync(transfer.Id));
    }
  }
}