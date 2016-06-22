using System;
using TicTacToe.Lib;
using TicTacToe.Lib.GameStates;

namespace TicTacToe.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new GameBoard(new UserInput());

            IGameState currentState = new GameStateRunning();

            // GameLoop
            while (currentState != null)
            {
                currentState.Present(board);

                try
                {
                    // Finite State Machine 
                    // implemented by using a strategy pattern
                    currentState = currentState.Execute(board);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                    System.Console.WriteLine("Press any key to continue.");
                    System.Console.ReadLine();
                }
            }
        }
    }
}
