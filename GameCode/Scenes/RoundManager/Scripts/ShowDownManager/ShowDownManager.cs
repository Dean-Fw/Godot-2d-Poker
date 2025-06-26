using Godot;
using System;
using System.Linq;

public partial class ShowDownManager : Node
{
    public Player DetermineWinner(Player[] players)
    {
        var random = new Random();
        return players[random.Next(0, players.Count())];
    }
}
