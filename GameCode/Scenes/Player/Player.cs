using Godot;
using System;

public partial class Player : Node
{
	[Export] public HBoxContainer HandContainer;

	[Signal] public delegate void TurnEndEventHandler(Player player); 

	// Abstract Parent method for testing moving on the game
	public virtual void MoveNext() {
		throw new Exception("Called From the base");
	}
}
