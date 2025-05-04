using Godot;

public partial class PotManager : Node
{
    [Export] private HBoxContainer tableCenter = null!;
    [Export] private PackedScene potScene = null!;
    [Export] private RoundManager roundManager = null!;
    [Export] private PlayerParent playerParent = null!;

    public Pot Pot { get; private set; } = null!;

    public override void _Ready()
    {
        roundManager.RoundEnd += (RoundPhase _) => HandleRoundEnd();
    }

    private void HandleRoundEnd()
    {
        var potTotal = 0;

        foreach (var player in playerParent.Players)
        {
            potTotal += player.CurrentBet.Value;
            player.AddChipsToPot();
        }

        if (!tableCenter.ContainsChildOfType<Pot>())
        {
            Pot = potScene.Instantiate<Pot>();
            tableCenter.AddChild(Pot);
        }

        Pot.AddChips(potTotal);
    }

}
