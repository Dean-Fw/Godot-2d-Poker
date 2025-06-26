using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class TableCenter : Node
{
    [Export] private PackedScene potScene = null!;
    [Export] public CommunityCards CommunityCards { get; private set; } = null!;

    public Pot PotInstance { get; private set; } = null!;

    public void CollectChips(List<Player> players)
    {
        var potTotal = 0;

        foreach (var player in players)
        {
            if (!player.RoundInformation.GetChildren().ToList().Exists(c => c is Bet))
                continue;

            potTotal += player.CurrentBet.Value;

            player.CurrentBet.ClearBet();

            if (PotInstance == null)
            {
                PotInstance = potScene.Instantiate<Pot>();
                AddChild(PotInstance);
            }

        }

        PotInstance.AddChips(potTotal);
    }

    public void GiveChipsTo(Player winner)
    {
        winner.AddChips(PotInstance.PotValue);

        PotInstance.QueueFree();
        PotInstance = null!;
    }
}
