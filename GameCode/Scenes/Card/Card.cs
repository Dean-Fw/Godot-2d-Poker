using Godot;
using System.IO;

public partial class Card : TextureRect
{
	private const string PATH_TO_CARD_ASSESTS = "res://Assets/PlayingCards/";
	private const string PATH_TO_CARD_BACK = "card_back.png";

	public Suit Suit {get; set;}
	public int Value {get; set;}

	public override void _Ready() {
		// Sets card Texture to card back
		Texture = GD.Load<Texture2D>(Path.Join(PATH_TO_CARD_ASSESTS, PATH_TO_CARD_BACK));
	}

	public void FlipCard(){
			switch(Value) {
				case 1:
					Texture = GD.Load<Texture2D>(
						GetTexturePath(
							"ace", 
							Suit.ToString().ToLower()
						)
					);
					break;
				case 11:
					Texture = GD.Load<Texture2D>(
						GetTexturePath(
							"jack", 
							Suit.ToString().ToLower()
						)
					);
					break;
				case 12: 
					Texture = GD.Load<Texture2D>(
						GetTexturePath(
							"queen", 
							Suit.ToString().ToLower()
						)
					);
					break;
				case 13: 
					Texture = GD.Load<Texture2D>(
						GetTexturePath(
							"king", 
							Suit.ToString().ToLower()
						)
					);

					break;
				default:
					Texture = GD.Load<Texture2D>(
						GetTexturePath(
							Value.ToString(), 
							Suit.ToString().ToLower()
						)
					);

					break;
			}
	}

	private static string GetTexturePath(string value, string suit){
		return Path.Join(
			PATH_TO_CARD_ASSESTS, $"{value}_of_{suit}.png"
		);
	}

}
