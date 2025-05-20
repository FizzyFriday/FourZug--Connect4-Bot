using FourZug.Backend;

namespace FourZug.API
{
    // Handles Frontend requests to the Backend
    internal static class API
    {
        // Returns best move from the bot
        // Preconditions:
        //     grid - represents the game board
        //     turn - turn of current player
        // Postconditions:
        //     Return column of best move
        public static int BestMove(string[,] grid, string turn)
        {
            return TreeManager.BestMove(grid, turn);
        }

        // Returns the grid after making column move
        // Preconditions:
        //     grid - represents the game board
        //     turn - turn of current player
        //     col - column user entered
        // Postconditions:
        //     Return grid after column move
        public static string[,] MakeMove(string[,] grid, string turn, int col)
        {
            return UtilityEngine.MakeMove(grid, turn, col);
        }

        // Returns the valid columns
        // Preconditions:
        //      grid - represents the game board
        // Postconditions:
        //      Return list of all valid columns
        public static List<int> ValidColumns(string[,] grid)
        {
            return UtilityEngine.ValidColumns(grid);
        }

        // Returns if game has ended
        // Preconditions:
        //      grid - represents the game board
        //      turn - turn of current player
        // Postconditions:
        //      Returns string, representing Win for current player, Draw or StillInPlay
        public static string BoardState(string[,] grid, string turn)
        {
            return HeuristicsEngine.GetGameState(grid, turn);
        }
    }
}
