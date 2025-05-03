using Godot;

public partial class Dealer : Control
{
    [Export] private PackedScene deckScene;
    [Export] private HBoxContainer communityCardsContianer;
    [Export] private PlayerParent PlayersParent;

    private Deck deck = null!;

    public override void _Ready()
    {
        deck = deckScene.Instantiate<Deck>();
        AddChild(deck);

        PlayersParent.PlayersReady += () => HandlePlayersReady();
    }

    public void DealToCommunityCards()
    {
        communityCardsContianer.AddChild(
            GetCard()
        );
    }

    public void DealToPlayer(Player player)
    {
        player.HandContainer.AddChild(
            GetCard()
        );
    }

    private void HandlePlayersReady()
    {
        // get all players from the players node and listen to their trun end signal
        foreach (var player in PlayersParent.Players)
        {
            player.TurnEnd += DealToPlayer;
        }
    }

    private Card GetCard()
    {
        var card = deck.GetChild<Card>(0);
        deck.RemoveChild(card);

        card.FlipCard();

        return card;
    }


}
