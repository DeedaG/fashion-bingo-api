public class CreatePlayerRequest
{
    public Guid PlayerId { get; set; }
    public string? Name { get; set; }
    public bool IsPremium { get; set; }
}
