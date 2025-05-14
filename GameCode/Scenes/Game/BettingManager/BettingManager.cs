using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class BettingManager : Node
{
    private int minimumBet;
    private List<Player> playersAtTable = [];
    public void StartBetting(List<Player> players, int ante)
    {
        minimumBet = ante;
        playersAtTable = players;

        StartPlayerTurn(
            players.First(p => p.Blinds.Contains(Blind.UnderTheGun))
        );
    }

    private void StartPlayerTurn(Player player)
    {
        player.TurnEnd += HandleTurnEnd;

        player.StartTurn(minimumBet);
    }

    private void HandleTurnEnd(Player player)
    {
        player.TurnEnd -= HandleTurnEnd;

        if (

            StartPlayerTurn(
                playersAtTable.GetNext(player)
            );
    }

}
