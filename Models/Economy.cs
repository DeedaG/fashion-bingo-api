public class Economy
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int Coins { get; set; }
    public int Gems { get; set; }
    public int Energy { get; set; }
}
