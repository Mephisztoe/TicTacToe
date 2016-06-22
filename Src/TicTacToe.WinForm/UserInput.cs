using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Lib;

namespace TicTacToe.WinForm
{  
    public sealed class UserInput : IUserInput
    {
        public int Input { get; set; }

        public int GetField(Player player)
        {
            return this.Input;
        }   
    }
}
