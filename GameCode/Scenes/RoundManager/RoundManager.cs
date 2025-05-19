using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class RoundManager : Node
{
    [Export] private BlindsManager blindsManager = null!;
    [Export] private Dealer dealer = null!;
    [Export] private BettingManager bettingManager = null!;
    [Export] private TableCenter tableCenter = null!;
    [Export] private ShowDownManager showDownManager = null!;

    [Signal] public delegate void RoundOverEventHandler();

    public int Ante { get; set; } = 10;

    private RoundPhase roundPhase;
    private List<Player> playersInRound = [];

    public override void _Ready()
    {
        bettingManager.BettingEnded += HandleBettingEnded;
    }

    public void StartGameRound(List<Player> players)
    {
        playersInRound = [];
        roundPhase = RoundPhase.PreFlop;
        playersInRound.AddRange(players);

        ReadyTable();

        blindsManager.SetBlinds(playersInRound);

        players.First(p => p.Blinds.Contains(Blind.Dealer))
            .AddDealerChip();

        blindsManager.GatherBlinds(playersInRound, Ante);

        foreach (var player in playersInRound)
            dealer.Deal(player.HandContainer, 2);

        bettingManager.StartBetting(playersInRound, Ante * 2);
    }

    private void HandleBettingEnded(Player[] remainingPlayers)
    {
        tableCenter.CollectChips(playersInRound);

        if (remainingPlayers.Count() == 1)
        {
            tableCenter.GiveChipsTo(remainingPlayers[0]);

            EmitSignal(SignalName.RoundOver);

            return;
        }

        // TODO: If all players are bust (i.e all in) then we need to deal remaining cards and showdown
        if (roundPhase == RoundPhase.River)
        {
            var winner = showDownManager.DetermineWinner(remainingPlayers);
            tableCenter.GiveChipsTo(winner);

            EmitSignal(SignalName.RoundOver);

            return;
        }

        roundPhase++;

        if (roundPhase == RoundPhase.Flop)
            dealer.Deal(tableCenter.CommunityCards, 3);
        else
            dealer.Deal(tableCenter.CommunityCards, 1);

        // First unfolded player to the right of the dealer 
        var dealerPlayer = playersInRound.First(p => p.Blinds.Contains(Blind.Dealer));
        var next = playersInRound.GetNextUnfoldedPlayer(dealerPlayer);

        foreach (var player in remainingPlayers)
            player.Acted = false;

        bettingManager.ContinueBetting(next);
    }

    private void ReadyTable()
    {
        tableCenter.CommunityCards.Clear();

        foreach (var player in playersInRound)
            player.HandContainer.Clear();

        dealer.ReadyDeck();
    }

}
