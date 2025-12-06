public class CoinSourceBreakdownDto
{
    public int CoinsFromMysteryBoxes { get; set; }
    public int CoinsSpentOnMysteryBoxes { get; set; }
    public int NetFromMysteryBoxes => CoinsFromMysteryBoxes - CoinsSpentOnMysteryBoxes;
}
