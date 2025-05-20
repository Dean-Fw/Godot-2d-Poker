using System.Collections.Generic;

public static class ListExtensions
{
    public static Player GetNext<Player>(this List<Player> input, Player current)
    {
        return input.IndexOf(current) + 1 >= input.Count ? input[0] : input[input.IndexOf(current) + 1];
    }

    public static Player? GetNextUnfoldedPlayer(this List<Player> players, Player currentPlayer)
    {
        var searchableList = CreateSearchableList(players, currentPlayer);

        foreach (var player in searchableList)
        {
            if (!player.Folded)
                return player;
        }

        return null;
    }

    public static Player? GetNextNonBustPlayer(this List<Player> players, Player currentPlayer)
    {
        var searchableList = CreateSearchableList(players, currentPlayer);

        foreach (var player in searchableList)
        {
            if (player.ChipCount != 0)
                return player;
        }

        return null;
    }

    private static List<Player> CreateSearchableList(List<Player> players, Player currentPlayer)
    {
        // I have 5 players and the current player is in position 1. 
        // I want to search every player in the list to the right of the current for a non bust player
        // Once I have searched all of the players to the right I will want to start from index 0 and search until I am back at the current players
        //
        // 1. Take the index of current player
        // [0, 1, 2, 3, 4]
        //     ^
        var indexOfCurrentPlayer = players.IndexOf(currentPlayer);
        // [0, 1, 2, 3, 4], [2, 3, 4]
        // Take a slice of the current index to the end of list and then start to the index of player -1
        var searchableList = players.Slice(indexOfCurrentPlayer + 1, players.Count - 1 - indexOfCurrentPlayer);
        // [0, 1, 2, 3, 4], [2, 3, 4, 0]
        // take all values to the left of the player and add them to the end of the new list
        if (indexOfCurrentPlayer != 0)
            searchableList.AddRange(players.Slice(0, indexOfCurrentPlayer));

        return searchableList;
    }
}
