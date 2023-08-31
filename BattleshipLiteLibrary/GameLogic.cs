using BattleshipLiteLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipLiteLibrary
{
    public static class GameLogic
    {
        public static int GetShotCount(PlayerInfoModel player)
        {
            int shotCount = 0;

            foreach (var shot in player.ShotGrid)
            {
                if (shot.Status != GridSpotStaus.Empty) 
                {
                    shotCount++;
                }
            }

            return shotCount;
        }

        public static bool IdentifyShotResult(PlayerInfoModel opponent, string row, int column)
        {
            bool isAHit = false;

            foreach (var ship in opponent.ShipLocations)
            {
                if (ship.SpotLetter == row.ToUpper() && ship.SpotNumber == column)
                {
                    isAHit = true;
                    ship.Status = GridSpotStaus.Sunk;
                }
            }

            return isAHit;
        }

        public static void InitializeGrid(PlayerInfoModel playerModel)
        {
            List<string> letters = new List<string>
            {
                "A",
                "B",
                "C",
                "D",
                "E"
            };

            List<int> numbers = new List<int> { 0, 1, 2, 3, 4, 5 };

            foreach (string letter in letters)
            {
                foreach (int number in numbers)
                {
                    AddGridSport(playerModel, letter, number);
                }
            }

        }

        public static void MarkShotResult(PlayerInfoModel player, string row, int column, bool isAHit)
        {

            foreach (var gridSpot in player.ShotGrid)
            {
                if (gridSpot.SpotLetter == row.ToUpper() && gridSpot.SpotNumber == column)
                {
                    if (isAHit)
                    {
                        gridSpot.Status = GridSpotStaus.Hit;
                    }
                    else
                    {
                        gridSpot.Status = GridSpotStaus.Miss;
                    }

                }
            }
        }

        public static bool PlaceShip(PlayerInfoModel playerModel, string location)
        {
            bool output = false;
            (string row, int column) = SplitShotIntoRowAndColumn(location);


            bool isValidationLocation = ValidateGridLocation(playerModel, row, column);
            bool isSpotOpen = ValidateShipLocation(playerModel, row, column);

            if (isValidationLocation && isSpotOpen)
            {
                playerModel.ShipLocations.Add(new GridSpotModel
                {
                    SpotLetter = row.ToUpper(),
                    SpotNumber = column,
                    Status = GridSpotStaus.Ship
                });
                output = true;
            }

            return output;

           
        }

        private static bool ValidateShipLocation(PlayerInfoModel playermodel, string row, int column)
        {
            bool isValidationLocation = true;

            foreach (var ship in playermodel.ShipLocations)
            {
                if(ship.SpotLetter == row.ToUpper() && ship.SpotNumber == column)
                {
                    isValidationLocation = false;
                }
            }

            return isValidationLocation;
        }

        private static bool ValidateGridLocation(PlayerInfoModel playermodel, string row, int column)
        {
            bool isValidationLocation = false;

            foreach (var ship in playermodel.ShotGrid)
            {
                if (ship.SpotLetter == row.ToUpper() && ship.SpotNumber == column)
                {
                    isValidationLocation = true;
                }
            }

            return isValidationLocation;

        }

        public static bool PlayerStillActive(PlayerInfoModel player)
        {
            bool isActive = false;

            foreach (var ship in player.ShipLocations)
            {
                if (ship.Status != GridSpotStaus.Sunk)
                {
                    isActive = true;
                }
                
            }

            return isActive;
        }

        public static (string row, int column) SplitShotIntoRowAndColumn(string shot)
        {
            string row = "";
            int column = 0;
            if (shot.Length != 2)
            {
                throw new ArgumentException("This was an invalid shot type", "shot");
            }
            char[] shotArray = shot.ToArray();

            row = shotArray[0].ToString();
            column = int.Parse(shotArray[1].ToString());

            return (row, column);
            
        }

        public static bool ValidateShot(PlayerInfoModel player, string row, int column)
        {
            bool isValidShot = false;

            foreach (var gridSpot in player.ShotGrid)
            {
                if (gridSpot.SpotLetter == row.ToUpper() && gridSpot.SpotNumber == column)
                {
                    if (gridSpot.Status == GridSpotStaus.Empty)
                    {
                        isValidShot = true;
                    }
                }
            }

            return isValidShot;
        }

        private static void AddGridSport(PlayerInfoModel playerModel, string letter, int number)
        { 
            GridSpotModel spot = new GridSpotModel 
            {
                SpotLetter = letter,
                SpotNumber = number,
                Status = GridSpotStaus.Empty
            };

            playerModel.ShotGrid.Add(spot);

            
        }
        
    }
}
