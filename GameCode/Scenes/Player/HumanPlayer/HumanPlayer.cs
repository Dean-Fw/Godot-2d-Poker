using Godot;

public partial class HumanPlayer : Player
{
	[Export] private Button NextButton;

	public override void _Ready() {
		NextButton.ButtonDown += () => MoveNext();
	}
    
	public override void MoveNext() {
		EmitSignal(SignalName.TurnEnd, this); 
	}
}
