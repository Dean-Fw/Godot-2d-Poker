using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class RoundManager : Node
{
    [Export] private BlindsManager blindsManager = null!;
    [Export] private Dealer dealer = null!;
    [Export] private BettingManager bettingManager = null!;

    public int Ante { get; set; } = 10;

    public void StartGameRound(List<Player> players)
    {
        blindsManager.SetBlinds(players);

        players.First(p => p.Blinds.Contains(Blind.Dealer))
            .AddDealerChip();

        blindsManager.GatherBlinds(players, Ante);

        dealer.DealToPlayers(players);

        bettingManager.StartBetting(players, Ante * 2);
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

