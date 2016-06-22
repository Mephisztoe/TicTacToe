using System;
using System.Windows.Forms;
using TicTacToe.Lib;
using TicTacToe.Lib.GameStates;

namespace TicTacToe.WinForm
{
    public partial class MainForm : Form
    {
        private UserInput userInput = null;
        private IGameState currentState = null;
        private GameBoard board = null;

        public MainForm()
        {
            InitializeComponent();

            this.userInput = new UserInput();
            this.board = new GameBoard(this.userInput);
            this.currentState = new GameStateRunning();
        }

        private void Execute(Button button, int field)
        {
            this.userInput.Input = field;
            try
            {
                this.currentState = this.currentState.Execute(this.board);

                // Previous player
                button.Text = this.board.ActivePlayer == Player.Player1 ? "O" : "X";
                
                if (this.currentState is GameStateWon)
                {
                    MessageBox.Show("Won");
                    Application.Exit();
                }
                else if (this.currentState is GameStateRemis)
                {
                    MessageBox.Show("Remis");
                    Application.Exit();
                }
                else
                    ToggleActivePlayer();
            }
            catch
            {
                MessageBox.Show("Invalid move");
            }
        }

        /// <summary>
        /// Shows the active player in the status bar.
        /// </summary>
        private void ToggleActivePlayer()
        {
            this.ActivePlayerValueToolStripStatusLabel.Text = this.ActivePlayerValueToolStripStatusLabel.Text == "1" ? "2" : "1";
        }

        /// <summary>
        /// Generic button click handler. Extracts the field
        /// to set from its Tag property.
        /// </summary>
        /// <param name="sender">Button that has been clicked.</param>
        /// <param name="e">Event arguments.</param>
        private void button_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var field = int.Parse(button.Tag.ToString());

            this.Execute(button, field);
        }
    }
}
