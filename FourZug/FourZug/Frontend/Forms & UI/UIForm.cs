using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FourZug.Frontend.Forms
{
    // Handles UI changes
    public partial class UIForm : Form
    {
        public UIForm()
        {
            InitializeComponent();
        }

        // Sets up empty spots on screen
        public void InitializeBoard()
        {
            this.Height = 500;
            this.Width = 500;
            // Add empty spots to board
        }

        // Displays game onto screen
        public void DisplayBoard(string[,] grid)
        {
            // Display game grid into table
        }

        public void DisplayPlayerTurn()
        {
            // Display that it is players turn
            // Highlight valid columns?
        }

        public void DisplayEndGame(string boardState, string lastPlayToMove)
        {
            /*
            UIManager.DisplayGame();
            Console.WriteLine("");

            if (boardState == "Win")
            {
                Console.WriteLine($"Player {turn} wins!");
            }
            else if (boardState == "Draw")
            {
                Console.WriteLine("The game ended with a draw");
            }
            */
        }
    }
}
