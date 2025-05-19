using Godot;
using System.Collections.Generic;

public partial class Player : Control
{
    [Export] public HBoxContainer HandContainer = null!;

    [Export] private Label chipCounter = null!;

    [Export] private PackedScene betScene = null!;
    [Export] public HBoxContainer RoundInformation = null!;
    [Export] private Label BlindsLabel = null!;

    [Signal] public delegate void TurnEndEventHandler(Player player);

    public int ChipCount { get; private set; }

    public Bet CurrentBet { get; private set; } = null!;

    public bool Folded { get; private set; }

    public bool Acted { get; set; }

    public List<Blind> Blinds { get; set; } = [];

    protected int amountToCall;

    public override void _Ready()
    {
        chipCounter.Text = $"Chips: {ChipCount}";
        CurrentBet = betScene.Instantiate<Bet>();
    }

    public void AddDealerChip()
    {
        BlindsLabel.Text = "D";
    }


    public void GiveBlinds(int blind)
    {
        MoveChipsToTable(blind);
    }

    public void SetChipCount(int value)
    {
        ChipCount = value;
        chipCounter.Text = $"Chips: {ChipCount}";
    }

    public void AddChips(int value)
    {
        ChipCount += value;
        chipCounter.Text = $"Chips: {ChipCount}";
    }

    public virtual void StartTurn(int minimumBet)
    {
        amountToCall = minimumBet - CurrentBet.Value < 0 ? 0 : minimumBet - CurrentBet.Value;
    }

    // Signal to end this players turn	
    protected void MoveNext()
    {
        Acted = true;
        EmitSignal(SignalName.TurnEnd, this);
    }

    protected void MakeBet(int value)
    {
        if (value != 0)
            MoveChipsToTable(value);

        // Signal a bet has been made
        MoveNext();
    }

    protected void Fold()
    {
        foreach (var card in HandContainer.GetChildren())
            HandContainer.RemoveChild(card);

        Folded = true;

        MoveNext();
    }

    private void MoveChipsToTable(int value)
    {
        // Set players current bet to the bet value
        SetChipCount(ChipCount - value);
        CurrentBet.AddChips(value);

        if (!RoundInformation.ContainsChildOfType<Bet>())
        {
            RoundInformation.AddChild(CurrentBet);
        }
    }

}
