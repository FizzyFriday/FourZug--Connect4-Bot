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
            const byte idGainFromCol = 6, idGainFromRow = 1;
            int id = (idGainFromCol * col) + (idGainFromRow * row);
            return id;
        }

        // Gets the piece at the row and column related to unique "id"
        public string PieceAtPositionID(string[,] grid, int ID)
        {
            const byte idGainFromCol = 6, idGainFromRow = 1;
            int col = ID / idGainFromCol, row = ID % idGainFromRow;
            return grid[col, row];
        }
    }
}
