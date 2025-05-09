namespace FourZug.Backend
{
    // Contains helper methods such as getting valid moves for a grid
    // Originally GameUtility.cs
    internal static class UtilityEngine
    {
        // - PUBLIC METHODS -
        // Makes a move onto a grid, and returns the new grid
        public static string[,] MakeMove(string[,] grid, string turn, int col)
        {
            // Clones grid so value is used not reference
            grid = (string[,])grid.Clone();

            // Returns all columns where at least top
            // slot is empty
            List<int> validColumns = ValidColumns(grid);

            // Checks if inputted column is a valid move
            if (validColumns.IndexOf(col) == -1)
            {
                Console.WriteLine("Column invalid");
                return null;
            }

            // Starts 1 spot above highest index of array
            int currentRow = grid.GetLength(1);

            // Keep moving down until spot underneath isn't empty
            // Result is the row the piece should fall into (via gravity)
            while (currentRow > 0)
            {
                if (grid[col, currentRow - 1] == " ")
                {
                    currentRow--;
                }
                else break;
            }

            // Make move and return new grid
            grid[col, currentRow] = turn;
            return grid;
        }

        // Returns all valid columns in the game
        public static List<int> ValidColumns(string[,] grid)
        {
            List<int> validCols = new();

            // Runs through all columns of grid
            for (int col = 0; col < grid.GetLength(0); col++)
            {
                // If the toprow is empty, col is valid
                int topRow = grid.GetLength(1) - 1;
                if (grid[col, topRow] == " ")
                {
                    validCols.Add(col);
                }
            }

            return validCols;
        }
    }
}
