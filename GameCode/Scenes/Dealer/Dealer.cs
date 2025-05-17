using Godot;
using System;

public partial class Dealer : Control
{
    [Export] private PackedScene deckScene = null!;

    private Deck deck = null!;

    public override void _Ready()
    {
        deck = deckScene.Instantiate<Deck>();
        AddChild(deck);
    }

    public void Deal(Node target, int numberOfCards)
    {
        for (var cardsDealt = 0; cardsDealt < numberOfCards; cardsDealt++)
        {
            var card = GetCard();

            if (target is HumanPlayer || target is CommunityCards)
                card.FlipCard();

            target.AddChild(card);
        }
    }

    private Card GetCard()
    {
        var random = new Random();

        var card = deck.GetChild<Card>(
            random.Next(0, deck.GetChildren().Count)
        );

        deck.RemoveChild(card);

        return card;
    }


}
