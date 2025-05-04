using Godot;

public partial class PotManager : Node
{
    [Export] private HBoxContainer tableCenter = null!;
    [Export] private PackedScene potScene = null!;
    [Export] private PlayerParent playerParent = null!;

    public Pot Pot { get; private set; } = null!;

    private void HandleBet(int value)
    {
        if (!tableCenter.ContainsChildOfType<Pot>())
        {
            Pot = potScene.Instantiate<Pot>();
            tableCenter.AddChild(Pot);
        }

        Pot.AddChips(value);
    }

}
