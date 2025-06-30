namespace FourZug.Backend.UtilityEngine.UtilityEngineProcessors
{
    // The actual processor of the component

    // Contains helper methods such as getting valid moves for a grid
    internal static class UtilityHelper
    {

        // Makes a move onto a grid, and returns the new grid
        public static string[,] MakeMove(string[,] grid, string turn, int col)
        {
            // Clones grid so value is used not reference
            grid = (string[,])grid.Clone();

            // Returns valid column moves
            List<byte> validColumns = ValidColumns(grid);

            // Checks if inputted column is a valid move
            if (!validColumns.Contains((byte)col))
            {
                throw new Exception("Invalid column move made on grid");
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
        public static List<byte> ValidColumns(string[,] grid)
        {
            List<byte> validCols = new();

            // Runs through all columns of grid
            for (byte col = 0; col < grid.GetLength(0); col++)
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
