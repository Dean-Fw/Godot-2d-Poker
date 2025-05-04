using Godot;
using System.Linq;

public partial class HumanPlayer : Player
{
    [Export] private Button nextButton = null!;

    private bool cardsFlipped = false;

    public override void _Ready()
    {
        base._Ready();
        nextButton.ButtonDown += () => HandlePress();
    }

    public override void StartTurn()
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
        nextButton.Disabled = false;
    }

    private void HandlePress()
    {
        nextButton.Disabled = true;
        MakeBet(10);
    }
}
