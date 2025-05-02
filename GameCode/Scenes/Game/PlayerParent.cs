using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class PlayerParent : Control
{
	[Signal] public delegate void PlayersReadyEventHandler();

	public List<Player> Players {get; private set;}

	public override void _Ready() {
		Players = GetChildren()
			.OfType<Player>()
			.ToList();

		GD.Print(Players.Count);

		GiveInitialChips(100); 	

		EmitSignal(SignalName.PlayersReady);
	}

	private void GiveInitialChips(int value) {
		foreach(var player in Players) 
			player.ChipCount = value;
	}
}
