using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class RoundManager : Node
{
    [Export] private BlindsManager blindsManager = null!;
    [Export] private Dealer dealer = null!;
    [Export] private BettingManager bettingManager = null!;
    [Export] private TableCenter tableCenter = null!;

    public int Ante { get; set; } = 10;

    private RoundPhase roundPhase;
    private List<Player> playersInRound = [];

    public override void _Ready()
    {
        bettingManager.BettingEnded += HandleBettingEnded;
    }

    public void StartGameRound(List<Player> players)
    {
        roundPhase = RoundPhase.PreFlop;
        playersInRound = players;

        blindsManager.SetBlinds(playersInRound);

        players.First(p => p.Blinds.Contains(Blind.Dealer))
            .AddDealerChip();

        blindsManager.GatherBlinds(playersInRound, Ante);

        foreach (var player in playersInRound)
            dealer.Deal(player.HandContainer, 2);

        bettingManager.StartBetting(playersInRound, Ante * 2);
    }

    private void HandleBettingEnded(int remainingPlayers)
    {
        tableCenter.CollectChips(playersInRound);

        if (remainingPlayers == 1)
        {
            GD.Print("Round Over: 1 Player Left");
            return;
        }

        if (roundPhase == RoundPhase.River)
        {
            GD.Print("Round Over: Got To the River");
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

        foreach (var player in playersInRound)
            player.Acted = false;

        bettingManager.ContinueBetting(next);
    }


    //private void EndRound()
    //{
    //// end this cycle of the game if the river round has finished (for now) 
    //if (currentRoundPhase == RoundPhase.River)
    //{
    //EmitSignal(SignalName.RoundEnd, Variant.From<RoundPhase>(RoundPhase.ShowDown));
    //
    //GD.Print("Game Ended");
    //return;
    //}
    //
    //// Move the round on
    //currentRoundPhase = currentRoundPhase + 1;
    //
    //// Signal the round has ended and the round we are going to
    //EmitSignal(SignalName.RoundEnd, Variant.From<RoundPhase>(currentRoundPhase));
    //
    //// Reset betting
    //highestBet = 0;
    //
    //StartRound();
    //
}
