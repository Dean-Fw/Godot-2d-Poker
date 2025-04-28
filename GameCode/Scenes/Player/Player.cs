using Godot;
using System;

public partial class Player : Node
{
	[Export] public HBoxContainer HandContainer;

	[Signal] public delegate void TurnEndEventHandler(Player player); 

	// Abstract Parent method for testing moving on the game
	protected void MoveNext() {
		EmitSignal(SignalName.TurnEnd, this);
	}
}
