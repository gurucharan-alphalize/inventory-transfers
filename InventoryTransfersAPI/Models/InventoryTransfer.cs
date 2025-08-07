namespace InventoryTransfersAPI.Models
{
  public class Product
  {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
  }

  public class InventoryTransfer
  {
    public int Id { get; set; }
    public string FromLocation { get; set; } = string.Empty;
    public string ToLocation { get; set; } = string.Empty;
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }
    public string Status { get; set; } = "Pending";
    public DateTime TransferDate { get; set; }
  }
}