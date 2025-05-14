using Godot;
using System.Collections.Generic;
using System;
using System.Linq;

public partial class PlayerParent : Control
{
    [Signal] public delegate void PlayersReadyEventHandler();

    public List<Player> Players { get; private set; } = [];

    public override void _Ready()
    {
        Players = GetChildren()
            .OfType<Player>()
            .ToList();

        GiveInitialChips(100);

        EmitSignal(SignalName.PlayersReady);
    }

    public Player GetNextUnfoldedPlayer(Player currentPlayer)
    {
        // There should never be a case where every player has folded, this will also cause the below loop to be infinite
        // In the case there is somehow throw this Exception
        if (Players.Where(p => !p.Folded).Count() == 0)
            throw new Exception("There cannot be a case where every player has folded");

        var indexOfCurrentPlayer = Players.IndexOf(currentPlayer);

        // Get the next player in the list
        var nextPlayer = Players.GetNext(currentPlayer);

        // If the next player has not folded this should never start
        while (nextPlayer.Folded)
        {
            // If the next player has folded, get the next player until we find one that has not folded
            nextPlayer = Players.GetNext(nextPlayer);
        }

        return nextPlayer;
    }

    private void GiveInitialChips(int value)
    {
        foreach (var player in Players)
            player.SetChipCount(value);
    }
}
