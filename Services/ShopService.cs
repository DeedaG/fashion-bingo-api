using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class ShopService
{
    private readonly AppDbContext _context;
    private readonly ClosetService _closetService;

    private static readonly List<ShopOffer> Offers = new()
    {
        new ShopOffer
        {
            Id = "sun-dress",
            Name = "Sun Dress",
            Type = "Dress",
            Style = "Casual",
            Rarity = "Rare",
            Sprite = "sun-dress.png",
            CostCoins = 250,
            Description = "Limited summer drop straight from the runway.",
            PrimaryColor = "#fef1d4"
        },
        new ShopOffer
        {
            Id = "black-top",
            Name = "Black Top",
            Type = "Blouse",
            Style = "Streetwear",
            Rarity = "Common",
            Sprite = "blouse-black.png",
            CostCoins = 180,
            Description = "Staple piece for every capsule closet.",
            PrimaryColor = "#101010"
        },
        new ShopOffer
        {
            Id = "gold-skirt",
            Name = "Gold Skirt",
            Type = "Pants",
            Style = "Sportswear",
            Rarity = "Epic",
            Sprite = "gold-skirt.png",
            CostCoins = 320,
            Description = "Taffeta shimmer with a perfect swoosh.",
            PrimaryColor = "#fcd864"
        },
        new ShopOffer
        {
            Id = "doc-martens",
            Name = "Doc Martens",
            Type = "Shoes",
            Style = "Boots",
            Rarity = "Rare",
            Sprite = "doc-marten.png",
            CostCoins = 210,
            Description = "Street-ready boots for your winter looks.",
            PrimaryColor = "#231f20"
        }
    };

    public ShopService(AppDbContext context, ClosetService closetService)
    {
        _context = context;
        _closetService = closetService;
    }

    public IEnumerable<ShopOffer> GetOffers()
    {
        return Offers;
    }

    public ShopPurchaseResult PurchaseOffer(Guid playerId, string offerId)
    {
        if (string.IsNullOrWhiteSpace(offerId))
        {
            throw new ArgumentException("offerId is required", nameof(offerId));
        }

        var offer = Offers.FirstOrDefault(o => string.Equals(o.Id, offerId, StringComparison.OrdinalIgnoreCase));
        if (offer == null)
        {
            throw new InvalidOperationException("Offer not found.");
        }

        var player = _context.Player
                              .Include(p => p.Economy)
                              .FirstOrDefault(p => p.Id == playerId);

        if (player == null)
        {
            throw new InvalidOperationException("Player not found.");
        }

        player.Economy ??= new Economy();

        if (player.Economy.Coins < offer.CostCoins)
        {
            throw new InvalidOperationException("Not enough coins for this purchase.");
        }

        player.Economy.Coins -= offer.CostCoins;
        _context.SaveChanges();

        var clothing = new ClothingItem
        {
            Name = offer.Name,
            Type = offer.Type,
            Style = offer.Style,
            Rarity = offer.Rarity,
            ImageUrl = $"/clothing/{offer.Sprite}",
            PrimaryColor = offer.PrimaryColor ?? "#ffffff"
        };

        var addedItem = _closetService.AddItemToCloset(playerId, clothing);
        return new ShopPurchaseResult(player.Economy, addedItem);
    }
}
