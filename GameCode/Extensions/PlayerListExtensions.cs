using System.Collections.Generic;
using System.Linq;
using System;

public static class ListExtensions
{
    public static Player GetNext<Player>(this List<Player> input, Player current)
    {
        return input.IndexOf(current) + 1 >= input.Count ? input[0] : input[input.IndexOf(current) + 1];
    }

    public static Player GetNextUnfoldedPlayer(this List<Player> Players, Player currentPlayer)
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
}
