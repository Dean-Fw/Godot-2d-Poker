using Godot;

public partial class Player : Node
{
	[Export] public HBoxContainer HandContainer;

	[Signal] public delegate void TurnEndEventHandler(Player player); 
	
	public virtual void StartTurn() {
	}

	// Signal to end this players turn	
	protected void MoveNext() {
		EmitSignal(SignalName.TurnEnd, this);
	}
}
