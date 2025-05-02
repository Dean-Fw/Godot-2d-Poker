using Godot;

public partial class ComputerPlayer : Player {

	public override void _Ready() {
		base._Ready();	
	}

    

	public override void StartTurn() {
		GD.Print("PC START");
		MakeBet(10);
		MoveNext();
	}
}
