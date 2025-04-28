using Godot;

public partial class Dealer : Control
{
	[Export] private PackedScene deckScene;
	[Export] private HBoxContainer communityCardsContianer;
	[Export] private Player player;

	private Deck deck = null!;

	public override void _Ready() {
		deck = deckScene.Instantiate<Deck>();
		AddChild(deck);

		// Subscribe to the player trunend signal
		player.TurnEnd += DealToPlayer;


		for(int i = 0; i < 5; i ++)
			DealToCommunityCards();

	}

	public void DealToCommunityCards() {
		communityCardsContianer.AddChild(
			GetCard()
		);
	}

	public void DealToPlayer(Player player) {
		player.HandContainer.AddChild(
			GetCard()
		);

	}

	private Card GetCard() {
		var card = deck.GetChild<Card>(0);
		deck.RemoveChild(card);

		card.FlipCard();

		return card;
	}


}
