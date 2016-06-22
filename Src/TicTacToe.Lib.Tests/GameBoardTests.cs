using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TicTacToe.Lib.Tests
{
    [TestClass]
    public class GameBoardTests
    {
        private GameBoard board = null;

        [TestInitialize]
        public void TestInitialize()
        {
            this.board = new GameBoard(null);
        }

        [TestMethod]
        [TestCategory("GameBoard")]      
        public void Fail_If_Greater_Than_Max()
        {
            Action action = () => board.Set(9);
            action.ShouldThrow<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        [TestCategory("GameBoard")]
        public void Fail_If_Less_Than_Min()
        {
            Action action = () => board.Set(-1);
            action.ShouldThrow<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        [TestCategory("GameBoard")]
        public void Set_Fails_If_Occupied()
        {
            board.Set(4);

            Action action = () => board.Set(4);
            action.ShouldThrow<ArgumentException>();
        }

        [TestMethod]
        [TestCategory("GameBoard")]
        public void Player_Switch_After_Move()
        {
            // It is assumed that Player 1 has the first move
            var activePlayerBeforeMove = board.ActivePlayer;

            // After making a move, it is assumed that the game automatically switched the active player to Player 2
            board.Set(4);

            // However represented, the active players should differ by now :)
            board.ActivePlayer.Should().NotBe(activePlayerBeforeMove);
        }

        [TestMethod]
        [TestCategory("GameBoard")]
        public void Is_Occupied()
        {
            board.Set(4);

            board.IsOccupied(4).Should().BeTrue();
        }

        [TestMethod]
        [TestCategory("GameBoard")]
        public void Check_Winner_Vertical_Player_1()
        {
            // Gameboard representation
            // 8 7 6 
            // 5 4 3
            // 2 1 0

            board.Set(8);
            board.Set(7);
            board.Set(5);
            board.Set(4);
            board.Set(2);
            board.Set(0);

            // X O #
            // X O #
            // X # O

            board.CheckWinner().Should().BeTrue();
        }

        [TestMethod]
        [TestCategory("GameBoard")]
        public void Check_Winner_Vertical_Player_2()
        {
            // Gameboard representation
            // 8 7 6 
            // 5 4 3
            // 2 1 0

            board.Set(8);
            board.Set(7);
            board.Set(5);
            board.Set(4);
            board.Set(0);
            board.Set(1);

            // X O #
            // X O #
            // # O X

            board.CheckWinner().Should().BeTrue();
        }

        [TestMethod]
        [TestCategory("GameBoard")]
        public void Check_Winner_Horizontal_Player_1()
        {
            // Gameboard representation
            // 8 7 6 
            // 5 4 3
            // 2 1 0

            board.Set(8);
            board.Set(5);
            board.Set(7);
            board.Set(4);
            board.Set(6);
            board.Set(0);

            // X X X
            // O O #
            // # # O

            board.CheckWinner().Should().BeTrue();
        }

        [TestMethod]
        [TestCategory("GameBoard")]
        public void Check_Winner_Horizontal_Player_2()
        {
            // Gameboard representation
            // 8 7 6 
            // 5 4 3
            // 2 1 0

            board.Set(8);
            board.Set(5);
            board.Set(7);
            board.Set(4);
            board.Set(0);
            board.Set(3);

            // X X #
            // O O O
            // # # X

            board.CheckWinner().Should().BeTrue();
        }

        [TestMethod]
        [TestCategory("GameBoard")]
        public void Check_Winner_Diagonal_Player_1()
        {
            // Gameboard representation
            // 8 7 6 
            // 5 4 3
            // 2 1 0

            board.Set(8);
            board.Set(5);
            board.Set(4);
            board.Set(2);
            board.Set(0);
            board.Set(1);

            // X # #
            // O X #
            // O O X

            board.CheckWinner().Should().BeTrue();
        }

        [TestMethod]
        [TestCategory("GameBoard")]
        public void Check_Winner_Diagonal_Player_2()
        {
            // Gameboard representation
            // 8 7 6 
            // 5 4 3
            // 2 1 0

            board.Set(5);
            board.Set(8);
            board.Set(1);
            board.Set(4);
            board.Set(2);
            board.Set(0);

            // O # #
            // X O #
            // X X O

            board.CheckWinner().Should().BeTrue();
        }

        [TestMethod]
        [TestCategory("GameBoard")]
        public void Display_Board()
        {
            // Gameboard representation
            // 8 7 6 
            // 5 4 3
            // 2 1 0

            board.Set(8);
            board.Set(5);
            board.Set(4);
            board.Set(2);
            board.Set(0);
            board.Set(1);

            // X # #
            // O X #
            // O O X

            board.ToString().ShouldBeEquivalentTo("X##\r\nOX#\r\nOOX\r\n");
        }
    }
}
