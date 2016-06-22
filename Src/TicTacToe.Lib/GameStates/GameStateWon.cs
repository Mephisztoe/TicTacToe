﻿namespace TicTacToe.Lib.GameStates
{
    public class GameStateWon : GameStateBase, IGameState
    {
        public override IGameState Execute(GameBoard board)
        {
            return new GameStateExit() { LastGameState = this };
        }

        public override void Present(GameBoard board)
        {
            base.Present(board);

            System.Console.WriteLine("Won!");
        }
    }
}
