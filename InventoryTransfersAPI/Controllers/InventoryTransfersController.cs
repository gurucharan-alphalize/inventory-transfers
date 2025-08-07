using InventoryTransfersAPI.DTOs;
using InventoryTransfersAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryTransfersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryTransfersController : ControllerBase
    {
        private readonly IInventoryTransferService _service;

        public InventoryTransfersController(IInventoryTransferService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryTransferDto>>> GetAll() {
            var transfers = await _service.GetAllAsync();
            return Ok(transfers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryTransferDto>> GetById(int id) {
            var transfer = await _service.GetByIdAsync(id);
            if(transfer == null) return NotFound();
            return Ok(transfer);
        }

        [HttpPost]
        public async Task<ActionResult<InventoryTransferDto>> Create(InventoryTransferCreateDto dto) {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, InventoryTransferUpdateDto dto) {
            if(id != dto.Id) return BadRequest("Id mismatch");
            var updated = await _service.UpdateAsync(dto);
            if(!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var deleted = await _service.DeleteAsync(id);
            if(!deleted) return NotFound();
            return NoContent();
        }
    }
}