using Accessibility;
using FourZug.Backend.UtilityEngine.UtilityEngineProcessors;

namespace FourZug.Backend.UtilityEngine.UtilityEngineAccess
{
    // The implemented interface of the component

    public class UtilityEngine : IUtilityEngine
    {
        // Makes a move using string bits
        public string[] MakeMove(string[] stringBits, string bitsTurn, byte posID)
        {
            return UtilityHelper.MakeMove(stringBits, bitsTurn, posID);
        }

        // Make a move and return result. Used by API player move input.
        public string[,] MakeMove(string[,] grid, string turn, int col)
        {
            string bitsTurn = UtilityHelper.PieceStringBitConvert(turn);
            string[] stringBits = Flatten2DGrid(grid);

            byte posID = UtilityHelper.NextEmptyIDInCol(stringBits, col);
            string[] newStringBits = UtilityHelper.MakeMove(stringBits, turn, posID);
            return Unflatten1DGrid(newStringBits);
        }

        public List<byte> ValidMoveIDs(string[] stringBits)
        {
            return UtilityHelper.ValidMoveIDs(stringBits);
        }

        //  Returns the valid column moves of a board
        public List<int> GetValidMoves(string[,] grid)
        {
            string[] stringBits = Flatten2DGrid(grid);
            List<byte> validIDs = UtilityHelper.ValidMoveIDs(stringBits);

            List<int> validColumns = new();
            foreach (byte validID in validIDs)
            {
                int col = UtilityHelper.IDToColRow(validID).col;
                validColumns.Add(col);
            }

            return validColumns;
        }

        // Converts a grid row and columns into an unique "id"
        public byte RowColumnToID(int row, int col)
        {
            return UtilityHelper.ColRowToID(col, row);
        }

        // Converts 2D string grid to 1D byte grid
        public string[] Flatten2DGrid(string[,] grid)
        {
            string[] stringBits = new string[grid.GetLength(0) + grid.GetLength(1)];

            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 6; col++)
                {
                    int posID = RowColumnToID(row, col);
                    // Do string to byte conversion
                    stringBits[posID] = UtilityHelper.PieceStringBitConvert(grid[col, row]);
                }
            }
            return stringBits;
        }

        // Converts 1D byte grid to 2D string grid
        public string[,] Unflatten1DGrid(string[] stringBits)
        {
            string[,] grid = new string[7, 6];

            for (int id = 0; id < stringBits.Length; id++)
            {
                int idCol = id / 6, idRow = id % 6;
                grid[idCol, idRow] = UtilityHelper.PieceStringBitConvert(stringBits[id]);
            }
            return grid;
        }

        // Remove contract?
        public string StringToStringBits(string str)
        {
            return UtilityHelper.PieceStringBitConvert(str);
        }
    }
}
