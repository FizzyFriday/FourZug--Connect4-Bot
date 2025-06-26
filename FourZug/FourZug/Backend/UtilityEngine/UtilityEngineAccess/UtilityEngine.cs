using Accessibility;
using FourZug.Backend.UtilityEngine.UtilityEngineProcessors;

namespace FourZug.Backend.UtilityEngine.UtilityEngineAccess
{
    // The implemented interface of the component

    public class UtilityEngine : IUtilityEngine
    {
        // Make a move and return result
        public string[,] MakeMove(string[,] grid, string turn, int col)
        {
            string[,] boardResult = UtilityHelper.MakeMove(grid, turn, col);
            return boardResult;
        }

        //  Returns the valid column moves of a board
        public List<byte> GetValidBoardColumns(string[,] grid)
        {
            List<byte> validColumns = UtilityHelper.ValidColumns(grid);
            return validColumns;
        }

        // Converts a grid row and columns into an unique "id"
        public int RowColumnToID(int row, int col)
        {
            const byte idGainFromCol = 6;
            int id = (idGainFromCol * col) + (row);

            return id;
        }

        // Gets the piece at the row and column related to unique "id"
        public string PieceAtPositionID(string[,] grid, int ID)
        {
            const byte idGainFromCol = 6;
            int col = ID / idGainFromCol, row = ID % idGainFromCol;
            return grid[col, row];
        }

        // Converts 2D string grid to 1D byte grid
        byte[] StringGridToByteGrid(string[,] grid)
        {
            return null;
        }

        // Converts 1D byte grid to 2D string grid
        string[,] ByteGridToStringGrid(byte[] grid)
        {
            return null;
        }
    }
}
