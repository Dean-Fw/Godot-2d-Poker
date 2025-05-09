using Godot;

public partial class BlindsManager : Node
{

    [Export] public int Ante { get; private set; }

    [Export] private double anteIncreaseRate;

    public RoundBlinds Blinds { get; private set; } = null!;
    // Possibly not the cleaneast implementation but works for now :)
    public void SetBlinds(int playerCount)
    {
        // if the blinds have not been set then we start from the beginning of the playercount
        if (Blinds == null)
        {
            Blinds = new RoundBlinds();
            Blinds.Dealer = 0;
            Blinds.SmallBlind = GetPositionOfNextBlind(Blinds.Dealer, playerCount);
            Blinds.BigBlind = GetPositionOfNextBlind(Blinds.SmallBlind, playerCount);
            Blinds.UnderTheGun = GetPositionOfNextBlind(Blinds.BigBlind, playerCount);
        }

        // Otherwise we want to shift the blinds one index
        Blinds.Dealer = GetPositionOfNextBlind(Blinds.Dealer, playerCount);
        Blinds.SmallBlind = GetPositionOfNextBlind(Blinds.Dealer, playerCount);
        Blinds.BigBlind = GetPositionOfNextBlind(Blinds.SmallBlind, playerCount);
        Blinds.UnderTheGun = GetPositionOfNextBlind(Blinds.BigBlind, playerCount);
    }

    private int GetPositionOfNextBlind(int blind, int playerCount)
    {
        return blind + 1 > playerCount - 1 ? 0 : blind + 1;

    }
}
