using Godot;

public partial class Pot : Control
{
    [Export] private Label ChipCount = null!;

    public int PotValue { get; private set; }

    public void AddChips(int value)
    {
        PotValue += value;

        ChipCount.Text = PotValue.ToString();
    }
}
