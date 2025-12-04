using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class ClothingCatalog
{
    private static readonly List<Palette> Palettes = new()
    {
        new("Sunset Circuit", "#ff8a5c", "#ffb86f", "#ffdca1", "#ffeccf"),
        new("Midnight Harbor", "#1c2a49", "#284c7c", "#3d74b5", "#89a8d4"),
        new("Urban Jungle", "#2d4739", "#3f6b46", "#79ad5c", "#c7df85"),
        new("Cosmic Rose", "#5d2d5e", "#8e3e63", "#c75b7a", "#fcb1a1")
    };

    private static readonly List<ClothingTemplate> Templates = new()
    {
        new("Minimal Grid Tee", "Shirt", "Streetwear", "Common"),
        new("Everyday Taper Joggers", "Pants", "Athleisure", "Common"),
        new("Sketchbook Beanie", "Hat", "Casual", "Common"),
        new("Halftone Wrap Blouse", "Shirt", "Boho", "Rare"),
        new("Outline Rush Sneakers", "Shoes", "Athleisure", "Rare"),
        new("Gallery Pleat Trousers", "Pants", "Resort", "Rare"),
        new("Contour Blazer Shell", "Shirt", "Evening", "Epic"),
        new("Vector Runner Boots", "Shoes", "Streetwear", "Epic"),
        new("Orbit Line Visor", "Hat", "Futuristic", "Epic"),
        new("Halo Wire Necklace", "Accessory", "Runway", "Legendary"),
        new("Monoline Strap Heels", "Shoes", "Runway", "Legendary"),
        new("Feather Crest Fascinator", "Hat", "Couture", "Legendary")
    };

    private static readonly Dictionary<string, Func<string, string>> OutlineByType = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Shirt"] = color => SvgData(BuildShirtSvg(color)),
        ["Pants"] = color => SvgData(BuildPantsSvg(color)),
        ["Shoes"] = color => SvgData(BuildShoesSvg(color)),
        ["Hat"] = color => SvgData(BuildHatSvg(color)),
        ["Accessory"] = color => SvgData(BuildAccessorySvg(color))
    };

    public static ClothingItem CreateRandomItem(string rarity, Random rng)
    {
        var pool = Templates
            .Where(t => string.Equals(t.Rarity, rarity, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (pool.Count == 0)
        {
            pool = Templates;
        }

        var template = pool[rng.Next(pool.Count)];
        var palette = Palettes[rng.Next(Palettes.Count)];
        var color = palette.GetColorForType(template.Type, rng);
        var imageUrl = OutlineByType.TryGetValue(template.Type, out var outline)
            ? outline(color)
            : OutlineByType["Accessory"](color);

        return new ClothingItem
        {
            Id = Guid.NewGuid(),
            Name = template.Name,
            Type = template.Type,
            Style = template.Style,
            Rarity = template.Rarity,
            ImageUrl = imageUrl
        };
    }

    private static string SvgData(string svg)
    {
        return $"data:image/svg+xml;base64,{Convert.ToBase64String(Encoding.UTF8.GetBytes(svg))}";
    }

    private record ClothingTemplate(
        string Name,
        string Type,
        string Style,
        string Rarity);

    private record Palette(string Name, string Primary, string Secondary, string Accent, string Highlight)
    {
        public string GetColorForType(string type, Random rng)
        {
            return type.ToLower() switch
            {
                "shirt" => Primary,
                "pants" => Secondary,
                "shoes" => Accent,
                "hat" => Highlight,
                "accessory" => rng.Next(2) == 0 ? Accent : Highlight,
                _ => Primary
            };
        }
    }

    private static string BuildShirtSvg(string baseColor)
    {
        var highlight = AdjustColor(baseColor, 0.35);
        var shadow = AdjustColor(baseColor, -0.25);
        return $"<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 160 160'><path d='M32 48L74 28l12-6 12 6 42 20-6 24-18 6v80H42V90l-18-6z' fill='{baseColor}' stroke='{shadow}' stroke-width='5' stroke-linejoin='round'/><path d='M55 58l50 36' stroke='{shadow}' stroke-width='4' opacity='.5'/><path d='M60 96c12 8 28 8 40 0v44H60z' fill='{highlight}' opacity='.35'/></svg>";
    }

    private static string BuildPantsSvg(string baseColor)
    {
        var highlight = AdjustColor(baseColor, 0.25);
        var shadow = AdjustColor(baseColor, -0.2);
        return $"<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 160 160'><path d='M48 20h64l10 40-16 78-16-8-8-60-8 60-16 8-16-78z' fill='{baseColor}' stroke='{shadow}' stroke-width='5' stroke-linejoin='round'/><path d='M48 60h64' stroke='{shadow}' stroke-width='4'/><path d='M65 28h30v18H65z' fill='{highlight}' opacity='.4'/></svg>";
    }

    private static string BuildShoesSvg(string baseColor)
    {
        var highlight = AdjustColor(baseColor, 0.3);
        var shadow = AdjustColor(baseColor, -0.35);
        return $"<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 160 160'><path d='M38 102c20-8 32-4 48 8l20 5v15H30c-6 0-11-5-11-11v-7z' fill='{baseColor}' stroke='{shadow}' stroke-width='5' stroke-linejoin='round'/><path d='M46 101l2-8h34l2 9' stroke='{shadow}' stroke-width='4'/><path d='M44 113c15-5 30-5 45 0' stroke='{highlight}' stroke-width='4'/><path d='M42 118h52' stroke='{shadow}' stroke-width='3'/></svg>";
    }

    private static string BuildHatSvg(string baseColor)
    {
        var highlight = AdjustColor(baseColor, 0.35);
        var shadow = AdjustColor(baseColor, -0.2);
        return $"<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 160 160'><ellipse cx='80' cy='120' rx='65' ry='20' fill='{baseColor}' stroke='{shadow}' stroke-width='5'/><path d='M40 118c0-32 20-58 40-58s40 26 40 58' fill='{baseColor}' stroke='{shadow}' stroke-width='5' stroke-linejoin='round'/><path d='M48 100h64' stroke='{highlight}' stroke-width='4'/></svg>";
    }

    private static string BuildAccessorySvg(string baseColor)
    {
        var highlight = AdjustColor(baseColor, 0.4);
        return $"<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 160 160'><path d='M35 70q45 55 90 0' stroke='{baseColor}' stroke-width='6' fill='none' stroke-linecap='round'/><path d='M48 95q32 32 64 0' stroke='{highlight}' stroke-width='5' fill='none' stroke-linecap='round'/><circle cx='80' cy='120' r='10' fill='{baseColor}'/><path d='M80 130v20' stroke='{baseColor}' stroke-width='5'/></svg>";
    }

    private static string AdjustColor(string hex, double amount)
    {
        hex = hex.TrimStart('#');
        if (hex.Length == 3)
        {
            hex = string.Concat(hex.Select(c => $"{c}{c}"));
        }
        var r = Convert.ToInt32(hex.Substring(0, 2), 16);
        var g = Convert.ToInt32(hex.Substring(2, 2), 16);
        var b = Convert.ToInt32(hex.Substring(4, 2), 16);

        if (amount >= 0)
        {
            r = (int)Math.Min(255, r + (255 - r) * amount);
            g = (int)Math.Min(255, g + (255 - g) * amount);
            b = (int)Math.Min(255, b + (255 - b) * amount);
        }
        else
        {
            var factor = 1 + amount;
            r = (int)Math.Max(0, r * factor);
            g = (int)Math.Max(0, g * factor);
            b = (int)Math.Max(0, b * factor);
        }

        return $"#{r:X2}{g:X2}{b:X2}";
    }
}
