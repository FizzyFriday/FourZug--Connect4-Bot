using Accessibility;
using System.Reflection.Metadata.Ecma335;

namespace FourZug.Backend.UtilityEngineAccess
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
        public char PieceAtPositionID(char[,] grid, int ID)
        {
            const byte idGainFromCol = 6;
            int col = ID / idGainFromCol, row = ID % idGainFromCol;
            return grid[col, row];
        }

        // Makes a move onto a grid, and returns the new grid
        public char[,] MakeMove(char[,] grid, char turn, int col, int availableRow)
        {
            // Clones grid so value is used not reference
            grid = (char[,])grid.Clone();

            // Checks if inputted column is a valid move
            if (availableRow == -1)
            {
                throw new Exception("Invalid column move made on grid");
            }

            grid[col, availableRow] = turn;
            return grid;
        }

        // Returns all valid columns in the game
        public List<byte> GetValidMoves(char[,] grid)
        {
            List<byte> validCols = new();
            int[] availableRows = GetAvailableRows(grid);

            for (int col = 0; col < grid.GetLength(0); col++)
            {
                if (availableRows[col] != -1) validCols.Add((byte)col);
            }
            return validCols;
        }

        // Gets the row a piece would fall in for each col
        public int[] GetAvailableRows(char[,] grid)
        {
            int[] rowAvailability = new int[7];
            for (int col = 0; col < 7; col++)
            {
                // If top of column full, no row available
                if (grid[col, 5] != ' ')
                {
                    rowAvailability[col] = -1;
                    continue;
                }

                // Get lowest empty row
                int row = grid.GetLength(1);
                while (grid[col, row - 1] == ' ')
                {
                    row--;
                    if (row == 0) break;
                }
                rowAvailability[col] = row;
            }
            return rowAvailability;
        }
    }
}
