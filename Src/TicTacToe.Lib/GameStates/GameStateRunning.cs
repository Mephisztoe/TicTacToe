namespace TicTacToe.Lib.GameStates
{
    public class GameStateRunning : GameStateBase, IGameState
    {
        // Count moves, because after 9 moves without a 
        // winning combination, the game results in a remis.
        public static int moves = 0;
        
        public override IGameState Execute(GameBoard board)
        {                       
            // Active Players' field choice
            int field = board.GetField();

            try
            {
                // Inverse input for internal representation
                board.Set(9 - field);
                moves++;

                if (board.CheckWinner())
                {
                    return new GameStateWon() { LastGameState = this };
                }
                else if (moves == 9)
                {
                    // Only count as remis if not won with the last move
                    return new GameStateRemis() { LastGameState = this };
                }
            }
            catch
            {
                throw;                     
            }

            return this;
        }
    }
}
