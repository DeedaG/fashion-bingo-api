using System.ComponentModel.DataAnnotations.Schema;

public class Mannequin
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [NotMapped]
    public Dictionary<string, ClothingItem> EquippedItems { get; set; } = new Dictionary<string, ClothingItem>();
}
