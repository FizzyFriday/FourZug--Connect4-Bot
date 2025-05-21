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
        private string[,] grid;
        private Panel[,] boardPanels;

        // Gets called as part of the initialization process by AppInit.cs
        public UIForm()
        {
            InitializeComponent();
            InitializeBoard();
        }


        private void InitializeBoard()
        {
            const int colCount = 7, rowCount = 6;

            // Set size of app window
            this.Height = 500;
            this.Width = 700;
            boardPanels = new Panel[colCount, rowCount];


            // Create an empty board
            grid = new string[colCount, rowCount];
            for (int c = 0; c < colCount; c++)
            {
                for (int r = 0; r < rowCount; r++)
                {
                    grid[c, r] = " ";

                    // Add panel onto UI board
                    Panel panel = new Panel();
                    boardPanels[c, r] = panel;

                    // Sets up the panel visuals
                    panel.Size = new Size(50, 50);
                    panel.Location = new Point(60 * c, 60 * r);
                    panel.BackColor = Color.Black;

                    // Adds panel onto UI (gameBoard is a named groupbox on UI)
                    this.gameBoard.Controls.Add(panel);
                }
            }
        }

        // Displays game onto screen
        private void DisplayBoard(string[,] grid)
        {
            // Display game grid into table
        }

        // Ran when user selects to take a turn
        private void UserTakeTurn(int col)
        {
            List<int> validCols = API.API.ValidColumns(grid);
            if (validCols.IndexOf(col) == -1)
            {
                // Notify of invalid column
                return;
            }

            // User makes move
            bool gameEnded = MakeBoardMove(col, "X");
            if (gameEnded) return;

            // Bot makes move
            int botCol = API.API.BestMove(grid, "O");
            MakeBoardMove(botCol, "O");
        }

        // Makes a move on the board, and returns if the game ended
        private bool MakeBoardMove(int col, string turn)
        {
            // Make move and display
            this.grid = API.API.MakeMove(grid, turn, col);
            DisplayBoard(grid);

            // Handle the board state after making move
            string boardState = API.API.BoardState(grid, turn);
            if (boardState != "StillInPlay")
            {
                // End the game
                EndGame(boardState, turn);
                return true;
            }
            return false;
        }

        private void EndGame(string boardState, string turnBy)
        {
            // Display game has ended
            // Prevent any more input
        }
    }
}
