using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class PlayerParent : Control
{
	public override void _Ready() {
		GiveInitialChips(100); // Don't think this is a SOLID way to do this, Round Manager is now responsible for initalising players...
	}

	public List<Player> GetPlayers() {
		return GetChildren()
			.OfType<Player>()
			.ToList();
	}

	private void GiveInitialChips(int value) {
		foreach(var player in GetPlayers()) 
			player.ChipCount = value;
	}
}
