using InventoryTransfersAPI.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryTransfersAPI.Services
{
  public interface IInventoryTransferService
  {
    Task<List<InventoryTransferDto>> GetAllAsync();
    Task<InventoryTransferDto?> GetByIdAsync(int id);
    Task<InventoryTransferDto> CreateAsync(InventoryTransferCreateDto dto);
    Task<bool> UpdateAsync(InventoryTransferUpdateDto dto);
    Task<bool> DeleteAsync(int id);
  }
}