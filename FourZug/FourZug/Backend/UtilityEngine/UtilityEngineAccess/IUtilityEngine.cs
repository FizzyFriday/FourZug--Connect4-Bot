namespace FourZug.Backend.UtilityEngine.UtilityEngineAccess
{
    // The interface blueprint of the component

    public interface IUtilityEngine
    {
        // Makes a move using string bits
        string[] MakeMove(string[] stringBits, string bitsTurn, byte posID);

        string[,] MakeMove(string[,] grid, string turn, int col);

        List<byte> ValidMoveIDs(string[] stringBits);

        // Uses string bits to get valid board columns
        List<int> GetValidMoves(string[,] grid);

        // Converts 2D string grid to 1D array of strings representing bits
        string[] Flatten2DGrid(string[,] grid);

        // Converts a 1D array of strings representing bits to 2D grid
        string[,] Unflatten1DGrid(string[] grid);

        // Converts a grid row and columns into an unique "id"
        byte ColRowToID (int col, int row = -1);

        (int col, int row) ColRowFromID(byte id);

        public sbyte NextEmptyIDInCol(string[] stringBits, int col);
    }
}
