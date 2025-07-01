using Accessibility;

namespace FourZug.Backend.ta
{
    // The implemented interface of the component

    public class UtilityEngine : IUtilityEngine
    {
        // INTERFACE CONTRACTS

        // Converts a grid row and columns into an unique "id"
        public int RowColumnToID(int row, int col)
        {
            const byte idGainFromCol = 6;
            int id = idGainFromCol * col + row;

            return id;
        }

        // Gets the piece at the row and column related to unique "id"
        public string PieceAtPositionID(string[,] grid, int ID)
        {
            const byte idGainFromCol = 6;
            int col = ID / idGainFromCol, row = ID % idGainFromCol;
            return grid[col, row];
        }

        // Makes a move onto a grid, and returns the new grid
        public string[,] MakeMove(string[,] grid, string turn, int col)
        {
            // Clones grid so value is used not reference
            grid = (string[,])grid.Clone();

            // Returns valid column moves
            List<byte> validColumns = GetValidMoves(grid);

            // Checks if inputted column is a valid move
            if (validColumns.IndexOf((byte)col) == -1)
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
        public List<byte> GetValidMoves(string[,] grid)
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
