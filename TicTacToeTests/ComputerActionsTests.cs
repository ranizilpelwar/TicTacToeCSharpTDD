using System.Collections.Generic;
using TicTacToeCSharpConsole;
using Xunit;

namespace TicTacToeTests
{
    public class ComputerActionTests
    {
        [Fact]
        public void whenANullBoardIsProvidedThenEmptyStringIsReturned()
        {
            var computerAction = new ComputerActions();
            Assert.Equal("", computerAction.getBestMove(null, ""));
        }

        [Fact]
        public void whenThereAreNoUnoccupiedTilesThenEmptyStringIsReturned()
        {
            var board = new List<string>{ "X", "X", "O",
                                          "O", "O", "X",
                                          "X", "X", "O"};
            var computerAction = new ComputerActions();
            Assert.Equal("", computerAction.getBestMove(board, ""));
        }

        [Fact]
        public void whenThereIsOneUnoccupiedTileThenThatTileIsReturned()
        {
            var board = new List<string>{ "X", "X", "Y",
                                          "X", "X", "Y",
                                          "Y", "Y", "9"};
            var computerAction = new ComputerActions();
            Assert.Equal("9", computerAction.getBestMove(board, ""));
        }

        //[Fact] //Dup
        //public void whenThereAreTwoUnoccupiedTilesThenTheWinningTileIsReturned_scenario1()
        //{
        //    var board = new List<string>{ "X", "Y", "X",
        //                                  "X", "Y", "Y",
        //                                  "7", "X", "9"};
        //    var computerAction = new ComputerActions();
        //    Assert.Equal("7", computerAction.getBestMove(board, "X"));
        //}

        [Theory]
        [MemberData(nameof(ComputerActionsDataSource.TestData), MemberType = typeof(ComputerActionsDataSource))]
        public void whenThereAreMultipleUnoccupiedTilesThenTheWinningTileIsReturned(string tile1, string tile2, string tile3,
                                                                               string tile4, string tile5, string tile6,
                                                                               string tile7, string tile8, string tile9, 
                                                                               string expectedMove, string playerSymbol, string assertionMessage)
        {
            var board = new List<string> { tile1, tile2, tile3, tile4, tile5, tile6, tile7, tile8, tile9 };
            var computerAction = new ComputerActions();
            Assert.True(expectedMove == computerAction.getBestMove(board, playerSymbol), assertionMessage);
        }
    }

    public static class ComputerActionsDataSource
    {
        public static List<object[]> TestData { get; } = new List<object[]>
        {
            new object[] {"X", "Y", "X",
                          "X", "Y", "Y",
                          "7", "X", "9", "7", "X", "tile 7 results in the only win"},

            new object[] {"1", "Y", "X",
                          "X", "X", "Y",
                          "X", "Y", "9", "1", "X", "tile 1 results in the fastest win"},

            new object[] {"X", "Y", "X",
                          "4", "Y", "6",
                          "X", "X", "Y", "4", "X", "tile 4 results in the only win"},

            new object[] {"X", "Y", "X",
                          "4", "X", "6",
                          "X", "8", "Y", "4", "X", "tile 4 results in the fastest win compared to tile 6"},

            new object[] {"X", "Y", "X",
                          "4", "X", "6",
                          "X", "8", "9", "4", "X", "tile 4 results in the fastest win when there are more unoccupied tiles"},

            new object[] {"X", "Y", "3",
                          "X", "5", "Y",
                          "Y", "Y", "X", "5", "X", "tile 5 results in the only win"},

            new object[] {"1", "Y", "3",
                          "X", "5", "Y",
                          "Y", "Y", "X", "5", "X", "Choice between two tiles with similar ability to win should select tile 5"}
        };
    }
}
