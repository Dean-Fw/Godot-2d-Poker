using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class BettingManager : Node
{
    private int minimumBet;
    private List<Player> bettingPlayers = [];

    [Signal] public delegate void BettingEndedEventHandler();

    public void StartBetting(List<Player> players, int ante)
    {
        minimumBet = ante;
        bettingPlayers = players;

        StartPlayerTurn(
            bettingPlayers.First(p => p.Blinds.Contains(Blind.UnderTheGun))
        );
    }

    public void ContinueBetting(Player startingPlayer)
    {
        minimumBet = 0;

        StartPlayerTurn(startingPlayer);
    }

    private void StartPlayerTurn(Player player)
    {
        player.TurnEnd += HandleTurnEnd;

        player.StartTurn(minimumBet);
    }

    private void HandleTurnEnd(Player player)
    {
        player.TurnEnd -= HandleTurnEnd;

        if (player.CurrentBet.Value > minimumBet)
            minimumBet = player.CurrentBet.Value;

        if (player.Folded)
            bettingPlayers.Remove(player);

        var nextPlayer = bettingPlayers.GetNext(player);

        // In an opening round of calls, if the big blind has not had thier turn they may raise or check)
        if (nextPlayer.CurrentBet.Value == minimumBet && nextPlayer.Blinds.Contains(Blind.BigBlind))
        {
            // Remove the player's BigBlind status as it could  retrigger this check and will not be needed again this round
            nextPlayer.Blinds.Remove(Blind.BigBlind);
            StartPlayerTurn(nextPlayer);
            return;
        }

        if (nextPlayer.CurrentBet.Value < minimumBet || (!player.Blinds.Contains(Blind.Dealer) && minimumBet == 0))
        {
            StartPlayerTurn(nextPlayer);
            return;
        }

        EmitSignal(SignalName.BettingEnded);
    }

}
