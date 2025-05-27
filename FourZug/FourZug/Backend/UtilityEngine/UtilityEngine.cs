namespace FourZug.Backend.UtilityEngine
{
    // The implemented interface of the component

    public class UtilityEngine : IUtilityEngine
    {
        public void InitComponentReferences()
        { 
            // Create the interface references for UtilityHelper
        }

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
