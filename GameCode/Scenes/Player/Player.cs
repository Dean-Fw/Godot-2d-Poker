using Godot;

public partial class Player : Node
{
    [Export] public HBoxContainer HandContainer;

    [Export] private Label chipCounter;
    [Export] private PackedScene pot;

    [Signal] public delegate void TurnEndEventHandler(Player player);
    [Signal] public delegate void BetEventHandler(int value);

    public int ChipCount { get; set; }
    public int CurrentBet { get; set; }

    public override void _Ready()
    {
        chipCounter.Text = $"Chips: {ChipCount}";
    }

    public void SetChipCount(int value)
    {
        ChipCount = value;
        chipCounter.Text = $"Chips: {ChipCount}";
    }

    public virtual void StartTurn()
    {
    }

    // Signal to end this players turn	
    protected void MoveNext()
    {
        EmitSignal(SignalName.TurnEnd, this);
    }

    protected void MakeBet(int value)
    {
        // Set players current bet to the bet value
        CurrentBet += value;
        SetChipCount(ChipCount - value);

        // Signal a bet has been made
        EmitSignal(SignalName.Bet, value);
        MoveNext();
    }

}
