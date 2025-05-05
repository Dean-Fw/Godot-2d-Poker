using Godot;
using System.Linq;

public partial class HumanPlayer : Player
{
    [Export] private ActionsContainer actionsContainer = null!;

    private bool cardsFlipped = false;

    public override void _Ready()
    {
        base._Ready();

        actionsContainer.Bet += HandleBet;
    }

    public override void StartTurn(int minimumBet)
    {
        if (!cardsFlipped)
        {
            foreach (var card in HandContainer.GetChildren().OfType<Card>())
            {
                card.FlipCard();
            }
            cardsFlipped = true;
        }

        GD.Print("Player Start");
        actionsContainer.ReadyActions(minimumBet, ChipCount);
    }

    private void HandleBet(int betAmount)
    {
        MakeBet(betAmount);
    }
}
