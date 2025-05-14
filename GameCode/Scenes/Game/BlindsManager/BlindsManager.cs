using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class BlindsManager : Node
{
    public void SetBlinds(List<Player> players)
    {
        var dealer = players.FirstOrDefault(p => p.Blinds.Contains(Blind.Dealer));

        if (dealer == null)
        {
            dealer = players[players.Count - 1];
        }

        dealer = players.GetNext(dealer);
        dealer.Blinds.Add(Blind.Dealer);

        var smallBlind = players.GetNext(dealer);
        smallBlind.Blinds.Add(Blind.SmallBlind);

        var bigBlind = players.GetNext(smallBlind);
        bigBlind.Blinds.Add(Blind.BigBlind);

        var underTheGun = players.GetNext(bigBlind);
        underTheGun.Blinds.Add(Blind.UnderTheGun);
    }


    public void GatherBlinds(List<Player> players, int Ante)
    {
        players.First(p => p.Blinds.Contains(Blind.SmallBlind))
            .GiveBlinds(Ante);

        players.First(p => p.Blinds.Contains(Blind.BigBlind))
            .GiveBlinds(Ante * 2);
    }
}
