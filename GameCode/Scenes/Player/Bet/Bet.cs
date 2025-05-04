using Godot;

public partial class Bet : Node
{
    [Export] private Label betLabel = null!;

    public int Value = 0;

    public override void _Ready()
    {
        betLabel.Text = Value.ToString();
    }

    public void AddChips(int amount)
    {
        Value += amount;
        betLabel.Text = Value.ToString();
    }
}
