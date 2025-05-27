using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FourZug.API;

namespace FourZug.Frontend.Forms
{
    // Handles UI changes
    public partial class UIForm : Form
    {
        private string[,] grid;
        private bool playersTurn;
        private bool gameEnded;
        private Panel[,] boardPanels;

        // A reference to the API. Also loads the backend references
        private FourZug.API.API api = new();



        // Gets called as part of the initialization process by AppInit.cs
        public UIForm()
        {
            InitializeComponent();
            InitializeBoard();
        }


        private void InitializeBoard()
        {
            txtGameResult.Enabled = false;
            txtTurnNum.Text = $"Turn {turnNum}";
            const int colCount = 7, rowCount = 6;
            playersTurn = true;
            gameEnded = false;

            // Impacts the sizing and display of the board
            const int panelSize = 50;
            const int panelGapPx = 3;
            int positionScale = panelSize + panelGapPx;

            // Size is determined by the number of columns/rows and their sizes + number of gaps
            int gameBoardWidthSize = (colCount * panelSize) + ((colCount + 1) * panelGapPx);
            int gameBoardHeightSize = (rowCount * panelSize) + ((rowCount + 1) * panelGapPx);

            // Set size of game board
            this.gbxGameBoard.Height = gameBoardHeightSize;
            this.gbxGameBoard.Width = gameBoardWidthSize;

            // Set size of app window
            this.Height = gameBoardHeightSize + 150;
            this.Width = gameBoardWidthSize + 40;
            boardPanels = new Panel[colCount, rowCount];


            // Create an empty board
            grid = new string[colCount, rowCount];
            for (int c = 0; c < colCount; c++)
            {
                for (int r = 0; r < rowCount; r++)
                {
                    grid[c, r] = " ";

                    // Create panel
                    Panel panel = new Panel();
                    boardPanels[c, r] = panel;

                    // Sets up the panel visuals
                    panel.Size = new Size(panelSize, panelSize);
                    panel.BackColor = Color.Black;

                    // Adds tag of panel's column, and add click event
                    panel.Tag = c;
                    panel.Click += (s, e) => UserTakeTurn(s, e);

                    // Flips the grid upside down, into correct display
                    int rowInUI = (rowCount - 1) - r;
                    // Using the current column and row, size of panels, and gaps between each panel
                    // The panels are placed in a perfect grid
                    panel.Location = new Point(panelGapPx + (positionScale * c), panelGapPx + (positionScale * rowInUI));

                    // Adds panel onto UI (gameBoard is a named groupbox on UI)
                    this.gbxGameBoard.Controls.Add(panel);
                }
            }
        }

        // Displays game onto screen
        private void DisplayBoard(string[,] grid, bool gameEnded = false)
        {
            // Display grid onto UI
            for (int col = 0; col < grid.GetLength(0); col++)
            {
                for (int row = 0; row < grid.GetLength(1); row++)
                {
                    // Using the value at each index, change colour of each panel
                    // Using the boardPanels field, and matching index
                    string positionPiece = grid[col, row];

                    // Set colour based on piece
                    if (positionPiece == "X")
                    {
                        Panel parallelPanel = boardPanels[col, row];
                        parallelPanel.BackColor = Color.Blue;
                    }
                    if (positionPiece == "O")
                    {
                        Panel parallelPanel = boardPanels[col, row];
                        parallelPanel.BackColor = Color.Red;
                    }
                }
            }

            // Forces the UI to finish updating before allowing further processing
            Application.DoEvents();
        }

        // Ran when user selects to take a turn
        private void UserTakeTurn(object sender, EventArgs e)
        {
            // Prevent a move if not player's turn or game ended
            if (!playersTurn || gameEnded) return;

            // Converted clicked object to Panel and check if null
            Panel? clickedPanel = sender as Panel;
            if (clickedPanel == null) return;

            // Get column of selected panel
            int? tag = clickedPanel.Tag as int?;
            if (tag == null) return;
            int col = (int)tag;

            // Check if column is valid
            List<int> validCols = API.API.ValidColumns(grid);
            if (validCols.IndexOf(col) == -1) return;

            // User makes move and switches turn
            turnNum++;
            MakeBoardMove(col, "X");
            if (this.gameEnded) return;
            this.playersTurn = false;

            // Bot makes move
            int botCol = API.API.BestMove(grid, "O");
            turnNum++;
            MakeBoardMove(botCol, "O");
            this.playersTurn = true;
            
        }

        // Makes a move on the board, and returns if the game ended
        private void MakeBoardMove(int col, string turn)
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
            }
        }

        // Displays the game ended and prevent input
        private void EndGame(string boardState, string turnBy)
        {
            // Displays the results of game
            if (boardState == "Draw")
            {
                txtGameResult.Text = "Draw";
                this.BackColor = Color.Orange;
            }
            if (boardState == "Win" && turnBy == "X")
            {
                txtGameResult.Text = "You win!";
                this.BackColor = Color.Blue;

            }
            else if (boardState == "Win" && turnBy == "O")
            {
                txtGameResult.Text = "Bot wins!";
                this.BackColor = Color.Red;
            }

            this.gameEnded = true;
        }
    }
}
