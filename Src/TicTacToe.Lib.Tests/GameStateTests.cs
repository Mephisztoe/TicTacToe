using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToe.Lib.Fakes;
using TicTacToe.Lib.GameStates;
using FluentAssertions;

namespace TicTacToe.Lib.Tests
{
    [TestClass]
    public class GameStateTests
    {
        private int[] movesPlayerOneWins = { 1, 2, 4, 6, 7 };
        private int[] movesRemis = { 1, 2, 4, 7, 8, 5, 9, 6, 3 };

        private static int move = 0;
     
        [TestMethod]
        [TestCategory("GameState")]
        public void GameState_Running()
        {           
            var userInput = new StubIUserInput()
            {
                GetFieldPlayer = player => { return 1; }
            };
   
            var state = new GameStateRunning().Execute(new GameBoard(userInput));
                       
            state.Should().BeOfType<GameStateRunning>();           
        }

        [TestMethod]
        [TestCategory("GameState")]
        public void GameState_Won()
        {         
            var userInput = new StubIUserInput()
            {
                GetFieldPlayer = player => { return movesPlayerOneWins[move++]; }
            };

            var state = this.SimulateGame(new GameBoard(userInput));
                       
            state.Should().BeOfType<GameStateWon>();
        }

        [TestMethod]
        [TestCategory("GameState")]
        public void GameState_Remis()
        {
            var userInput = new StubIUserInput()
            {
                GetFieldPlayer = player => { return movesRemis[move++]; }
            };
           
            var state = this.SimulateGame(new GameBoard(userInput));
                      
            state.Should().BeOfType<GameStateRemis>();
        }

        /// <summary>
        /// Simulates the gameloop.
        /// </summary>
        /// <param name="board">Board on which to play.</param>
        /// <returns>Returns last game state.</returns>
        private IGameState SimulateGame(GameBoard board)
        {
            IGameState currentState = new GameStateRunning();
            IGameState lastState = null;

            while (currentState != null)
            {
                currentState = currentState.Execute(board);

                if (currentState != null)
                    lastState = currentState.LastGameState;
            }

            return lastState;
        }
    }
}
