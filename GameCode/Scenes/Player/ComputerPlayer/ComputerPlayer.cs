using Godot;

public partial class ComputerPlayer : Player
{

    public override void _Ready()
    {
        base._Ready();
    }

    public override void StartTurn(int minimumBet)
    {
        GD.Print($"{Name} Start");
        MakeBet(minimumBet);
    }
}
