using Godot;
using System.Collections.Generic;
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

    public void RemoveBustPlayers()
    {
        var bustPlayers = new List<Player>(Players.Where(p => p.ChipCount <= 0));

        foreach (var player in bustPlayers)
        {
            RemoveChild(player);
            Players.Remove(player);
        }
    }

    private void GiveInitialChips(int value)
    {
        foreach (var player in Players)
            player.SetChipCount(value);
    }

}
