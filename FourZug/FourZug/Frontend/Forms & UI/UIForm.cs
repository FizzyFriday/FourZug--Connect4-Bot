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

        // Gets called as part of the initialization process by AppInit.cs
        public UIForm()
        {
            InitializeComponent();
            InitializeBoard();
        }


        private void InitializeBoard()
        {
            // Create an empty board
            const int colCount = 7, rowCount = 6;
            grid = new string[colCount, rowCount];
            for (int c = 0; c < colCount; c++)
            {
                for (int r = 0; r < rowCount; r++)
                {
                    grid[c, r] = " ";

                }
            }

            // Handle board creation on UI
            this.Height = 300;
            this.Width = 500;
            // Add empty spots to board
        }

        // Displays game onto screen
        private void DisplayBoard(string[,] grid)
        {
            // Display game grid into table
        }

        // Ran when user selects to take a turn
        private void UserTakeTurn(int col)
        {
            // If input invalid, notify user. Return.

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
