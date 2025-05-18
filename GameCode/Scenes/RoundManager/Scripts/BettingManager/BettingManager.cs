using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class BettingManager : Node
{
    private int minimumBet;
    private List<Player> bettingPlayers = [];

    [Signal] public delegate void BettingEndedEventHandler(Player[] playersRemaining);

    public void StartBetting(List<Player> players, int ante)
    {
        minimumBet = ante;
        bettingPlayers.AddRange(players);
        GD.Print($"Count at bettingStart: {bettingPlayers.Count}");
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

        if (bettingPlayers.Count == 1)
        {
            EmitSignal(SignalName.BettingEnded, Variant.CreateFrom(bettingPlayers.ToArray()));
            return;
        }

        var allPlayersCalled = bettingPlayers
            .Where(p => p.CurrentBet.Value == minimumBet).ToList().Count == bettingPlayers.Count;

        var allPlayersActed = bettingPlayers.Where(p => p.Acted).ToList().Count == bettingPlayers.Count;


        GD.Print($"Everyone has acted: {allPlayersActed}, everyone has called: {allPlayersCalled}");

        if (allPlayersCalled && allPlayersActed)
        {
            EmitSignal(SignalName.BettingEnded, Variant.CreateFrom(bettingPlayers.ToArray()));
            return;
        }

        else
        {
            StartPlayerTurn(bettingPlayers.GetNext(player));
            return;
        }
    }

}
