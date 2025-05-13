using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class BlindsManager : Node
{
    public void SetBlinds(List<Player> players)
    {
        var dealer = players.FirstOrDefault(p => p.Blind == Blinds.Dealer);

        if (dealer == null)
        {
            dealer = players[players.Count - 1];
        }

        dealer = players.GetNext(players.IndexOf(dealer));
        dealer.Blind = Blinds.Dealer;

        var smallBlind = players.GetNext(players.IndexOf(dealer));
        smallBlind.Blind = Blinds.SmallBlind;

        var bigBlind = players.GetNext(players.IndexOf(smallBlind));
        bigBlind.Blind = Blinds.BigBlind;
    }
}
