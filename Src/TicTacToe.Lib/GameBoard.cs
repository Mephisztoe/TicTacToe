using System;
using System.Text;

namespace TicTacToe.Lib
{
    public enum Player
    {
        Player1,
        Player2
    };

    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class GameBoard
    {
        // Winning combinations:
        // ------------------------------------------------------
        // 1 1 1  0 0 0  0 0 0  1 0 0  0 1 0  0 0 1  1 0 0  0 0 1
        // 0 0 0  1 1 1  0 0 0  1 0 0  0 1 0  0 0 1  0 1 0  0 1 0 
        // 0 0 0  0 0 0  1 1 1  1 0 0  0 1 0  0 0 1  0 0 1  1 0 0
        // ------------------------------------------------------
        //   448     56      7    292    146     73    273     84

        private int[] winningCombinations = { 448, 56, 7, 292, 146, 73, 273, 84 };

        // The board itself is represented binary and thus can be stored in a single integer.
        // However, since there are two players and actually three states (#, X, O), the board
        // needs to be split in two boards internally. One for Player 1 and another for Player 2.
        //
        // A board like this:      XO#
        //                         OX#
        //                         ##X
        //
        // is then represented as: 010100000100010001
        //
        // Which means:            Player 2  Player 1
        //                         010100000 100010001
        //
        // Or:                     010       100  
        //                         100       010
        //                         000       001

        private int board = 0;

        private IUserInput userInput = null;
       
        public Player ActivePlayer { get; private set; } = Player.Player1;

        // The methods necessary for capturing user input is injected as 
        // an interface so that it can be replaced by a stub for better
        // unit testing the GameState.
        public GameBoard(IUserInput userInput)
        {
            this.userInput = userInput;         
        }

        /// <summary>
        /// Retrieves the input of a player, i.e. the field to set.
        /// </summary>
        /// <returns>Choosen field.</returns>
        public int GetField()
        {
            return this.userInput.GetField(this.ActivePlayer);
        }

        /// <summary>
        /// Creates a textual representation of the board.
        /// </summary>
        /// <returns>String representation of the board.</returns>
        public override string ToString()
        {
            string result = string.Empty;

            // Convert the board (represented as a number) to a bit string.
            // The padding makes sure that it is 18 characters in size and gets leading zeros.
            var bits = Convert.ToString(board, 2).PadLeft(18, '0');
            var nextCharacter = string.Empty;
            var sb = new StringBuilder();

            for (int i = 0; i < 9; i++)
            {
                nextCharacter = "#"; // An empty field
                if (bits[i].Equals('1')) nextCharacter = "O"; 
                if (bits[i+9].Equals('1')) nextCharacter = "X";

                sb.Append(nextCharacter);

                // Linebreak
                if ((i+1) % 3 == 0) sb.AppendLine();
            }

            return sb.ToString();
        }

        public void Set(int pos)
        {
            if (pos < 0 || pos > 8) throw new ArgumentOutOfRangeException("The gameboard only consists of nine fields. Please set a new coin only on positions from zero to eight.");
            if (this.IsOccupied(pos)) throw new ArgumentException("This field is already occupied");

            // Gameboard representation
            // 8 7 6 
            // 5 4 3
            // 2 1 0

            // Binary representation
            // 2^8 2^7 2^6   2^5 2^4 2^3   2^2 2^1 2^0
            // 0   0   0     0   0   0     0   0   0

            // Setting to pos 4 e.g. is like left-shifting a bit by 4 like this
            // Using GetPosition for the active player makes sure the bit gets
            // shifted to the respective players' area of the board.
            this.board |= this.GetPosition(pos, this.ActivePlayer);

            // After making a move, switch to the other player
            this.SwitchPlayer();
        }

        /// <summary>
        /// Switches the active player.
        /// </summary>
        private void SwitchPlayer()
        {
            this.ActivePlayer = this.ActivePlayer == Player.Player1 ? Player.Player2 : Player.Player1;
        }
        
        /// <summary>
        /// Checks if there is a winner in town.
        /// </summary>
        /// <returns>True if there is a winner.</returns>
        /// <remarks>
        /// Currently, there is no need for this method to return who 
        /// actually has won the game, because this information is presented
        /// right after a move has been made and thus it is clear who won.
        /// </remarks>
        public bool CheckWinner()
        {
            foreach (int winningCombination in this.winningCombinations)
            {
                if (this.IsMatch(winningCombination))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if the combination is found on the board.
        /// </summary>
        /// <param name="combination">Combination to look for.</param>
        /// <returns>True if match is found.</returns>
        private bool IsMatch(int combination)
        {
            return (this.board & combination << 9) == combination << 9 || // Check if player 2 won
                (this.board & combination ) == combination; // Check if player 1 won
        }

        /// <summary>
        /// Check if field is occupied by using shift and logical operators.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool IsOccupied(int pos)
        {
            // Position 2 set (amongst others):
            // 0 0 1  1 0 0  1 0 0

            // Check if position 2 is set:
            // 1 << 2 => 1 0 0
            // Board:         0 0 1  1 0 0  1 0 0
            // Check:         0 0 0  0 0 0  1 0 0 
            // ----------------------------------
            // Logical "And": 0 0 0  0 0 0  1 0 0 (results in 1 << 2)

            var player1Pos = this.GetPosition(pos, Player.Player1);
            var player2Pos = this.GetPosition(pos, Player.Player2);

            return (this.board & player1Pos) == player1Pos ||  // Check if occupied by Player 1
                (this.board & player2Pos) == player2Pos; // Check if occupied by Player 2
        }

        /// <summary>
        /// Retrieves the bit position in the internal representation of the game board.
        /// </summary>
        /// <param name="pos">Requested position</param>
        /// <returns>The requested position of the respective players' area of the board.</returns>
        private int GetPosition(int pos, Player player)
        {
            // Shift to the respective players' area of the board and return the requested position
            return (1 << (int)player * 9) << pos;
        }
    }
}