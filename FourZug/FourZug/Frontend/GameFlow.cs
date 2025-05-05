using FourZug.API;

namespace FourZug.Frontend
{
    // Handles game display
    // Handles gameplay flow
    internal static class GameFlow
    {
        private static string[,] grid;
        private static string turn = "";
        private static bool gameEnded = false;


        // - PUBLIC METHODS -

        // Program entry point
        public static void Main()
        {
            int colSize = 7;
            int rowSize = 6;

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
            while (!gameEnded)
            {
                DisplayGame();
                Console.WriteLine($"Player {turn}. Enter move (0-6)");
                int colMove = ValidateInput();
                Console.WriteLine("");

                API.API.MakeMove(grid, turn, colMove);
                // Check if game ended
            }
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
    }
}
