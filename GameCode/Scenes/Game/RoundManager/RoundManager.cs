using Godot;

public partial class RoundManager : Node
{
    [Export] private PlayerParent playersParent = null!;
    [Export] private BlindsManager blindsManager = null!;

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
        blindsManager.SetBlinds(playersParent.Players.Count);
        highestBet = blindsManager.Ante;

        playersParent.Players[blindsManager.Blinds.SmallBlind].GiveBlinds(blindsManager.Ante);

        playersParent.Players[blindsManager.Blinds.BigBlind].GiveBlinds(blindsManager.Ante * 2);

        highestBet = blindsManager.Ante * 2;

        playersParent.Players[blindsManager.Blinds.Dealer].AddDealerChip();
        StartRound();
    }

    private void StartRound()
    {
        StartPlayerTurn(playersParent.Players[blindsManager.Blinds.UnderTheGun]);
    }

    private void StartPlayerTurn(Player player)
    {
        // Listen for when this player has finished their turn
        player.TurnEnd += HandleTurnEnd;

        // Tell them to begin their turn
        player.StartTurn(highestBet);
    }

    private void HandleTurnEnd(Player player)
    {
        // Stop Listening to THIS player about them finishing their turn
        player.TurnEnd -= HandleTurnEnd;

        if (playersParent.Players.Count == 1)
        {
            GD.Print("Game ended as only one player left");
            return;
        }

        if (player.CurrentBet.Value > highestBet)
            highestBet = player.CurrentBet.Value;

        //Pick the next player
        var nextPlayer = playersParent.GetNextUnfoldedPlayer(player);

        // If the next player has not matched the highest bet then they have to act
        // TODO This needs to be changed to include cases where the table has checked (Bet will be 0)
        // Possibly need to refactor turn management in to it's own node, breaching the S in SOLID here
        if (nextPlayer.CurrentBet.Value != highestBet)
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

        // Reset betting
        highestBet = 0;

        StartRound();

    }

}
