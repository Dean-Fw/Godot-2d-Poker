using Godot;

public partial class Pot : Control
{
    [Export] private Label ChipCount = null!;

    private int size;

    public int PotValue { get; private set; }

    public void AddChips(int value)
    {
        size += value;
        ChipCount.Text = size.ToString();
    }
}
