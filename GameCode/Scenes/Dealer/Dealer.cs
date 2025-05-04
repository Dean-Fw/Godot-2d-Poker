using Godot;

public partial class Dealer : Control
{
    [Export] private PackedScene deckScene = null!;
    [Export] private HBoxContainer communityCardsContianer = null!;
    [Export] private PlayerParent playersParent = null!;
    [Export] private RoundManager roundManager = null!;

    private Deck deck = null!;

    public override void _Ready()
    {
        deck = deckScene.Instantiate<Deck>();
        AddChild(deck);

        playersParent.PlayersReady += () => HandlePlayersReady();

        roundManager.RoundEnd += HandleRoundEnd;
    }

    public void DealToCommunityCards(int cardsToDeal)
    {
        for (int cardsDealt = 0; cardsDealt < cardsToDeal; cardsDealt++)
        {
            var card = GetCard();
            card.FlipCard();

            communityCardsContianer.AddChild(
                card
            );
        }
    }

    public void DealHandToPlayer(Player player)
    {
        player.HandContainer.AddChild(
                GetCard()
        );

        player.HandContainer.AddChild(
                GetCard()
        );
    }

    private void HandlePlayersReady()
    {
        // get all players from the players node and listen to their trun end signal
        foreach (var player in playersParent.Players)
        {
            DealHandToPlayer(player);
        }
    }

    private void HandleRoundEnd(RoundPhase nextRoundPhase)
    {
        if (nextRoundPhase == RoundPhase.Flop)
        {
            DealToCommunityCards(3);
            return;
        }

        DealToCommunityCards(1);
    }

    private Card GetCard()
    {
        var card = deck.GetChild<Card>(0);
        deck.RemoveChild(card);

        return card;
    }


}
