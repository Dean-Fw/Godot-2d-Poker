using Godot;

public partial class Dealer : Control
{
	[Export] private PackedScene deckScene;
	[Export] private HBoxContainer communityCardsContianer;

	private Deck deck = null!;

    	public override void _Ready()
    	{
       		deck = deckScene.Instantiate<Deck>();
		AddChild(deck);

		for(int i = 0; i < 5; i ++)
			DealToCommunityCards();

    	}

	public void DealToCommunityCards() {
		var card = deck.GetChild<Card>(0);
		deck.RemoveChild(card);

		card.FlipCard();

		communityCardsContianer.AddChild(card);
	}


}
