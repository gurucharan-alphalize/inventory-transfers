namespace InventoryTransfersAPI.DTOs
{
  public class InventoryTransferDto
  {
    public int Id { get; set; }
    public string FromLocation { get; set; } = string.Empty;
    public string ToLocation { get; set; } = string.Empty;
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime TransferDate { get; set; }
  }

  public class InventoryTransferCreateDto
  {
    public string FromLocation { get; set; } = string.Empty;
    public string ToLocation { get; set; } = string.Empty;
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime TransferDate { get; set; }
  }

  public class InventoryTransferUpdateDto
  {
    public int Id { get; set; }
    public string FromLocation { get; set; } = string.Empty;
    public string ToLocation { get; set; } = string.Empty;
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime TransferDate { get; set; }
  }
}