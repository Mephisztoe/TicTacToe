namespace TicTacToe.Lib.GameStates
{
    public interface IGameState
    {
        IGameState LastGameState { get; set; }

        void Present(GameBoard board);

        IGameState Execute(GameBoard board);
    }
}
