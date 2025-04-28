using Godot;

public partial class HumanPlayer : Player
{
	[Export] private Button NextButton;

	public override void _Ready() {
		NextButton.ButtonDown += () => HandlePress();
	}
    
	public void HandlePress() {
		MoveNext();
	}
}
