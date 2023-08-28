using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleshipLiteLibrary;
using BattleshipLiteLibrary.Models;

namespace BattleShipLite
{
    internal class PlayerInformation
    {
        public static PlayerInfoModel CreatePlayer(string playerTitle)
        {
            PlayerInfoModel output = new PlayerInfoModel();

            Console.WriteLine($"Player information for {playerTitle}");

            // Ask the user for their name
            output.UsersName = AskForUsersName();
            // Load up the shot grid
            GameLogic.InitializeGrid(output);
            // ask user for their 5 ship placements
            PlaceShips(output);
            // Clear
            Console.Clear();

            return output;
        }

        public static string AskForUsersName()
        {
            Console.WriteLine("What is your name? ");
            string output = Console.ReadLine();

            return output;

        }

        public static void PlaceShips(PlayerInfoModel playermodel)
        {
            do
            {
                Console.WriteLine($"Where do you want to place your ship number" +
                    $"{playermodel.ShipLocations.Count + 1} ");
                string location = Console.ReadLine();

                bool isValidLocation = GameLogic.PlaceShip(playermodel, location);

                if (isValidLocation == false)
                {
                    Console.WriteLine("That was not a valid location. Please Try again");
                }


            } while (playermodel.ShipLocations.Count < 5);
        }
    }
}
