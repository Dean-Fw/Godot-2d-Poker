using Godot;
using System.Linq;

public partial class PotManager : Node {
	[Export] private HBoxContainer tableCenter;
	[Export] private PackedScene potScene;
	[Export] private PlayerParent playerParent;

	public Pot Pot {get; private set;} = null!;

	public override void _Ready() {
		playerParent.PlayersReady += () => HandlePlayersReady();
	}

	private void HandlePlayersReady() {
		foreach(var player in playerParent.Players)
			player.Bet += HandleBet;
	}

	private void HandleBet(int value) {
		if (!tableCenter.GetChildren().ToList().Exists(x => x is Pot)) 
			Pot = potScene.Instantiate<Pot>();
			tableCenter.AddChild(Pot);

		Pot.AddChips(value);
	}

}
