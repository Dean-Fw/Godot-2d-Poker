using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class RoundManager : Node
{
    [Export] private BlindsManager blindsManager = null!;
    [Export] private Dealer dealer = null!;
    [Export] private BettingManager bettingManager = null!;

    public int Ante { get; set; } = 10;

    private RoundPhase roundPhase;
    private List<Player> playersInRound = [];

    public override void _Ready()
    {
        bettingManager.BettingEnded += () => HandleBettingEnded();
    }

    public void StartGameRound(List<Player> players)
    {
        roundPhase = RoundPhase.PreFlop;
        playersInRound = players;

        blindsManager.SetBlinds(playersInRound);

        players.First(p => p.Blinds.Contains(Blind.Dealer))
            .AddDealerChip();

        blindsManager.GatherBlinds(playersInRound, Ante);

        dealer.DealToPlayers(playersInRound);

        bettingManager.StartBetting(playersInRound, Ante * 2);
    }

    private void HandleBettingEnded()
    {
        if (roundPhase == RoundPhase.River)
        {
            GD.Print("Rond Over");
            return;
        }

        roundPhase++;

        if (roundPhase == RoundPhase.Flop)
            dealer.DealToCommunityCards(3);
        else
            dealer.DealToCommunityCards(1);

        // First unfolded player to the right of the dealer 
        var dealerPlayer = playersInRound.First(p => p.Blinds.Contains(Blind.Dealer));
        var next = playersInRound.GetNextUnfoldedPlayer(dealerPlayer);

        bettingManager.ContinueBetting(next);
    }


    //private void HandleTurnEnd(Player player)
    //{
    //// Stop Listening to THIS player about them finishing their turn
    //player.TurnEnd -= HandleTurnEnd;
    //
    //if (playersParent.Players.Count == 1)
    //{
    //GD.Print("Game ended as only one player left");
    //return;
    //}
    //
    //if (player.CurrentBet.Value > highestBet)
    //highestBet = player.CurrentBet.Value;
    //
    ////Pick the next player
    //var nextPlayer = playersParent.GetNextUnfoldedPlayer(player);
    //
    //// If the next player has not matched the highest bet then they have to act
    //// TODO This needs to be changed to include cases where the table has checked (Bet will be 0)
    //if (nextPlayer.CurrentBet.Value != highestBet)
    //{
    //StartPlayerTurn(nextPlayer);
    //return;
    //}
    //
    //// end the round
    //EndRound();
    //}
    //
    //private void EndRound()
    //{
    //// end this cycle of the game if the river round has finished (for now) 
    //if (currentRoundPhase == RoundPhase.River)
    //{
    //EmitSignal(SignalName.RoundEnd, Variant.From<RoundPhase>(RoundPhase.ShowDown));
    //
    //GD.Print("Game Ended");
    //return;
    //}
    //
    //// Move the round on
    //currentRoundPhase = currentRoundPhase + 1;
    //
    //// Signal the round has ended and the round we are going to
    //EmitSignal(SignalName.RoundEnd, Variant.From<RoundPhase>(currentRoundPhase));
    //
    //// Reset betting
    //highestBet = 0;
    //
    //StartRound();
    //
}

