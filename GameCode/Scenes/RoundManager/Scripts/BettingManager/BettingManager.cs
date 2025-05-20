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
        bettingPlayers.Clear();
        bettingPlayers.AddRange(players);

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


        var allPlayersCalled = bettingPlayers
            .Where(p => p.CurrentBet.Value == minimumBet).ToList().Count == bettingPlayers.Count;

        var allPlayersActed = bettingPlayers.Where(p => p.Acted).ToList().Count == bettingPlayers.Count;

        GD.Print($"Everyone has acted: {allPlayersActed}, everyone has called: {allPlayersCalled}");

        var next = bettingPlayers.GetNextNonBustPlayer(player);

        if (player.Folded)
            bettingPlayers.Remove(player);

        if (allPlayersCalled && allPlayersActed)
        {
            EmitSignal(SignalName.BettingEnded, Variant.CreateFrom(bettingPlayers.ToArray()));
            return;
        }

        if (bettingPlayers.Count == 1)
        {
            EmitSignal(SignalName.BettingEnded, Variant.CreateFrom(bettingPlayers.ToArray()));
            return;
        }

        if (next != null)
        {
            StartPlayerTurn(next);
            return;
        }

        EmitSignal(SignalName.BettingEnded, Variant.CreateFrom(bettingPlayers.ToArray()));
        return;
    }

}
