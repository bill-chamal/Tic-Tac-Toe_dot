using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class WinMessegeForm : Form
    {
        public WinMessegeForm(string a)
        {
            InitializeComponent();
            if (a != "DRAW")
                label1.Text = a + "\nWon the GAME!";
            else
                label1.Text = a + "\nNo one won...";
        }

    }
}
