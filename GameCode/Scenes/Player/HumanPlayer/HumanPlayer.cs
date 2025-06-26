using Godot;
using System.Linq;

public partial class HumanPlayer : Player
{
    [Export] private ActionsContainer actionsContainer = null!;

    public override void _Ready()
    {
        base._Ready();

        actionsContainer.Bet += HandleBet;
        actionsContainer.Fold += () => HandleFold();
    }

    public override void StartTurn(int minimumBet)
    {
        base.StartTurn(minimumBet);

        foreach (var card in HandContainer.GetChildren().OfType<Card>())
        {
            card.FlipCard();
        }

        GD.Print("Player Start");
        actionsContainer.ReadyActions(amountToCall, ChipCount);
    }

    private void HandleBet(int betAmount)
    {
        MakeBet(betAmount);
    }

    private void HandleFold()
    {
        Fold();
    }
}
