using FourZug.API;

namespace FourZug.Frontend
{
    // Handles game display
    // Handles gameplay flow
    internal static class GameFlow
    {
        private static string[,] grid = new string[7, 6];
        private static string turn = "X";
        private static string boardState = "StillInPlay";


        // - PUBLIC METHODS -

        // Program entry point
        public static void Main()
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

        // Displays grid onto screen
        private static void DisplayGame()
        {
            // Running down the grid
            for (int r = 5; r >= 0; r--)
            {
                // Display top header
                Console.WriteLine(new string('-', 43));
                string col = "";

                // Display row contents
                for (int c = 0; c < 7; c++)
                {
                    col += $"|  {grid[c, r]}  ";
                }
                col += "|";
                Console.WriteLine(col);
            }
            // Display bottom of grid
            Console.WriteLine(new string('-', 43));
        }

        // Handles gameplay loop
        private static void GameLoop()
        {
            // Runs until game ends
            while (boardState == "StillInPlay")
            {
                Console.Clear();
                DisplayGame();
                
                int colMove=-1;
                // Bot decides their move
                if (turn == "O")
                {
                    Console.WriteLine("Bot is deciding their move...");
                    colMove = API.API.BestMove(grid, turn);
                    //Console.WriteLine($"Bot says best move is: {colMove}");
                }

                // User decides their move
                if (turn == "X")
                {
                    Console.WriteLine($"Player {turn}. Enter move (0-6)");
                    colMove = ValidateInput();
                    Console.WriteLine("");
                }

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
            GameEnd();
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

        // Handles the end of game display message
        private static void GameEnd()
        {
            Console.Clear();
            DisplayGame();
            Console.WriteLine("");

            if (boardState == "Win")
            {
                Console.WriteLine($"Player {turn} wins!");
            }
            else if (boardState == "Draw")
            {
                Console.WriteLine("The game ended with a draw");
            }
        }
    }
}
