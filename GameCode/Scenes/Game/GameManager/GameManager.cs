using Godot;

public partial class GameManager : Node
{
    [Export] private PlayerParent playerParent = null!;
    [Export] private RoundManager roundManager = null!;

    public override void _Ready()
    {
        playerParent.PlayersReady += () => HandlePlayersReady();
    }

    private void StartGame()
    {
        roundManager.StartGameRound(playerParent.Players);
    }

    // Once the Player Parent has initialised the players start the game
    private void HandlePlayersReady()
    {
        StartGame();
    }

}
