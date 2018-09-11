using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System;
namespace TicTacToeCSharpConsole
{
    public class ComputerActions
    {
        private List<List<string>> winningConfigurations;

        public ComputerActions()
        {
            winningConfigurations = new List<List<string>> {
                new List<string> {"1", "2", "3"},
                new List<string> {"4", "5", "6"},
                new List<string> {"7", "8", "9"},
                new List<string> {"1", "4", "7"},
                new List<string> {"2", "5", "8"},
                new List<string> {"3", "6", "9"},
                new List<string> {"1", "5", "9"},
                new List<string> {"3", "5", "7"}
            };
        }

        public string getBestMove(List<string> board, String playerSymbol)
        {
            if (board == null)
            {
                return "";
            }
            var unoccupiedTiles = getUnoccupiedTiles(board);
            if (unoccupiedTiles.Count == 0) {
                return "";
            }
            var initialTileToWinMapping = getInitialTileToWinMapping(unoccupiedTiles);
            var newMapping = new List<(string, int, bool)>();
            foreach ((string, int, bool) mapping in initialTileToWinMapping) {
                var updatedBoard = getUpdatedBoard(board, mapping.Item1, playerSymbol);
                newMapping.Add((mapping.Item1, mapping.Item2 + 1, winExists(updatedBoard, playerSymbol)));
            }
            return findTheBestMoveThatResultsInAWin(newMapping);
        }

        private string findTheBestMoveThatResultsInAWin(List<(string, int, bool)> mapping)
        {
            var winningTiles = mapping.FindAll(x => x.Item3 == true);
            if (winningTiles.Any())
            {
                var smallestWinCount = winningTiles.Min(x => x.Item2);
                //get tile 5 if it is open
                //otherwise get any tile with the lowest win count

                var mappingsWithTile5AsAWin = winningTiles.FindAll(x => x.Item1 == "5" && x.Item2 == smallestWinCount);
                if (mappingsWithTile5AsAWin.Any())
                {
                    return mappingsWithTile5AsAWin.First().Item1;
                }
            }
            var smallestCount = mapping.Min(x => x.Item2);
            var mappingsWithTile5 = mapping.FindAll(x => x.Item1 == "5" && x.Item2 == smallestCount);
            if (mappingsWithTile5.Any())
            {
                return mappingsWithTile5.First().Item1;
            }

            return mapping.OrderBy(x => x.Item2 == smallestCount).First().Item1;
        }

        private List<string> getUnoccupiedTiles(List<string> board) 
        {
            return board.FindAll(x => isUnoccupiedTile(x) == true);
        }


        private bool isUnoccupiedTile(string tile) 
        {
            Regex rx = new Regex(@"[1-9]{1}",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matches = rx.Matches(tile);
            return matches.Count == 1;
        }

        private IEnumerable<(string, int, bool)> getInitialTileToWinMapping(List<string> unoccupiedTiles) 
        {
            var initialTileToWinMapping = unoccupiedTiles.Select(x => (x, 0, false));
            return initialTileToWinMapping;
        }

        private List<string> getUpdatedBoard(List<string> board, string tile, string symbol) 
        {
            var newBoard = board.Select(x => (x == tile) ? symbol : x).ToList();
            return newBoard;
        }


        public bool winExists(List<string> board, string playerSymbol) 
        {
            return (board[0] == playerSymbol && board[1] == playerSymbol && board[2] == playerSymbol) ||
                   (board[3] == playerSymbol && board[4] == playerSymbol && board[5] == playerSymbol) ||
                   (board[6] == playerSymbol && board[7] == playerSymbol && board[8] == playerSymbol) ||
                   (board[0] == playerSymbol && board[3] == playerSymbol && board[6] == playerSymbol) ||
                   (board[1] == playerSymbol && board[4] == playerSymbol && board[7] == playerSymbol) ||
                   (board[2] == playerSymbol && board[5] == playerSymbol && board[8] == playerSymbol) ||
                   (board[0] == playerSymbol && board[4] == playerSymbol && board[8] == playerSymbol) ||
                   (board[2] == playerSymbol && board[4] == playerSymbol && board[6] == playerSymbol);
        }
    }
}









//private string getHighestScoringTile(List<List<(string, int, bool)>> mapping)
//{
//    var max = mapping.Select(x => (key(x), x)).Max().Item2;
//}