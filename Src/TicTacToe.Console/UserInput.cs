using TicTacToe.Lib;

namespace TicTacToe.Console
{
    public class UserInput : IUserInput
    {
        public int GetField(Player player)
        {
            string input = string.Empty;
            int field = 0;

            do
            {
                System.Console.WriteLine("Player {0}, please enter a number between 1 and 9.", (int)player + 1);
                input = System.Console.ReadLine();
            } while (!int.TryParse(input, out field) || field < 1 || field > 9);

            return field;
        }
    }
}