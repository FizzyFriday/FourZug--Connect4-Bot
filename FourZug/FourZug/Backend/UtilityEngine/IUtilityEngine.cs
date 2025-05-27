namespace FourZug.Backend.UtilityEngine
{
    // The interface blueprint of the component

    public interface IUtilityEngine
    {
        // Creates and saves interface references of a component
        void InitComponentReferences();

        // Makes a move onto a grid, and returns the new grid
        string[,] MakeMove(string[,] grid, string turn, int col);

        // Returns all valid columns provided a game board
        List<byte> GetValidBoardColumns(string[,] grid);
    }
}
