using FourZug.Backend;


namespace FourZug.API
{
    // Provides a Backend interface for the frontend
    internal static class API
    {
        /*
         * Returns the best move given a game grid/board
         * @pre:
         *      @param - grid, represents the game board
         *      @param - turn, turn of current player
         * @post:
         *      @return - Returns column of best move
         */
        public static int BestMove(string[,] grid, string turn)
        {
            return TreeManager.BestMove(grid, turn);
        }


        /*
         * Returns the grid after making column move
         * @pre:
         *      @param - grid, represents the game board
         *      @param - turn, turn of current player
         *      @param - col, column user selected
         * @post:
         *      @modify - Makes move change to grid
         *      @return - Returns changed grid
         */
        public static string[,] MakeMove(string[,] grid, string turn, int col)
        {
            return UtilityEngine.MakeMove(grid, turn, col);
        }


        /*
         * Returns valid columns based on a game grid/board
         * @pre:
         *      @param - grid, represents the gameboard
         * @post:
         *      @return - Returns a list of valid column moves
         */
        public static List<int> ValidColumns(string[,] grid)
        {
            return UtilityEngine.ValidColumns(grid);
        }


        /*
         * Returns state of the game given a grid/board
         * @pre:
         *      @param - grid, represents the game board
         *      @param - turn, turn of current player
         * @post:
         *      @return - String, representing state of game 
         *          String can be: Win (for current player), Draw or StillInPlay
         */
        public static string BoardState(string[,] grid, string turn)
        {
            return HeuristicsEngine.GetGameState(grid, turn);
        }
    }
}
