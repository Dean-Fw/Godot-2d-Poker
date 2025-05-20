using Godot;

public partial class GameManager : Node
{
    [Export] private PlayerParent playerParent = null!;
    [Export] private RoundManager roundManager = null!;

    public override void _Ready()
    {
        playerParent.PlayersReady += () => HandlePlayersReady();
        roundManager.RoundOver += () => HandleRoundOver();
    }

    private void StartGame()
    {
        roundManager.StartGameRound(playerParent.Players);
    }

    private void HandleRoundOver()
    {
        playerParent.RemoveBustPlayers();

        if (playerParent.Players.Count > 1)
        {
            GD.Print("NEW ROUND");
            StartGame();
            return;
        }

        GD.Print($"Thats all folks, {playerParent.Players[0]} wins!");
    }

    // Once the Player Parent has initialised the players start the game
    private void HandlePlayersReady()
    {
        StartGame();
    }

}
