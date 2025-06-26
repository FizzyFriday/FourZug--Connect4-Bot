namespace FourZug.Backend.UtilityEngine.UtilityEngineAccess
{
    // The interface blueprint of the component

    public interface IUtilityEngine
    {
        // Makes a move using string bits
        string[] MakeMove(string[] stringBits, string turn, int col);

        // Makes a move onto a grid, and returns the new grid
        string[,] MakeMove(string[,] grid, string turn, int col);

        // Uses string bits to get valid board columns
        List<string> GetValidMoveIDs(string[] stringBits);

        // Returns all valid columns provided a game board
        List<byte> GetValidBoardColumns(string[,] grid);

        // Converts a grid row and columns into an unique "id"
        int RowColumnToID(int row, int col);s

        // Converts 2D string grid to 1D array of strings representing bits
        string[] Flatten2DGrid(string[,] grid);

        // Converts a 1D array of strings representing bits to 2D grid
        string[,] Unflatten1DGrid(string[] grid);

        string StringToStringBits(string str);
    }
}
