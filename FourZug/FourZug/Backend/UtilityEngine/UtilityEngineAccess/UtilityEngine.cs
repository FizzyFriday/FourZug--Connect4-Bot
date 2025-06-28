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
            return UtilityHelper.MakeMove(grid, turn, col);
        }

        public List<byte> ValidMoveIDs(string[] stringBits)
        {
            return UtilityHelper.MoveOptionIDs(stringBits);
        }

        //  Returns the valid column moves of a board
        public List<int> GetValidMoves(string[,] grid)
        {
            string[] stringBits = Flatten2DGrid(grid);
            List<byte> validIDs = UtilityHelper.MoveOptionIDs(stringBits);

            List<int> validColumns = new();
            foreach (byte validID in validIDs)
            {
                int col = UtilityHelper.IDToColRow(validID).col;
                validColumns.Add(col);
            }

            return validColumns;
        }

        // Converts a grid row and columns into an unique "id"
        public byte ColRowToID(int col, int row)
        {
            return UtilityHelper.ColRowToID(col, row);
        }
<<<<<<< HEAD

        // Converts 2D string grid to 1D byte grid
        public string[] Flatten2DGrid(string[,] grid)
        {
            return UtilityHelper.Flatten2DGrid(grid);
        }

        // Converts 1D byte grid to 2D string grid
        public string[,] Unflatten1DGrid(string[] stringBits)
        {
            return UtilityHelper.Unflatten1DGrid(stringBits);
        }

        public (int col, int row) ColRowFromID(byte id)
        {
            return UtilityHelper.IDToColRow(id);
        }

        public sbyte NextEmptyIDInCol(string[] stringBits, int col)
        {
            return UtilityHelper.NextSpotInCol(stringBits, col);
        }
=======
>>>>>>> parent of 8fd35d5 (Added byte and string array conversion contracts)
    }
}
