namespace TicTacToe.Lib.GameStates
{
    public class GameStateExit : GameStateBase, IGameState
    {
        public override IGameState Execute(GameBoard board)
        {
            return null;
        }
    }
}
