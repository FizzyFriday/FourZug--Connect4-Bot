using FourZug.Backend.TreeManager;
using FourZug.Backend.UtilityEngine;
using FourZug.Backend.HeuristicsEngine;


namespace FourZug.API
{
    // Provides a Backend interface for the frontend
    public interface IAPI
    {
        /*
         * Returns the best move given a game grid/board
         * @pre:
         *      @param - grid, represents the game board
         *      @param - turn, turn of current player
         * @post:
         *      @return - Returns column of best move
         */
        int BestMove(string[,] grid, string turn);


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
        string[,] MakeMove(string[,] grid, string turn, int col);


        /*
         * Returns valid columns based on a game grid/board
         * @pre:
         *      @param - grid, represents the gameboard
         * @post:
         *      @return - Returns an int list of valid column moves
         */
        List<int> ValidBoardColumns(string[,] grid);


        /*
         * Returns state of the game given a grid/board
         * @pre:
         *      @param - grid, represents the game board
         *      @param - turn, turn of current player
         * @post:
         *      @return - String, representing state of game 
         *          String can be: Win (for current player), Draw or StillInPlay
         */
        string BoardState(string[,] grid, string turn);
    }
}
