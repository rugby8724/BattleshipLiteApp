using BattleShipLite;

using BattleshipLiteLibrary;
using BattleshipLiteLibrary.Models;

WelcomeMessage();

PlayerInfoModel activePlayer = PlayerInformation.CreatePlayer("Player 1");
PlayerInfoModel opponent = PlayerInformation.CreatePlayer("Player 2");

PlayerInfoModel winner = null;

do
{
    
    DisplayShotGrid(activePlayer);

    
    RecordPlayerShot(activePlayer, opponent);
    
    bool doesGameContinue = GameLogic.PlayerStillActive(opponent);

    // If over, set activePlaer as winner
    // else, go to opponent
    if (doesGameContinue)
    {
        // Swap using a temp variable
        // PlayerInfoModel tempHolder = opponent;
        // opponent = activePlayer;
        // activePlayer = tempHolder;

        // Use Tuple to swap positions

        (activePlayer, opponent) = (opponent, activePlayer);

    }
    else
    {
        winner = activePlayer;
    }

} while (winner == null);

IdentifyWinner(winner);

Console.ReadLine();

void IdentifyWinner(PlayerInfoModel winner)
{
    Console.WriteLine($"Congratulations to {winner.UsersName} for winning");
    Console.WriteLine($" {winner.UsersName} took { GameLogic.GetShotCount(winner)} shots");
}

void RecordPlayerShot(PlayerInfoModel activePlayer, PlayerInfoModel opponent)
{
    bool isValidShot = false;
    string row = "";
    int column = 0;

    do
    {
        string shot = AskForShot();
        (row, column) = GameLogic.SplitShotIntoRowAndColumn(shot);

        isValidShot = GameLogic.ValidateShot(activePlayer, row, column);

        if (isValidShot == false)
        {
            Console.WriteLine("Invalid shot location, please try again");
        }
    } while (isValidShot == false);
    
    bool isAHit = GameLogic.IdentifyShotResult(opponent, row, column);
    
    GameLogic.MarkShotResult(activePlayer, row, column, isAHit);
}

string AskForShot()
{
    Console.WriteLine("Please enter your shot selection");
    string output = Console.ReadLine();

    return output;
}

void DisplayShotGrid(PlayerInfoModel activePlayer)
{
    string currentRow = activePlayer.ShortGrid[0].SpotLetter;

    foreach (var gridSpot in activePlayer.ShortGrid)
    {
        if(gridSpot.SpotLetter != currentRow)
        {
            Console.WriteLine();
            currentRow = gridSpot.SpotLetter;
        }
        if (gridSpot.Status == GridSpotStaus.Empty)
        {
            Console.Write($" {gridSpot.SpotLetter}{gridSpot.SpotNumber} ");
        }
        else if (gridSpot.Status == GridSpotStaus.Hit)
        {
            Console.Write(" X ");
        }
        else if (gridSpot.Status == GridSpotStaus.Miss)
        {
            Console.Write(" O ");
        }
        else
        {
            Console.Write(" ? ");
        }

    }
}

static void WelcomeMessage()
{
    Console.WriteLine("Welcome to Battleship Lite");
    Console.WriteLine("created by Tad");
    Console.WriteLine();
}



