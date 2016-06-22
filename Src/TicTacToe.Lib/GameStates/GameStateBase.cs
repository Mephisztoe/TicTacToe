namespace TicTacToe.Lib.GameStates
{
    public abstract class GameStateBase : IGameState
    {
        public IGameState LastGameState { get; set; }

        public abstract IGameState Execute(GameBoard board);

        public virtual void Present(GameBoard board)
        {
            // Display the board 
            System.Console.Clear();
            System.Console.WriteLine(board.ToString());
        }
    }
}
