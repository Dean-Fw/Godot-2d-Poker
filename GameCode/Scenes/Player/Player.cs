using Godot;

public partial class Player : Node
{
    [Export] public HBoxContainer HandContainer = null!;

    [Export] private Label chipCounter = null!;

    [Export] private PackedScene betScene = null!;
    [Export] private HBoxContainer roundInformation = null!;
    [Export] private Label BlindsLabel = null!;

    [Signal] public delegate void TurnEndEventHandler(Player player);

    public int ChipCount { get; private set; }

    public Bet CurrentBet { get; private set; } = null!;

    public override void _Ready()
    {
        chipCounter.Text = $"Chips: {ChipCount}";
        CurrentBet = betScene.Instantiate<Bet>();
    }

    public void AddDealerChip()
    {
        BlindsLabel.Text = "D";
    }

    public void AddChipsToPot()
    {
        roundInformation.RemoveChild(CurrentBet);
        CurrentBet.ClearBet();
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
        SetChipCount(ChipCount - value);
        CurrentBet.AddChips(value);

        if (!roundInformation.ContainsChildOfType<Bet>())
        {
            roundInformation.AddChild(CurrentBet);
        }

        // Signal a bet has been made
        MoveNext();
    }

}
