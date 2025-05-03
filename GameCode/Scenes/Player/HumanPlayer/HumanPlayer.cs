using Godot;

public partial class HumanPlayer : Player
{
    [Export] private Button nextButton;

    public override void _Ready()
    {
        nextButton.ButtonDown += () => HandlePress();
    }

    public override void StartTurn()
    {
        GD.Print("Player Start");
        nextButton.Disabled = false;
    }

    private void HandlePress()
    {
        nextButton.Disabled = true;
        MakeBet(10);
        MoveNext();
    }

    private void ToggleButtonActive()
    {
        nextButton.Disabled = !nextButton.Disabled;
    }
}
