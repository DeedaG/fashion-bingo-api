public class Player
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Coins { get; set; }
    public List<ClothingItem> Closet { get; set; } = new List<ClothingItem>();
    public Mannequin CurrentMannequin { get; set; } = new Mannequin();
}
