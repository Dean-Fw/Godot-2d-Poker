using Godot;

public partial class RoundManager : Node
{
	[Export] private PlayerParent playersParent;

	[Signal] public delegate void TurnStartEventHandler();


	public override void _Ready() {
		playersParent.PlayersReady += () => HandlePlayersReady();
	}

	private void HandlePlayersReady() {
		StartPlayerTurn(playersParent.Players[0]);
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
		var playerIndex = playersParent.Players.IndexOf(player);

		// if the next player is outside the list pick the first player
		if(playerIndex + 1 == playersParent.Players.Count) {
			StartPlayerTurn(playersParent.Players[0]);
			return;
		} 

		// start the turn of the next player
		StartPlayerTurn(playersParent.Players[playerIndex + 1]);
	}


}
