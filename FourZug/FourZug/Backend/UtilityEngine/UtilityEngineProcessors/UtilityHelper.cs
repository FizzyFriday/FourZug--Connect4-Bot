namespace FourZug.Backend.UtilityEngine.UtilityEngineProcessors
{
    // The actual processor of the component

    // Contains helper methods such as getting valid moves for a grid
    internal static class UtilityHelper
    {

        public static string[] MakeMove(string[] stringBits, string bitsTurn, int posID)
        {
            stringBits = (string[])stringBits.Clone();
            List<int> validMoveIDs = ValidMoveIDs(stringBits);

            if (!validMoveIDs.Contains(posID))
            {
                throw new Exception("Invalid column move made on grid");
            }

            stringBits[posID] = bitsTurn;
            return stringBits;
        }

        // Makes a move onto a grid, and returns the new grid
        public static string[,] MakeMove(string[,] grid, string turn, int col)
        {
            // Clones grid so value is used not reference
            grid = (string[,])grid.Clone();

            // Returns valid column moves
            List<byte> validColumns = ValidColumns(grid);

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

        public static List<int> ValidMoveIDs(string[] stringBits)
        {
            List<int> validIds = new();
            byte topRow = 5;

            for (byte col = 0; col <= 6; col++)
            {
                int id = ColRowToID(col, topRow);
                if (stringBits[id] == "00") validIds.Add(id);
            }
            return validIds;
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

        // This will make integrating bitwise require less changes
        public static string PieceStringBitConvert(string c)
        {
            // Convert piece to string bits
            if (c == "X") return "10";
            else if (c == "O") return "01";
            else if (c == " ") return "00";

            // Convert string bits to piece
            if (c == "10") return "X";
            else if (c == "01") return "O";
            else if (c == "00") return " ";

            return "";
        }

        public static (int col, int row) IDToColRow(int id)
        {
            int col = id % 6, row = id / 6;
            return (col, row);
        }

        public static int ColRowToID(int col, int row)
        {
            int id = (col * 6) + row;
            return id;
        }
    }
}
