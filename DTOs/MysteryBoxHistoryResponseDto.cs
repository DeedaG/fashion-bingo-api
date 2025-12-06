using System.Collections.Generic;

public class MysteryBoxHistoryResponseDto
{
    public List<MysteryBoxHistoryEntryDto> Entries { get; set; } = new();
    public CoinSourceBreakdownDto CoinSources { get; set; } = new();
}
