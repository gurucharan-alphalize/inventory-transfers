using InventoryTransfersAPI.Data;
using InventoryTransfersAPI.DTOs;
using InventoryTransfersAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryTransfersAPI.Services
{
  public class InventoryTransferService : IInventoryTransferService
  {
    private readonly AppDbContext _context;

    public InventoryTransferService(AppDbContext context) {
      _context = context;
    }

    public async Task<List<InventoryTransferDto>> GetAllAsync() {
      return await _context.InventoryTransfers.Include(t => t.Product).Select(t => new InventoryTransferDto {
        Id = t.Id,
        FromLocation = t.FromLocation,
        ToLocation = t.ToLocation,
        ProductId = t.ProductId,
        ProductName = t.Product.Name,
        Quantity = t.Quantity,
        Status = t.Status,
        TransferDate = t.TransferDate
      }).ToListAsync();
    }

    public async Task<InventoryTransferDto?> GetByIdAsync(int id) {
      var entity = await _context.InventoryTransfers.Include(t => t.Product).FirstOrDefaultAsync(t => t.Id == id);
      if (entity == null) return null;
      return new InventoryTransferDto {
        Id = entity.Id,
        FromLocation = entity.FromLocation,
        ToLocation = entity.ToLocation,
        ProductId = entity.ProductId,
        ProductName = entity.Product.Name,
        Quantity = entity.Quantity,
        Status = entity.Status,
        TransferDate = entity.TransferDate
      };
    }

    public async Task<InventoryTransferDto> CreateAsync(InventoryTransferCreateDto dto) {
      var entity = new InventoryTransfer {
        FromLocation = dto.FromLocation,
        ToLocation = dto.ToLocation,
        ProductId = dto.ProductId,
        Quantity = dto.Quantity,
        TransferDate = dto.TransferDate,
        Status = "Pending"
      };
      await _context.InventoryTransfers.AddAsync(entity);
      await _context.SaveChangesAsync();
      return await GetByIdAsync(entity.Id) ?? throw new System.Exception("Creation failed");
    }

    public async Task<bool> UpdateAsync(InventoryTransferUpdateDto dto) {
      var entity = await _context.InventoryTransfers.FindAsync(dto.Id);
      if (entity == null) return false;
      entity.FromLocation = dto.FromLocation;
      entity.ToLocation = dto.ToLocation;
      entity.ProductId = dto.ProductId;
      entity.Quantity = dto.Quantity;
      entity.Status = dto.Status;
      entity.TransferDate = dto.TransferDate;
      await _context.SaveChangesAsync();
      return true;
    }

    public async Task<bool> DeleteAsync(int id) {
      var entity = await _context.InventoryTransfers.FindAsync(id);
      if (entity == null) return false;
      _context.InventoryTransfers.Remove(entity);
      await _context.SaveChangesAsync();
      return true;
    }
  }
}