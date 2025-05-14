using Godot;
using System.Collections.Generic;
using System;

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

    public void DealToPlayers(List<Player> players)
    {
        foreach (var player in players)
        {
            player.HandContainer.AddChild(GetCard());

            player.HandContainer.AddChild(GetCard());
        }

    }

    private void HandleRoundEnd(RoundPhase nextRoundPhase)
    {
        if (nextRoundPhase == RoundPhase.ShowDown)
            return;

        if (nextRoundPhase == RoundPhase.Flop)
        {
            DealToCommunityCards(3);
            return;
        }

        DealToCommunityCards(1);
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
