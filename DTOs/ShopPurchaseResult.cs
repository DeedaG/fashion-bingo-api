public class ShopPurchaseResult
{
    public Economy Economy { get; set; }
    public ClothingItem Item { get; set; }

    public ShopPurchaseResult(Economy economy, ClothingItem item)
    {
        Economy = economy;
        Item = item;
    }
}
