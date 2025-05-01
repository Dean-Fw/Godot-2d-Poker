using Godot;
using System.Collections.Generic;

public partial class RoundManager : Node
{
	[Export] private PlayerParent playersParent;

	[Signal] public delegate void TurnStartEventHandler();

	private List<Player> players = [];

	public override void _Ready() {
		players = playersParent.GetPlayers();

		StartPlayerTurn(players[0]);
	}

	private void StartPlayerTurn(Player player) {
		// Listen for when this player has finished their turn
		player.TurnEnd += HandleTurnEnd;
		
		// Tell them to begin their turn
		player.StartTurn();
	}

	private void HandleTurnEnd(Player player) {
		// Stop Listening to THIS player about them finishing their turn
		player.TurnEnd -= HandleTurnEnd;

		//Pick the next player
		
		// What position is th current player in
		var playerIndex = players.IndexOf(player);

		// if the next player is outside the list pick the first player
		if(playerIndex + 1 == players.Count) {
			StartPlayerTurn(players[0]);
			return;
		} 

		// start the turn of the next player
		StartPlayerTurn(players[playerIndex + 1]);
	}


}
