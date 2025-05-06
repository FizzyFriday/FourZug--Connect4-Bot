using FourZug.Frontend;
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
            return Bot.BestMove(grid, turn);
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
            return GameUtility.MakeMove(grid, turn, col);
        }

        // Returns the valid columns
        // Preconditions:
        //      grid - represents the game board
        // Postconditions:
        //      Return list of all valid columns
        public static List<int> ValidColumns(string[,] grid)
        {
            return GameUtility.ValidColumns(grid);
        }

        // Returns if game has ended
        // Preconditions:
        //      grid - represents the game board
        //      turn - turn of current player
        // Postconditions:
        //      Returns string, representing Win, Draw or StillInPlay
        public static string BoardState(string[,] grid, string turn)
        {
            return HeuristicsManager.GameState(grid, turn);
        }

    }
}
