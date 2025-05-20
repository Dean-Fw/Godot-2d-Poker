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
        base.StartTurn(minimumBet);

        GD.Print($"{Name} Start");

        var random = new Random();
        var choice = random.Next(5);

        switch (choice)
        {
            case 0:
            case 1:
            case 2:
                GD.Print($"{Name} Calls {amountToCall}");
                MakeBet(amountToCall);
                break;
            case 3:
                // Bet is a random int between 1 more than the minimumBet and the max chipcount
                var bet = random.Next(amountToCall + 1, ChipCount + 1);
                GD.Print($"{Name} Raises {bet}");

                MakeBet(bet);
                break;
            case 4:
                GD.Print($"{Name} Folds");
                Fold();
                break;

        }


    }
}
