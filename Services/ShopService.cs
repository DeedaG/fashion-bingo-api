public class ShopService
{
    private readonly AppDbContext _context;

    public ShopService(AppDbContext context)
    {
        _context = context;
    }

    public void SaveEconomy(Player player)
    {
        _context.Player.Update(player);
        _context.SaveChanges();
    }
}
