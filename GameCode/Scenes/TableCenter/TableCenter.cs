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

            player.RoundInformation.RemoveChild(player.CurrentBet);

            if (PotInstance == null)
            {
                PotInstance = potScene.Instantiate<Pot>();
                AddChild(PotInstance);
            }

        }

        GD.Print("POT COLLECTED");

        PotInstance.AddChips(potTotal);
    }
}
