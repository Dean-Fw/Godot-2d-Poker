using Godot;
using System;

public partial class ActionsContainer : Node
{
    [Export] private Button callButton = null!;
    [Export] private Button raiseButton = null!;
    [Export] private HSlider raiseAmountSlider = null!;
    [Export] private Button foldButton = null!;

    [Signal] public delegate void BetEventHandler(int bet);
    [Signal] public delegate void FoldEventHandler();

    private int callAmount;
    private int allInAmount;
    private int raiseAmount;

    public override void _Ready()
    {
        callButton.Pressed += () => CallButtonHandler();
        raiseAmountSlider.ValueChanged += RaiseAmountSliderHandler;
        raiseButton.Pressed += () => RaiseButtonHandler();
        foldButton.Pressed += () => FoldButtonHandler();
    }

    public void ReadyActions(int minimumBet, int maximumBet)
    {
        callAmount = minimumBet;
        allInAmount = maximumBet;

        callButton.Disabled = false;
        foldButton.Disabled = false;

        if (minimumBet >= maximumBet)
        {
            callButton.Text = "All In";
            return;
        }

        if (minimumBet == 0)
            callButton.Text = "Check";
        else
            callButton.Text = $"Call ({callAmount})";

        raiseButton.Text = $"Raise ({callAmount})";

        raiseAmountSlider.Editable = true;
        raiseAmountSlider.MinValue = callAmount;
        raiseAmountSlider.MaxValue = allInAmount;
        raiseAmountSlider.Value = 0;
    }

    private void DisableActions()
    {
        callButton.Disabled = true;
        raiseButton.Disabled = true;
        foldButton.Disabled = true;

        raiseAmountSlider.Editable = false;
    }

    private void CallButtonHandler()
    {
        DisableActions();
        EmitSignal(SignalName.Bet, callAmount);
    }

    private void RaiseAmountSliderHandler(double value)
    {
        raiseAmount = (int)Math.Floor(allInAmount * (value / allInAmount));
        raiseButton.Text = raiseAmount == allInAmount ? "All In" : $"Raise ({raiseAmount})";

        if (raiseAmount == callAmount)
        {
            raiseButton.Disabled = true;
            return;
        }

        raiseButton.Disabled = false;
    }

    private void RaiseButtonHandler()
    {
        DisableActions();
        EmitSignal(SignalName.Bet, raiseAmount);

        raiseAmount = 0;
        raiseAmountSlider.Value = 0;
    }

    private void FoldButtonHandler()
    {
        DisableActions();
        EmitSignal(SignalName.Fold);
    }
}
