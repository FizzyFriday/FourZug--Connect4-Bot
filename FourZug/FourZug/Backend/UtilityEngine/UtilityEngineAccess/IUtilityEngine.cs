namespace FourZug.Backend.UtilityEngine.UtilityEngineAccess
{
    // The interface blueprint of the component

    public interface IUtilityEngine
    {
        // Makes a move onto a grid, and returns the new grid
        string[,] MakeMove(string[,] grid, string turn, int col);

        // Returns all valid columns provided a game board
        List<byte> GetValidBoardColumns(string[,] grid);

        // Converts a grid row and columns into an unique "id"
        byte RowColumnToID(int row, int col);

        // Gets the piece at the row and column related to unique "id"
        string PieceAtPositionID(byte ID);
    }
}
