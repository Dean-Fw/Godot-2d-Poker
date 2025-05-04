using Godot;

public partial class RoundManager : Node
{
    [Export] private PlayerParent playersParent = null!;
    [Export] private BlindsManager blindsManager = null!;

    [Signal] public delegate void TurnStartEventHandler();
    [Signal] public delegate void RoundStartEventHandler(int playerCount);
    [Signal] public delegate void RoundEndEventHandler(RoundPhase nextPhase);

    private int highestBet;
    private RoundPhase currentRoundPhase;

    public override void _Ready()
    {
        playersParent.PlayersReady += () => HandlePlayersReady();
        currentRoundPhase = RoundPhase.PreFlop;
    }

    private void HandlePlayersReady()
    {
        StartRound();
    }

    private void StartRound()
    {
        EmitSignal(SignalName.RoundStart, playersParent.Players.Count);

        StartPlayerTurn(playersParent.Players[blindsManager.Blinds.UnderTheGun]);
    }

    private void StartPlayerTurn(Player player)
    {
        // Listen for when this player has finished their turn
        player.TurnEnd += HandleTurnEnd;

        // Tell them to begin their turn
        player.StartTurn();
    }

    private void HandleTurnEnd(Player player)
    {
        // Stop Listening to THIS player about them finishing their turn
        player.TurnEnd -= HandleTurnEnd;

        if (player.CurrentBet.Value > highestBet)
            highestBet = player.CurrentBet.Value;

        //Pick the next player

        // What position is the current player in
        var playerIndex = playersParent.Players.IndexOf(player);

        // Get the next player
        var nextPlayer = playerIndex + 1 >= playersParent.Players.Count ? playersParent.Players[0] : playersParent.Players[playerIndex + 1];

        // If the next player has not matched the highest bet then they have to act

        if (nextPlayer.CurrentBet == null || nextPlayer.CurrentBet.Value != highestBet)
        {
            StartPlayerTurn(nextPlayer);
            return;
        }

        // end the round
        EndRound();
    }

    private void EndRound()
    {
        // end this cycle of the game if the river round has finished (for now) 
        if (currentRoundPhase == RoundPhase.River)
        {
            EmitSignal(SignalName.RoundEnd, Variant.From<RoundPhase>(RoundPhase.ShowDown));

            GD.Print("Game Ended");
            return;
        }

        // Move the round on
        currentRoundPhase = currentRoundPhase + 1;

        // Signal the round has ended and the round we are going to
        EmitSignal(SignalName.RoundEnd, Variant.From<RoundPhase>(currentRoundPhase));

        StartRound();

    }

}
