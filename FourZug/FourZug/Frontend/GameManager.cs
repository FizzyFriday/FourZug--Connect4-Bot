using FourZug.API;
using FourZugBot.Frontend;

namespace FourZug.Frontend
{
    // Handles gameplay flow (Originally GameFlow.cs)
    internal static class GameManager
    {
        private static string[,] grid = new string[7, 6];
        private static string turn = "X";
        private static string boardState = "StillInPlay";


        // - PUBLIC METHODS -

        // Program entry point after Frontend/Forms/AppInit.cs
        public static void SetupGame()
        {
            int colSize = grid.GetLength(0);
            int rowSize = grid.GetLength(1);

            // Fills the grid with empty spaces
            for (int c = 0; c < colSize; c++)
            {
                for (int r = 0; r < rowSize; r++)
                {
                    grid[c, r] = " ";
                }
            }

            GameLoop();
        }


        // - PRIVATE METHODS -

        

        // Handles gameplay loop
        private static void GameLoop()
        {
            // Runs until game ends
            while (boardState == "StillInPlay")
            {
                UIManager.DisplayBoard(grid);

                int colMove = -1;

                // User decides their move
                if (turn == "X")
                {
                    UIManager.DisplayPlayerTurn();
                    colMove = ValidateInput();
                }

                // Bot decides their move
                if (turn == "O")
                {
                    colMove = API.API.BestMove(grid, turn);
                }

                // Make move onto board
                grid = API.API.MakeMove(grid, turn, colMove);

                // Gets if the game has ended or still in play
                boardState = API.API.BoardState(grid, turn);

                // Switches turn if the game is still going
                if (boardState == "StillInPlay")
                {
                    // Switches turn from X to O or O to X
                    turn = (turn == "X") ? "O" : "X";
                }
            }

            UIManager.DisplayEndGame(boardState, turn);
        }

        // Asks for user move repeatedly until input is acceptable
        private static int ValidateInput()
        {
            bool accepted = false;
            int colMove = -1;

            // While the move isn't acceptable
            while (!accepted)
            {
                try
                {
                    // Attempt to translate to int
                    // Input the column via the form & UImanager
                    colMove = Convert.ToInt16(Console.ReadLine());

                    // Check if int matches a valid column
                    List<int> validColumns = API.API.ValidColumns(grid);
                    if (validColumns.IndexOf(colMove) != -1)
                    {
                        accepted = true;
                    }
                }
                catch { }
            }

            return colMove;
        }
    }
}
