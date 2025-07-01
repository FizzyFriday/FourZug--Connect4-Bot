namespace FourZug.Backend.ta
{
    // The interface blueprint of the component

    public interface IUtilityEngine
    {
        // Makes a move onto a grid, and returns the new grid
        char[,] MakeMove(char[,] grid, char turn, int col);

        // Returns all valid columns provided a game board
        List<byte> GetValidMoves(char[,] grid);

        // Converts a grid row and columns into an unique "id"
        int RowColumnToID(int row, int col);

        // Gets the piece at the row and column related to unique "id"
        char PieceAtPositionID(char[,] grid, int ID);
    }
}
