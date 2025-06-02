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
    }
}
