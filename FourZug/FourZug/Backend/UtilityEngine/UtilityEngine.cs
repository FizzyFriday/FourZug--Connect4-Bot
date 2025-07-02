using Accessibility;

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

            foreach (int row in availableRows)
            {
                if (row != -1) validCols.Add((byte)row);
            }
            return validCols;
        }

        public int[] GetAvailableRows(char[,] grid)
        {
            int[] rowAvailability = new int[7];
            for (int col = 0; col < 7; col++)
            {
                rowAvailability[col] = -1;
                int row = grid.GetLength(1);
                while (grid[col, row - 1] == ' ')
                {
                    row--;
                    rowAvailability[col] = row;
                    if (row == 0) break;
                }
            }
            return rowAvailability;
        }
    }
}
