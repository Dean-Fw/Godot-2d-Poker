using Godot;
using System;

public partial class ComputerPlayer : Player
{

    public override void _Ready()
    {
        base._Ready();
    }

    public override void StartTurn(int minimumBet)
    {
        GD.Print($"{Name} Start");

        var random = new Random();
        var choice = random.Next(3);

        switch (choice)
        {
            case 0:
                GD.Print($"{Name} Calls");
                MakeBet(minimumBet);
                break;
            case 1:
                GD.Print($"{Name} Raises");
                MakeBet(ChipCount * (random.Next(minimumBet + 1, ChipCount + 1) / ChipCount));
                break;
            case 2:
                GD.Print($"{Name} Folds");
                Fold();
                break;

        }


    }
}
