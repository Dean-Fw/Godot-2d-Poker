using Godot;
using System;

public partial class Deck : Control
{
    [Export] private PackedScene cardScene = null!;

    public override void _Ready()
    {
        for (int cardIndex = 0; cardIndex < 52; cardIndex++)
        {
            var cardInstance = cardScene.Instantiate<Card>();

            cardInstance.Suit = (Suit)Math.Floor((float)cardIndex / 13);
            cardInstance.Value = cardIndex % 13 + 1;

            AddChild(cardInstance);
        }
    }
}
